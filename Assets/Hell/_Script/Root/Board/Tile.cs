using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Hell
{
    public class Tile : MonoBehaviour
    {
        [Serializable]
        public enum State
        {
            free,
            solid,
            team1Spawn,
            team2Spawn,
            team3Spawn,
            team4Spawn,
            destructable
        }

        [SerializeField]
        private GameObject[] prefabEffects;

        [HideInInspector]
        /// <summary>
        /// temporary effect on this tile
        /// </summary>
        public List<MasterTile> effects;

        void Start()
        {
            effects = new List<MasterTile>();
            foreach (var item in prefabEffects)
            {
                MasterTile master = item.Instantiate<MasterTile>();
                master.transform.parent = transform;
                effects.Add(master);
            }
        }
        public void AddEffect()
        {

        }
        public void RemoveEffect()
        {

        }


        /// <summary>
        /// if the tile is solid
        /// </summary>
        public State state = State.free;

        /// <summary>
        /// The pawn on this tile
        /// </summary>
        public Pawn Pawn;

        /// <summary>
        /// the coordinate of this tile
        /// </summary>
        [SerializeField]
        public Coordinate coord;

        public bool IsFree { get { return state != State.solid && Pawn == null; } }
  

        void OnDrawGizmos()
        {
            Board board = transform.parent.GetComponent<Board>();

            switch (state)
            {
                case State.free:
                    GizmosDrawTile(0.3f, 0.5f, 1.0f, board.tileDistance);
                    break;

                case State.solid:
                    GizmosDrawTile(1.0f, 0.0f, 0.0f, board.tileDistance);
                    break;

                case State.team1Spawn:
                    GizmosDrawTile(1.0f, 0.7f, 0.3f, board.tileDistance);
                    break;

                case State.team2Spawn:
                    GizmosDrawTile(0.8f, 0.2f, 0.8f, board.tileDistance);
                    break;

                case State.team3Spawn:
                    GizmosDrawTile(0.3f, 1.0f, 0.5f, board.tileDistance);
                    break;

                case State.team4Spawn:
                    GizmosDrawTile(0.2f, 0.1f, 0.8f, board.tileDistance);
                    break;

                case State.destructable:
                    GizmosDrawTile(0.5f, 0.5f, 0.5f, board.tileDistance);
                    break;

            }

        }

        public void GizmosDrawTile(float r, float g, float b, float distance)
        {
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation,Vector3.one);
            Gizmos.matrix = rotationMatrix;


            Gizmos.color = (Pawn != null)? new Color(r, g, b, 0.8f) : new Color(r, g, b, 0.3f);
            Gizmos.DrawCube(Vector3.zero, new Vector3(distance, 0.0f, distance));

            Gizmos.color = new Color(r, g, b);
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(distance * 0.98f, 0.0f, distance * 0.98f));
        }


    }
}