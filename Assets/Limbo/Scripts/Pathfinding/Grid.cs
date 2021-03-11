using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hell;
using Hell.Display;
using System;

/// <summary>
/// Grid Class.
/// </summary>
public class Grid : MonoBehaviour
{
	public bool onlyDisplayPathGizmos;

	//unwalkable mask
	//FIXME
	public LayerMask unwalkableMask;

	//defining the area in world coordinates that the grid will cover
	//FIXME
	public Vector2 gridWorldSize;

	//defining how much space each individual node covers
	public float nodeRadius{ get { return Board.s.tileDistance / 2; } }

	//making a 2 dimensional array of nodes
	private Node[,] grid;

	public Node this [Coordinate index] {
		get {
			return grid [index.x, index.y];
		}
		set {
			grid [index.x, index.y] = value;
		}
	}
		

	//bool that will allow for pathfinding with/without diagonal movement
	public bool allowDiagonalPaths;

	float nodeDiameter{ get { return Board.s.tileDistance; } }


	int gridSizeX { get { return Board.s.boardSize; } }

	int gridSizeY { get { return Board.s.boardSize; } }

	public List<Node> path;

	public int MaxSize {
		get {
			return gridSizeX * gridSizeY;
		}
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start ()
	{
		//nodeDiameter = nodeRadius * 2;
		//calculating how many nodes we can fit into the gridWorldSize, and rounding them
		//	gridSizeX = Mathf.RoundToInt (gridWorldSize.x / nodeDiameter);
		//	gridSizeY = Mathf.RoundToInt (gridWorldSize.y / nodeDiameter);
		CreateGrid ();
	}

	//making a grid of Nodes
	/// <summary>
	/// Creates the grid.
	/// </summary>
	void CreateGrid ()
	{
		grid = new Node[Board.s.boardSize, Board.s.boardSize];

		for (int j = 0; j < grid.GetLength (1); j++)
			for (int i = 0; i < grid.GetLength (0); i++)
				grid [i, j] = new Node (new Coordinate (i, j));//  walkable, worldPoint, x, y);
	}

	/// <summary>
	/// Gets the neighbours.
	/// </summary>
	/// <returns>The neighbours.</returns>
	/// <param name="node">Node.</param>
	public List<Node> GetSurroundingTiles (Node node)
	{
		//making a list of nodes
		List<Node> surroundingTiles = new List<Node> ();

		for (int i = 0; i < Enum.GetValues (typeof(Direction)).Length; i++) {
			Direction direction = ((Direction)i);

			if (direction != Direction.nothing)
				surroundingTiles.Add (grid.Get (node.coord + direction));	
		}

		/*
		//making a loop to search in a 3x3
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (allowDiagonalPaths) {
					//skipping the iteration for the starting node
					if (x == 0 && y == 0) {
						continue;
					}
				} else {
					int isDiagonalNode = Mathf.Abs (x + y);
					if (isDiagonalNode == 0 || isDiagonalNode == 2) {
						continue;
					}
				}
				int checkX = node.X + x;
				int checkY = node.Y + y;
				//checking if inside the grid
				if ((checkX >= 0 && checkX < gridSizeX) && (checkY >= 0 && checkY < gridSizeY)) {
					//adding this node to the neighbour
					surroundingTiles.Add (grid [checkX, checkY]);
				}
			}
		}*/
		return surroundingTiles;
	}
	//converting the world position into a node coordinate
	/// <summary>
	/// Nodes from world point.
	/// </summary>
	/// <returns>The from world point.</returns>
	/// <param name="worldPosition">World position.</param>
	public Node NodeFromWorldPoint (Vector3 worldPosition)
	{
		//calculating how far along the position is via percentage of the grid
		float percentX = (worldPosition.x - transform.position.x) / gridWorldSize.x + 0.5f - (nodeRadius / gridWorldSize.x);
		float percentY = (worldPosition.z - transform.position.z) / gridWorldSize.y + 0.5f - (nodeRadius / gridWorldSize.y);
		//clamping these between 0 and 1 to ensure that if the world position is outside of the grid for whatever reason, it doesn't throw errors
		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		//getting the x and y indices of the grid array and rounding them
		int x = Mathf.RoundToInt ((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt ((gridSizeY - 1) * percentY);
		//returning the node coordinate
		return grid [x, y];
	}

	//using gizmos to visualize the pathing
	/// <summary>
	/// Raises the draw gizmos event.
	/// </summary>
	void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube (transform.position, new Vector3 (gridWorldSize.x, 1, gridWorldSize.y));

		if (onlyDisplayPathGizmos) {
			if (path != null) {
				foreach (Node n in path) {
					Gizmos.color = Color.black;
					//drawing a cube, giving it a position and a size. The subtraction to the diameter gives them a bit of space
					Gizmos.DrawCube (n.WorldPosition, Vector3.one * (nodeDiameter - 0.1f));
				}
			}
		} else {
			if (grid != null) {
				foreach (Node n in grid) {
					//using a ternary operator to make the grid white color if its walkable and red if not
					Gizmos.color = (n.IsWalkable) ? Color.white : Color.red;
					if (path != null) {
						if (path.Contains (n)) {
							Gizmos.color = Color.black;
						}
					}
					//drawing a cube, giving it a position and a size. The subtraction to the diameter gives them a bit of space
					Gizmos.DrawCube (n.WorldPosition, Vector3.one * (nodeDiameter - 0.1f));
				}
			}
		}
	}
}
/*

[System.Serializable]
public class TileOutRangeException : System.Exception
{
	public TileOutRangeException (Coordinate coord) : base ("Ai is  trying to walk to a place outside the board!!!! " + coord.x + "-" + coord.y)
	{
	}
}*/