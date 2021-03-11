using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Hell;
using System.Collections;
using Hell.UI;

namespace Hell.Display
{
    public class Timeline : MonoBehaviour
    {
        public static readonly int AP_LIMIT = 4;

        /// <summary>
        /// Event fired when the starting of an act happen
        /// </summary>
        public event Action StartingPhase = delegate { };

        /// <summary>
        /// Event fired when the ending of an act happen
        /// </summary>
        public event Action EndingPhase = delegate { };

        /// <summary>
        /// Event fired when the start of a turn happen
        /// </summary>
        public event Action StartingTurn = delegate { };

        /// <summary>
        /// Event fired when the ending of a turn ended
        /// </summary>
        public event Action EndingTurn = delegate { };

        public event Action<Character> StartingPlayerAct = delegate { };

        public event Action<Character> EndingPlayerAct = delegate { };

        /// <summary>
        /// Set the plans list and and the iterator together
        /// </summary>
        public List<Plan> Plans { get; set; }

        public void RemoveMeFromPlanList(Character character)
        {
            Plan plan = Plans.SingleOrDefault(p => p.owner == character);
            if (plan != null)
                Plans.Remove(plan);
        }

        /// <summary>
        /// The entrypoint of the timeline, when this is called the timeline start to run
        /// </summary>
        /// <param name="plans">the plans about to be played by the timeline</param>
        public void RunTimeline(List<Plan> plans)
        {

            Plans = plans;

            StartCoroutine(RunTimelineCoroutine());
        }

        /// <summary>
        /// remove the empty plans from the timeline list
        /// </summary>
        /// <returns>return true if there are plans remaining after the cleaseing</returns>
        private bool CleanPlans()
        {

            Plans = Plans.Where(o => o.owner.IsAlive && o.actionQueue.Count > 0).ToList();

            return Plans.Count > 0;
        }

        /// <summary>
        /// Run timeline
        /// </summary>
        private IEnumerator RunTimelineCoroutine()
        {
            //Useful references
            Plan plan;
            HellIterator iterator;

            //->UNFREEZE SCREEN HERE

            //Hold a frame
            yield return null;

            //Order the plans by resolution layer
            Plans = Plans.OrderBy(o => o.timestamp).ToList();
         /*   string log = "";
            foreach (Plan item in Plans)
                log += item.ToString() + " ";
            Debug.Log(log);*/

            //Call starting turn event
            StartingTurn();

            Debug.Log("startingTurn");
            do
            {
                //Hold a frame
                yield return null;

                //Call starting phase event
                StartingPhase();

                //Initialize a new iterator object to cicle through all plans
                iterator = new HellIterator(Plans);

                do
                {
                    //->UI DISPLAY PLAYER OF THE TURN HERE

                    //get a reference of a plan
                    plan = Plans[iterator.value];

                    if (plan.owner.IsAlive)
                    {
                        //ask the owner to look at the thing he is doing
                        plan.owner.LookAt(plan.Top.Direction);

                        StartingPlayerAct(plan.owner);

                        //play the game display
                        yield return plan.Play(() =>
                            //ask board to play all his scheduled movements
                            StartCoroutine(Board.s.MoveTimeline.PlayBoardTimeline()));

                        //wait until the board ends to play movements
                        yield return new WaitWhile(() => Board.s.MoveTimeline.IsRunning);

                        EndingPlayerAct(plan.owner);

                        //-> WAIT FOR SOME TIME HERE
                        //Display damage at the end
                        yield return Pawn.WipeEachLifeBuffer();
                    }
                    //Iterate to the next player (if is not able to iterate ends the cicle)
                } while (iterator.Next());

                //Calls ending phase event
                EndingPhase();

                //clear all the plans and (if cannot do it, end cicle)
            } while (CleanPlans());

            //hold a frame
            yield return null;

            //call ending turn event
            EndingTurn();

            Debug.Log("ending turn");
            //hold a frame
            yield return null;

            //->FREEZE SCREEN HERE

            // say to the turn engine that the plans are done
            TurnEngine.s.FinishPlaying();
        }
    }
}