using UnityEngine;
using Hell.Display;

namespace Hell
{
	/// <summary>
	/// The board contains all references to tiles,
	/// and anything else connected to the board itself
	/// The board also should contain any method that describe positioning rules
	/// </summary>
	public sealed partial class Board : MonoBehaviour
	{
        public MoveTimeline MoveTimeline { get; private set; }

        /// <summary>
        /// Singleton instance
        /// </summary>
        public static Board s;
        
		/// <summary>
		/// Board's tile matrix, this is used to keep track of all tile references inside the room
		/// </summary>
		[SerializeField][HideInInspector]
		public Tile[] board = null;

		/// <summary>
		/// Indexer overload, 
		/// This gets and sets the board matrix's tiles
		/// </summary>
		/// <param name="index">The desired tile coordinate</param>
		/// <returns>The desired tile</returns>
		public Tile this [Coordinate index] {
			get{ return board [boardSize * index.y + index.x]; }
			set{ board [boardSize * index.y + index.x] = value; }
		}

		/// <summary>
		/// Initialize method
		/// </summary>
		void Initialize (){
			s = this;
            MoveTimeline = gameObject.InitializeComponent<MoveTimeline>();
		}

		/// <summary>
		/// Get a tile
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public Tile this [int x, int y] {
			get{ return board [boardSize * y + x]; }
			set{ board [boardSize * y + x] = value; } 
		}

		/// <summary>
		/// Verifiy is the given coordinate is inside the board (this method is temporarely broken
		/// </summary>
		/// <returns>true if its inside the board</returns>
		public bool IsInsideBoard (Coordinate coord){
			return (coord.x < boardSize && coord.y < boardSize && coord.x >= 0 && coord.y >= 0);
		}

		public bool IsWalkable (Coordinate coord){
			return IsInsideBoard (coord) && this [coord].IsFree;
		}
        
        public bool IsValidTile(Coordinate coord) { 
            return IsInsideBoard(coord) && this[coord].state != Tile.State.solid;
        }

        /// <summary>
        /// iF there's a pawn inside the board
        /// </summary>
        /// <param name="coord">the coordinate where there could be a pawn in</param>
        /// <returns>if there is a pawn there</returns>
        public bool HasPawn(Coordinate coord){
            return (IsInsideBoard(coord) && this[coord].Pawn != null);
        }

		/// <summary>
		/// Get a random free tile
		/// </summary>
		/// <returns>return the random tile</returns>
		public Tile GetRandomTile (Tile.State? state = null)
		{
			return board.Random (tile => tile.IsFree && (state == null || tile.state == state));
		}

        public Vector3 TheoreticalTilePosition(Coordinate startingPosition, Direction dir, int distance, float? height = null){
            return TheoreticalTilePosition(GetRangedCoord(startingPosition,dir,distance), height);
        }

        public Vector3 TheoreticalTilePosition(Coordinate coord, float? height = null){
            return coord.ToVector3((height ?? transform.position.y)) * tileDistance;
        }

        public Coordinate GetRangedCoord(Coordinate startingPosition, Direction dir, int distance){
            return (dir.toCoord() * distance) + startingPosition;
        }

    }
}