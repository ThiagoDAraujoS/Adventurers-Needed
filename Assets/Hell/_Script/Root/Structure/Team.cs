using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Hell.Display;
using System.Collections;

namespace Hell
{
    /// <summary>
    /// Master class that describes a Team.
    /// this class unifies the behaviour of the human team and ai team
    /// </summary>
    public class Team : MonoBehaviour//, ITurnSender, ITurnReceiver
    {
        public Color teamColor;

        /// <summary>
        /// the type of tile i'll spawn on
        /// </summary>
        public Tile.State spawningTile;

        /// <summary>
        /// all characters this player controll
        /// </summary>
        public List<Character> Characters { get; private set; }

        public bool IsDefeated {
            get { return Characters.All(c => !c.IsAlive); }
        }

        /// <summary>
        /// Virtual initialization method for any team class
        /// It's called by the master RoomManager class at the begining of he room
        /// </summary>
        protected virtual void Initialize() {
            Characters = new List<Character>();
        }

        /// <summary>
        /// player places its pawns around
        /// </summary>
        public void PlacePawns() {
            foreach (Character character in Characters)
            {
                Tile tile = Board.s.GetRandomTile(spawningTile);
                character.Tile = tile;
                character.transform.position = character.Tile.transform.position;
            }
        }

        public Character InstantiateACharacter(GameObject prefab)
        {
            if (Characters == null)
                Characters = new List<Character>();
            Character temp = prefab.Instantiate<Character>(teamColor, transform);
            Characters.Add(temp);
            return temp;
        }

        /// <summary>
        /// Build a team game object
        /// </summary>
        /// <returns>the team component inside the team game object</returns>
        public static Team BuildTeam(Transform parent, Color teamColor, Tile.State spawningZone) 
        {
            //instantiate the game object
            GameObject teamGameObject = new GameObject();

            //se the parent
            teamGameObject.transform.parent = parent;

            //places the component
            Team teamComponent = teamGameObject.AddComponent<Team>();

            //ajust the team color
            teamComponent.teamColor = teamColor;

            teamComponent.spawningTile = spawningZone;

            //return the team component
            return teamComponent;
        }

    }
}