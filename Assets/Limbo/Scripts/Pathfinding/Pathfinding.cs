using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Pathfinding : Singleton<Pathfinding>
{
	public Transform seeker, target;
	Grid grid;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake ()
	{	//getting a refrence to our grid
		grid = GetComponent<Grid> ();
	}

	/// <summary>
	/// Finds the path.
	/// </summary>
	/// <param name="startPos">Start position.</param>
	/// <param name="targetPos">Target position.</param>
	public List<Node> FindPath (Node startPos, Node targetPos)
	{
		//converting world positions to nodes
		//	Node startingNode = grid.NodeFromWorldPoint (startPos);
		//	Node targetNode = grid.NodeFromWorldPoint (targetPos);

		//possibleNodes is the set of nodes that need to be evaluated
		List<Node> possibleNodes = new List<Node> ();
		//evaluatedNodes is the set of nodes already evaluated
		HashSet<Node> evaluatedNodes = new HashSet<Node> ();
		//adding the starting node right away
		possibleNodes.Add (startPos);

		//creating the loop
		while (possibleNodes.Count > 0) {
			//the current node opens at 0
			Node currentNode = possibleNodes [0];
			for (int i = 1; i < possibleNodes.Count; i++) {
				//comparing the node's fcost to the current node
				if (possibleNodes [i].fCost < currentNode.fCost || possibleNodes [i].fCost == currentNode.fCost && possibleNodes [i].hCost < currentNode.hCost) {
					//if the fcost is less, we set the current node to be that node
					currentNode = possibleNodes [i];
				}
			}
			possibleNodes.Remove (currentNode);
			evaluatedNodes.Add (currentNode);

			//if we have found our path, retrace it and return
			if (currentNode == targetPos) {
				return RetracePath (startPos, targetPos);
			}
			//checking if the surroundingTiles is not walkable or in the closed list
			foreach (Node surroundingTiles in grid.GetSurroundingTiles(currentNode)) {
				if (!surroundingTiles.IsWalkable || evaluatedNodes.Contains (surroundingTiles)) {
					continue;
				}
				//calculating the gcost for the new nodes
				int newMovementCostToNodes = currentNode.gCost + GetDistance (currentNode, surroundingTiles);
				//if the new path to the surroundingTiles is shorter or surroundingTiles aren't in the open list
				if (newMovementCostToNodes < surroundingTiles.gCost || !possibleNodes.Contains (surroundingTiles)) {
					surroundingTiles.gCost = newMovementCostToNodes;
					//calculating the hcost for the new surroundingTiles
					surroundingTiles.hCost = GetDistance (surroundingTiles, targetPos);
					//setting the parent to the current node
					surroundingTiles.parent = currentNode;
					//adding the neighbour to the open set
					if (!possibleNodes.Contains (surroundingTiles)) {
						possibleNodes.Add (surroundingTiles);
					}
				}
			}
		}

		return null;
	}

	/// <summary>
	/// Retraces the path.
	/// </summary>
	/// <param name="startNode">Start node.</param>
	/// <param name="endNode">End node.</param>
	public List<Node> RetracePath (Node startNode, Node endNode)
	{
		//list of nodes
		List<Node> path = new List<Node> ();
		//the current node is the end node, and traces it back to where it started
		Node currentNode = endNode;

		//retracing steps until it reaches the parent
		while (currentNode != startNode) {
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		//since it was tracing backwards we can reverse it so it traces to the destination
		path.Reverse ();
		//grid.path = path;
		return path;
	}

	/// <summary>
	/// Gets the distance.
	/// </summary>
	/// <returns>The distance.</returns>
	/// <param name="nodeA">Node a.</param>
	/// <param name="nodeB">Node b.</param>
	int GetDistance (Node nodeA, Node nodeB)
	{
        int diagonalCost = 14;
        int horizontalCost = 10;
		int distanceX = Mathf.Abs (nodeA.X - nodeB.X);
		int distanceY = Mathf.Abs (nodeA.Y - nodeB.Y);
		//14 is the cost of moving diagonally, 10 is the cost of vertically or horizontally
		if (distanceX > distanceY) {
			return diagonalCost * distanceY + horizontalCost * (distanceX - distanceY);
		}
		//if distanceX isnt shorter than distanceY, do the opposite
		return diagonalCost * distanceX + horizontalCost * (distanceY - distanceX);
	}
}
