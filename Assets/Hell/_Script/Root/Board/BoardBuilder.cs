using UnityEngine;
using System;

namespace Hell
{
    public partial class Board : MonoBehaviour
    {
        /// <summary>
        /// The board size
        /// </summary>
        [SerializeField]
        public int boardSize = 10;

        /// <summary>
        /// The default tile prefab
        /// </summary>
        [SerializeField]
        private GameObject tilePrefab = null;

        /// <summary>
        /// The tile distance from eachother
        /// </summary>
        [SerializeField]
        public float tileDistance = 1.0f;

        /// <summary>
        /// Return if the board was initialized properly
        /// </summary>
        public bool WasInitialized
        {
            get { return board != null && transform.childCount > 0; }
        }

        /// <summary>
        /// Verify if the tile ammount in the box is the same as the ammount displayed in the scene
        /// </summary>
        public bool IsInSyncWithTileAmmount
        {
            get { return transform.childCount == boardSize * boardSize; }
        }

        /// <summary>
        /// Verify is the distance inbetween tiles displayed in the box is the same used to create the tile scene
        /// </summary>
        public bool IsInSyncWithDistance
        {
            get { return Vector3.Distance(transform.GetChild(0).position, transform.GetChild(1).position) == tileDistance; }
        }

        public float Duration
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Destroy all childs
        /// </summary>
        public void DestroyBoard()
        {
            if (board != null)
            {
                foreach (Tile tile in gameObject.GetComponentsInChildren<Tile>())
                    DestroyImmediate(tile.gameObject);

                board = null;
            }
        }

        /// <summary>
        /// Create a brand new board
        /// </summary>
        public void CreateBoard()
        {
            DestroyBoard();

            Tile tile;
            board = new Tile[boardSize * boardSize];

            for (int j = 0; j < boardSize; j++)
                for (int i = 0; i < boardSize; i++)
                {
                    tile = (Instantiate(tilePrefab,
                        new Vector3(i * tileDistance, 0.0f, j * tileDistance),
                        Quaternion.identity) as GameObject).GetComponentInChildren<Tile>();

                    tile.coord = new Coordinate(i, j);

                    tile.transform.parent = transform;

                    tile.name = "[" + i + "-" + j + "]";

                    this[tile.coord] = tile;
                }
        }

        /// <summary>
        /// Realocate the position of tiles in the board
        /// </summary>
        public void RelocateTiles()
        {
            foreach (Tile tile in board)
                tile.transform.position = new Vector3(
                    tile.coord.x * tileDistance,
                    tile.transform.position.y,
                    tile.coord.y * tileDistance);
        }

    }
}
