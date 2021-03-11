using UnityEngine;
using System.Collections.Generic;
using Photon;
using System;
using Hell.Display;
using Hell;

namespace Network
{
    public class Proxy : PunBehaviour, ITurnSender, ITurnReceiver
    {
        public bool isMaster;
        public PhotonPlayer master;

        private Dictionary<int, PhotonPlayer> player;
        public Dictionary<int, PhotonPlayer> Player
        {
            get
            {
                if (player == null)
                    player = new Dictionary<int, PhotonPlayer>();
                return player;
            }
        }

        /// <summary>
        /// Initialize this component
        /// </summary>
        void Initialize()
        {
            PhotonNetwork.ConnectUsingSettings("0.1");
        }

        /// <summary>
        /// when the room is joined
        /// </summary>
        public override void OnJoinedRoom()
        {
            SetClientName();
            FillReferences();
            DebugReferenceList();
        }

        /// <summary>
        /// Renders a message warning what is happening with the connection
        /// </summary>
        void OnGUI()
        {
            GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        }

        /// <summary>
        /// when join a lobby join a random room
        /// </summary>
        public override void OnJoinedLobby()
        {
            PhotonNetwork.JoinRandomRoom();
            //        this.photonView.RPC("");
        }

        /// <summary>
        /// If theres no room to join at random creates one
        /// </summary>
        void OnPhotonRandomJoinFailed()
        {
            PhotonNetwork.CreateRoom(null);
        }

        /// <summary>
        /// Set this client name with MASTER or PLAYER X depending of its role
        /// </summary>
        private void SetClientName()
        {
            PhotonNetwork.player.name = (isMaster) ?
                "MASTER" :
                "PLAYER " + (PhotonNetwork.playerList.Length - 1);
        }

        /// <summary>
        /// Fill master/player dictionary references
        /// </summary>
        private void FillReferences()
        {
            //PhotonNetwork.InstantiateSceneObject

            for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
                if (PhotonNetwork.playerList[i].name != "MASTER")
                    Player.Add(i, PhotonNetwork.playerList[i]);
                else
                    master = PhotonNetwork.player;
        }

        /// <summary>
        /// Just throw some debugLines to check some stuff
        /// </summary>
        private void DebugReferenceList()
        {
            Debug.Log("I am... " + PhotonNetwork.player.name);

            string debug = "";

            foreach (KeyValuePair<int, PhotonPlayer> player in Player)
                debug += player.Value.name + "\n";
            Debug.Log(debug);
        }


        /// <summary>
        /// Send this message to all players to ask them to place their pawns
        /// </summary>
     //   [PunRPC]
        public void PlacePawns()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send a message to all pawns to place them randomly
        /// </summary>
   //     [PunRPC]
        public void PlacePawnsAtRandom()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send a message to all players to prepare to plan their turn
        /// </summary>
    //    [PunRPC]
        public void CallPlanning()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// send a message to master saying players are done placing their pawns
        /// </summary>
   //     [PunRPC]
        public void FinishPlacingPawns()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send a message to master saying players are done placing their pawns randomly
        /// </summary>
     //   [PunRPC]
        public void FinishPlacingPawnsAtRandom()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// send a message to master saying players are done with their planning
        /// </summary>
        /// <param name="plan"></param>
   //     [PunRPC]
        public void FinishPlanning(List<Plan> plan)
        {
            throw new NotImplementedException();
        }
    }
}