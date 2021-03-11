using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using Hell.Display;
using System;

namespace Hell
{
    /// <summary>
    /// This class manages the turn steps and send messages allowing the current player 
    /// </summary>
    public class TurnEngine : MonoBehaviour
    {
        public event Func<Plan> OnLoadPlan;

        /// <summary>
        /// Singleton
        /// </summary>
        public static TurnEngine s;

        /// <summary>
        /// Teams reference
        /// </summary>
        public List<Team> Teams { get; set; }

        public Timeline Timeline { get; private set; }

        /// <summary>
        /// Initialize the engine
        /// </summary>
        void Initialize()
        {
            s = this;

            Timeline = gameObject.InitializeComponent<Timeline>();

            Teams = RoomManager.s.Teams;

            Debug.Log("<color=red> I fininish initializing </color>");

            PlacePawns();

            if(ServerProxyObject.s != null)
                ServerProxyObject.s.SendUnlockTabletMessage();

            AkSoundEngine.SetState("MistyField", "MF_1");
            StartCoroutine(CallPlanning());
        }

        private void PlacePawns(){
            foreach (Team team in Teams)
                team.PlacePawns();
        }

        private IEnumerator CallPlanning(){
            //Freezes screen with shader
            CameraShader3.s.ramp = true;

            //Locks UI and shows it
            UI.UIManager.S.GlobalState = UI.UIManager.LifeUIState.Unhidden;

            //controll music
            yield return new WaitForSeconds(2);
            AkSoundEngine.SetState("MistyField", "MF_2_3");

            //locked
            AkSoundEngine.PostEvent("Play_V0_AN_Choose_Act", gameObject);

            //unlock tablets to send plans
            if (ServerProxyObject.s != null)
                ServerProxyObject.s.SendUnlockTabletMessage();

            //wait for plans
            yield return this.StartMultiCoroutine(SocketManager.s.Sockets.
                Where(s => s.PawnInfo.IsAlive).
                Select<Socket,CoroutineDel>(s => s.WaitForPlan).
                ToArray());

            AkSoundEngine.PostEvent("Play_V0_AN_Fight", gameObject);

            //free tablets
            UI.UIManager.S.GlobalState = UI.UIManager.LifeUIState.Free;

            //set ui to hide
            UI.UIManager.S.SetAllLifeUI(false);

            //defrost camera
            CameraShader3.s.ramp = false;

            //controll music
            yield return new WaitForSeconds(2);
            AkSoundEngine.SetState("MistyField", "MF_4_5");

            List<Plan> planList = SocketManager.s.GetAllPlans();

            if (OnLoadPlan != null) 
                foreach (Delegate del in OnLoadPlan.GetInvocationList())
                    planList.Add((del as Func<Plan>)());

            //Run timeline with stuff
            Timeline.RunTimeline(planList);
        }

        /// <summary>
        /// Finish playing the timeline
        /// MUST be called at the end of the timeline
        /// </summary>
        public void FinishPlaying()
        {
            Debug.Log("<color=red> I finish playing </color>");
            if (RoomManager.s.VerifyVictory())
            {
                RoomManager.s.CalculateBounty();
                AkSoundEngine.StopAll();
                RoomManager.s.PrepareToExitRoom();
            }
            else
            {
                StartCoroutine(CallPlanning());
            }
        }
    }
}