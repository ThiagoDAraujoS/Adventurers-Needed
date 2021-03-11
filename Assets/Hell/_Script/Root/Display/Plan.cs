using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace Hell.Display
{
    /// <summary>
    /// This object describes a plan, these plans are played by the framework
    /// </summary>
    public class Plan : IPlayable
    {
        public Character owner;
        public float timestamp; 
        /// <summary>
        /// The actual action queue
        /// </summary>
        public Queue<ActionToken> actionQueue;

        public float GetDuration() { return Top.Master.actDuration; }

        /// <summary>
        /// The size of the plan, this variable is changed as more actions are added to the plan
        /// </summary>
        public int PlanSize { get; private set; }

        /// <summary>
        /// Reveal the first plan in the queue
        /// </summary>
        /// <returns></returns>
        public ActionToken Top { get { return (actionQueue.Count > 0) ? actionQueue.Peek() : null; } }

        public bool IsRunning{ get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public Plan(Character owner)
        {
            this.owner = owner;
            actionQueue = new Queue<ActionToken>();
        }

        /// <summary>
        /// Add an action, if with that new action the plan is supose to extrapolate the apLimit for a plan it throws an error
        /// </summary> 
        /// <param name="token"></param>
        public void AddAction(ActionToken token)
        {
            if (PlanSize + token.Count > RoomManager.AP_LIMIT)
                throw new PlanOutOfBoundsException();

            actionQueue.Enqueue(token);
            PlanSize += token.Count;
        }
        public void AddAction(int actionID, Direction direction)
        {
            ActionToken token = new ActionToken(owner,actionID, direction);

            if (PlanSize + token.Count > RoomManager.AP_LIMIT)
                throw new PlanOutOfBoundsException();

            actionQueue.Enqueue(token);
            PlanSize += token.Count;
        }

        /// <summary>
        /// Transforms the plan into a string for debug
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = "Time - " + timestamp;
            result += "PLAN - ";

            foreach (var command in actionQueue)
                result += "AP " + command.Count + " ";
            result += "\n";

            return result;
        }

        /// <summary>
        /// Dequeue the first value of the plan
        /// </summary>
        /// <returns>return true if there are no tokens in this plan anymore</returns>
        private bool Dequeue()
        {
            bool result = false;
            actionQueue.Dequeue();
            if (actionQueue.Count <= 0)
                result = true;
            return result;
        }

        /// <summary>
        /// Run the start method of the top master act of this plan
        /// </summary>
        public void PlayStart()
        {
            Top.Master.RunStart(Top);
        }

        /// <summary>
        /// Run the update method of the top master act of this plan
        /// </summary>
        /// <param name="time"></param>
        public void PlayUpdate(float time)
        {
            Top.Master.RunUpdate(Top, time);
        }

        /// <summary>
        /// Run the End method of the top master act of this plan
        /// </summary>
        public void PlayEnd()
        {
//            Debug.Log("Ending Master");
            Top.Master.RunEnd(Top);
            if (!Top.MoveIterator())
                Dequeue();
        }

        /// <summary>
        /// Resolve the top master act of this plan, and dequeue the procotol if its ended 
        /// </summary>
        public void PlayResolution()
        {
            Top.Master.RunResolution(Top);
        }

        public static implicit operator Plan(PlanNEW pn)
        {
            Socket owner = SocketManager.s.sockets.First(o => o.TabletInfo.tabletId == pn.owner);
            Plan p = new Plan(owner.PawnInfo.MyPawn);
            foreach (var item in pn.actions)
                p.AddAction(item.ActionId, item.Direction.toDirection());
            return p;
        }
    }

    [Serializable]
    public class PlanOutOfBoundsException : Exception
    {
        public PlanOutOfBoundsException() { }
    }
}