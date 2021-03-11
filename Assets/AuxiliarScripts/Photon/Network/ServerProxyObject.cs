using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Hell;
using Hell.Display;

public class ServerProxyObject : Photon.PunBehaviour
{
    /// <summary>
    /// Max number of players in the game
    /// </summary>
    private int roomSize = 4;

    /// <summary>
    /// property max number of players in game 
    /// </summary>
    public int RoomSize {
        get { return roomSize; }
        set { roomSize = value; }
    }

    /// <summary>
    /// The amount of teams in game
    /// </summary>
    public int TeamCount = 2;

    /// <summary>
    /// property to the amount of teams in game
    /// </summary>
    public int TeamSize { get { return RoomSize / TeamCount; } }

    /// <summary>
    /// singleton instance
    /// </summary>
    public static ServerProxyObject s;

    public bool wasInitialized = false;

    /// <summary>
    /// Server states
    /// </summary>
    public enum ServerListeningState {
        WAIT_FOR_PLAYERS,
        INSIDE_ROOM,
        BETWEEN_ROOMS,
        AI_MOVING,
        EXECUTING_PLANS
    }

    /// <summary>
    /// Current state
    /// </summary>
    public ServerListeningState __CURRENTSTATE__;

    /// <summary>
    /// Attach singleton instance
    /// </summary>
    private void Awake() {
        s = this;
    }

    /// <summary>
    /// Set the initial state
    /// </summary>
    private void Start() {
        __CURRENTSTATE__ = ServerListeningState.WAIT_FOR_PLAYERS;
    }

    private Dictionary<int, PlayerPlate> PlayerPlateDictionary = new Dictionary<int, PlayerPlate>();

    /// <summary>
    /// Initialize every serverproxy
    /// </summary>
    /// <param name="roomSize"></param>
    [PunRPC] public void Initialize(int roomSize)
    {
        wasInitialized = true;
        RoomSize = roomSize;
        StartCoroutine(SpawnSockets());
    }

    public IEnumerator SpawnSockets()
    {
        yield return new WaitUntil(() => SocketManager.s != null);
        SocketManager.s.SpawnSockets(roomSize);
        SortCharacters();
    }
    
    /// <summary>
    /// When tablets connects to the game run this method and save that connection to the socket
    /// </summary>
    /// <param name="photonPlayerReference"></param>
    [PunRPC] public void RegisterClientConnection(PhotonPlayer photonPlayerReference)
    {
        //if its not the server
        if (!photonPlayerReference.isMasterClient)
        {
            Socket socketToAdd = SocketManager.s.GetDisconnectedSocket();

            socketToAdd.TabletInfo = new TabletInfo(
                ClientProxyObject.singletonList[ClientProxyObject.singletonList.Count - 1],
                photonPlayerReference,
                photonPlayerReference.ID - 2);
            s.photonView.RPC("Initialize", socketToAdd.TabletInfo.player, RoomSize);

            if (SocketManager.s.GetDisconnectedSocket() == null) {
                PhotonNetwork.room.open = false;
                SortCharacters();
                RedirectClients("TB_Select");
            }
        }
    }

    /// <summary>
    /// return if the room is full
    /// </summary>
    public bool IsRoomFull {
        get {
            try{ return wasInitialized && PhotonNetwork.room.playerCount - 1 == RoomSize; }
            catch { return false;}
        }
    }

    /// <summary>
    /// set the room size
    /// </summary>
    [PunRPC] public void SetRoomSize(int amount) {
        RoomSize = amount;
    }

    /// <summary>
    /// Save character information (must be bind with ui)
    /// </summary>
    /// <param name="tabletId">the tablet id</param>
    /// <param name="characterName">the character chosen by the player</param>
    [PunRPC] public void ChooseCharacter(int tabletId, string characterName) {
        print("Called");
        SocketManager.s[tabletId].CharacterInfo.characterName = characterName;

        PlayerPlate myPlate = new PlayerPlate(SocketManager.s[tabletId]);
        PlayerPlateDictionary[tabletId] = myPlate;

        //  if (SocketManager.s.GetCharacterlessSocket() == null)
        //       RedirectClients("SwitchToInGameScreen");
        foreach(Socket socket in SocketManager.s.Sockets.Where(s=> s.TabletInfo != null))
        {
            //tell all tablets that something happened
            socket.TabletInfo.tablet.photonView.RPC("ReceiveUpdatedPlayerSelect", socket.TabletInfo.player, characterName);
        }
    }

    [PunRPC]
    public void ToggleTeam() {
        switch (RoomSize) {
            case 2:
                TeamCount = 2;
                //AkSoundEngine.PostEvent("Play_V0_AN_Free4All", gameObject);
                break;
            case 3:
                TeamCount = 3;
                //AkSoundEngine.PostEvent("Play_V0_AN_Free4All", gameObject);
                break;
            case 4: case 8:
                if (TeamCount == 2)
                {
                    //ffa
                  //  AkSoundEngine.PostEvent("Play_V0_AN_Free4All", gameObject);
                    TeamCount = 4;
                }
                else
                {
                  //  AkSoundEngine.PostEvent("V0_AN_TeamBattle", gameObject);
                    TeamCount = 2;
                }
                break;
            case 6:
                if (TeamCount == 2)
                {
                  //  AkSoundEngine.PostEvent("V0_AN_TeamBattle", gameObject);
                    TeamCount = 3;
                }
                else
                {
                  //  AkSoundEngine.PostEvent("V0_AN_TeamBattle", gameObject);
                    TeamCount = 2;
                }
                break;
        }
        SortCharacters();
        BroadcastTeams();
    }
    public void BroadcastTeams() {
        foreach (Socket socket in SocketManager.s.Sockets.Where(s => s.TabletInfo != null))
            socket.TabletInfo.tablet.photonView.RPC("SetTeam", socket.TabletInfo.player, socket.CharacterInfo.teamId, TeamSize);
    }


