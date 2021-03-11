using UnityEngine;
using System.Collections;
using Hell;

public class Node
{
	public Coordinate coord;

	//making a bool that determines if the node is traversable
	public bool IsWalkable {
		get{ return Board.s.IsWalkable (coord); }
	}
	//finding out what point in the world this node represents
	public Vector3 WorldPosition { 
		get {
			Vector3 result = Vector3.zero;
			if (Board.s.IsInsideBoard (coord))
				result = Board.s [coord].transform.position; 
			return result;
		}
	}

	public int X{ get { return coord.x; } }

	public int Y{ get { return coord.y; } }

	//the gcost is the distance from the starting node
	public int gCost;

	//hcost or the heuristic is the distance from the end node
	public int hCost;

	public Node parent;

	public string Action;

	//constructor
	/// <summary>
	/// Initializes a new instance of the <see cref="Node"/> class.
	/// </summary>
	/// <param name="_walkable">If set to <c>true</c> walkable.</param>
	/// <param name="_worldPos">World position.</param>
	/// <param name="_gridX">Grid x.</param>
	/// <param name="_gridY">Grid y.</param>
	//	public Node (bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
	/*	{
		IsWalkable = _walkable;
		WorldPosition = _worldPos;
		//letting our node keep track of it's own position in the array
		X = _gridX;
		Y = _gridY;
	}*/

	public Node (Coordinate coord)
	{
		this.coord = coord;
	}

	/// <summary>
	/// Gets the f cost.
	/// </summary>
	/// <value>The f cost.</value>
	//fcost is equal to gcost + hcost, it never needs to be assigned, only calculated
	public int fCost {
		get {
			return gCost + hCost;
		}
	}
}
