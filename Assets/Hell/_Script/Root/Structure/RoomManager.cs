using UnityEngine;
using System.Collections.Generic;
using Network;
using Hell.Display;
using Hell.UI;
using System.Linq;
using System;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Hell
{

    /// <summary>
    /// Master singleton manager, this class cascade/initialize all other objects, 
    /// and this is a reference hub with connections to the most important components the fighting scene has
    /// </summary>
    public class RoomManager : MonoBehaviour
    {
        public TeamColor teamColor;

        public Image[] winBanners;

        public GameObject[] destructablePrefabs;
        public int destructableAmount;
        private void SpawnDestructables() {
            for (int i = 0; i < destructableAmount; i++) {
                Pawn temp = destructablePrefabs.Random().Instantiate<Pawn>(Color.black, transform);
                Tile tile = Board.GetRandomTile(Tile.State.destructable);
                temp.Tile = tile;
                temp.transform.position = temp.Tile.transform.position;
            }
        }


        public Text matchHistory;
        private int plansReceived = 0;

        public Tile.State[] teamSettings;

        public CharactersPrefabList characterPrefabs;

        public static readonly int AP_LIMIT = 4;

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static RoomManager s;

        /// <summary>
        /// Start method, gather all references needed to fill this class
        /// </summary>
        /// 
        void Awake() {
            s = this;
        }

        void Start() {
            InitializeTurnEngine();
        }

        public void InitializeTurnEngine()
        {
            Board = gameObject.InitializeComponentInChildren<Board>();

            CreateTeamsAndSpawnCharacters();

            SpawnDestructables();

            TurnEngine = gameObject.InitializeComponent<TurnEngine>();
        }
        
        public void CreateTeamsAndSpawnCharacters()
        {
            //Load and initialize the teams
            Teams = new List<Team>();

            if (ServerProxyObject.s != null) {
                for (int i = 0; i < ServerProxyObject.s.TeamCount; i++)
                    Teams.Add(Team.BuildTeam(transform, teamColor.list[i], teamSettings[i]));
            } else {
                int teamCount = SocketManager.s.Sockets.Max(so => (so.CharacterInfo.teamId))+1;
                for (int i = 0; i < teamCount; i++)
                    Teams.Add(Team.BuildTeam(transform, teamColor.list[i], teamSettings[i]));
            }
            //for each socket in socket manager... arrange them by teams and prepare to bind
            foreach (Socket socket in SocketManager.s.Sockets.OrderBy(s => s.CharacterInfo.teamId))
            {
                socket.PawnInfo = new PawnInfo(Teams[socket.CharacterInfo.teamId].

                    //after instantiating a prefab with the same name written in the prefablist
                    InstantiateACharacter(characterPrefabs.list.First(c => socket.CharacterInfo.characterName == c.name)));

                //initialize socket to hook events
                socket.Init();
            }

        }

        public ServerConnectionHandler ServerProxy { get; private set; }

        public List<Team> Teams { get; private set; }

        /// <summary>
        /// Reference to the turn manager
        /// </summary>
        public TurnEngine TurnEngine { get; private set; }

        /// <summary>
        /// Reference to the proxy
        /// </summary>
        public Proxy Proxy { get; private set; }

        /// <summary>
        /// Board reference
        /// </summary>
        public Board Board { get; private set; }

        /// <summary>
        /// UIManager reference
        /// </summary>
        public UIManager UIManager { get; private set; }

        /// <summary>
        /// Verify if the game is over
        /// </summary>
        /// <returns></returns>
        public bool VerifyVictory()
        {
            byte teamsAlive = 0;
            foreach (Team team in Teams)
                if (!team.IsDefeated)
                    teamsAlive++;

            return teamsAlive <= 1;
        }
        
        /// <summary>
        /// Calculate the player bounty
        /// </summary>
        public void CalculateBounty()
        {

        }

        /// <summary>
        /// Exit the room and do anything needed
        /// </summary>
        public void PrepareToExitRoom()
        {
            //clear their names!
            foreach(Socket playerSocket in SocketManager.s.Sockets)
            {
                playerSocket.CharacterInfo.characterName = "";
            }

            //desaturate
            CameraShader3.s.ramp = true;

            StartCoroutine(DisplayBanner());
        }

        public void UpdateConsole(string information)
        {
            print("Called to update the console");
            matchHistory.text += (information + "\n");
            plansReceived++;

            if (plansReceived == ServerProxyObject.s.RoomSize)
            {
                matchHistory.text = "";
                plansReceived = 0;
            }
        }

        public IEnumerator DisplayBanner()
        {
            int winningTeam = -1;

            yield return new WaitForSeconds(1.5f);

            foreach (var item in SocketManager.s.sockets.Where(o => o.PawnInfo.IsAlive)){
                winningTeam = item.CharacterInfo.teamId;
            }

            winBanners[winningTeam].enabled = true;
            AkSoundEngine.PostEvent("Play_Victory", gameObject);
            AkSoundEngine.PostEvent("Play_VO_AN_Victory", gameObject);
            AkSoundEngine.PostEvent("Play_Clapping", gameObject);
            AkSoundEngine.PostEvent("Play_Fireworks", gameObject);
            
            yield return new WaitForSeconds(8);

            ServerProxyObject.s.RedirectClients("TB_Select");
            SceneManager.LoadScene("PC_CharacterSelect");
        }
    }


}