    /// <summary>
    /// Move clients to the next screen
    /// </summary>
    /// <param name="sceneName">the new screen</param>
    public void RedirectClients(string sceneName) {
        foreach (Socket socket in SocketManager.s.Sockets.Where(s => s.TabletInfo != null))
            socket.TabletInfo.tablet.photonView.RPC("ChangeScene", socket.TabletInfo.player, sceneName);
    }

    public void SendUnlockTabletMessage()
    {
        foreach(Socket s in SocketManager.s.sockets) {
            s.MyPlan = null;
            s.planTime = float.MaxValue;
        }

        foreach (Socket socket in SocketManager.s.Sockets.Where((s => s.TabletInfo != null)).Where(d => d.PawnInfo.IsAlive))
            socket.TabletInfo.tablet.photonView.RPC("UnlockTablet", socket.TabletInfo.player);
    }

    public void RedirectToGameControllers() {
        foreach (Socket socket in SocketManager.s.Sockets.Where(s => s.TabletInfo != null))
            socket.TabletInfo.tablet.photonView.RPC("ChangeScene", socket.TabletInfo.player, socket.CharacterInfo.characterName);
    }

    /// <summary>
    /// Send a message to a tablet
    /// </summary>
    /// <param name="clientToMessage">the tablet</param>
    /// <param name="messageToSend">the message</param>
    private void MessageClient(TabletInfo clientToMessage, string messageToSend) {
        clientToMessage.tablet.photonView.RPC("ReceiveMessage", clientToMessage.player, messageToSend);
    }

    /// <summary>
    /// Called when tablets finish to plan stuff, they put their id and a plan
    /// </summary>
    /// <param name="tabletId">the tablet id that called this function</param>
    /// <param name="jsonPlan">the plan generated by the player</param>
    [PunRPC] public void FinishPlanningEvent(int tabletId, string jsonPlan)
    {
        //if its the server
        if (photonView.isMine)
        {
            //transform the plan into a jsonobject and save a referent to it
            PlanNEW planToAdd = JsonUtility.FromJson<PlanNEW>(jsonPlan);

            //Add the owner information in it
            planToAdd.owner = tabletId;

            //save the plan inside the socket
            SocketManager.s[tabletId].MyPlan = planToAdd;
            SocketManager.s[tabletId].MyPlan.timestamp = Time.time;

            int myOrder = SocketManager.s.Sockets.Count(s => s.MyPlan != null);

            //convert to LINQ
            foreach(TabletInfo tbInfo in SocketManager.s.sockets)
            {
                if(tbInfo.tabletId == tabletId)
                {
                    tbInfo.tablet.photonView.RPC("ReceiveExecutionOrder", tbInfo.player, myOrder);
                    return;
                }
            }

            foreach (Socket characterSocket in SocketManager.s.sockets)
            {
                print("called");
                if (characterSocket.TabletInfo.tabletId == tabletId)
                {
                    print("MOVING " + myOrder + " : " + characterSocket.CharacterInfo.characterName);
                }
            }

            //string toShowOnScreen = ("Player " + tabletId.ToString() + " submitted their plan!");
            //print(toShowOnScreen);
            //RoomManager.s.UpdateConsole(toShowOnScreen);
        }
    }

    /// <summary>
    /// Multicoroutine stream that waits for all sockets to have a plan, when they all have a plan call the finishing plan routine
    /// </summary>
    public IEnumerator GetPlans()
    {
        //create a stream of coroutines, each one wait for one socket to have a plan, when they all the coroutine continue
        yield return this.StartMultiCoroutine(SocketManager.s.Sockets.Where(s=>s.PawnInfo.IsAlive).Select<Socket, CoroutineDel>(s => s.WaitForPlans).ToArray());

        //issue an order to the turn engine to keep running
        FinishPlanning(SocketManager.s.Sockets.Select(s => s.MyPlan).ToList(), null);
    }

    /// <summary>
    /// Get an artificial team...
    /// </summary>
    /// <param name="playerNumber"></param>
    /// <returns></returns>
    int FindMyTeam(int playerNumber){
        return (int)Mathf.Floor(((float)playerNumber) / ((float)TeamSize));
    }

    /// <summary>
    /// get an articifial player id...
    /// </summary>
    /// <param name="playerNumber"></param>
    /// <param name="teamNumber"></param>
    /// <returns></returns>
    int FindMyCharacterID(int playerNumber) {
        return playerNumber % TeamSize;
    }

    /// <summary>
    /// Sort all characters and record the right info inside their character info
    /// </summary>
    public void SortCharacters() {
        for (int i = 0; i < SocketManager.s.Sockets.Count; i++) {
            SocketManager.s.Sockets[i].CharacterInfo.playerId = FindMyCharacterID(i);
            SocketManager.s.Sockets[i].CharacterInfo.teamId = FindMyTeam(i);
            Debug.Log(SocketManager.s.Sockets[i].CharacterInfo.ToString());
        }
    }

    /// <summary>
    /// finish planning event
    /// </summary>
    public event Action<List<Plan>, Team> FinishPlanning;
}
