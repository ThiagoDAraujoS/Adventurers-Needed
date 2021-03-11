using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Hell;
using System.Linq;
/*
public class Unit : MonoBehaviour, AIStateMachine
{
	public BaseState currentState {
		get;
		set;
	}

	private Seeker mySeeker;

	private Grid grid;

	private Dictionary<Vector3, string> directionToName = new Dictionary<Vector3, string> ();

	// Use this for initialization
	void Start ()
	{
		directionToName.Add (Vector3.forward, "move north");
		directionToName.Add (Vector3.down, "move south");
		directionToName.Add (Vector3.right, "move east");
		directionToName.Add (Vector3.left, "move west");
		mySeeker = GetComponent<Seeker> ();
		this.SetState<IdleState> ();
		grid = GetComponent<Grid> ();
	}
	
	// Update is called once per frame
	public List<string> PlanMoveToTarget (Character user)
	{
		//if (Input.GetKey (KeyCode.Space)) {
		Character target = GetClosestEnemy (grid [user.Coord]);

		var path = Pathfinding.Instance.FindPath (grid [user.Coord], grid [target.Coord]);//target.transform.position);
		List<string> nextDirections = new List<string> ();
		Vector3 lastNodePos = path [0].WorldPosition;
		for (int i = 1; i <= 4; i++) {
			Vector3 dirToNode = path [i].WorldPosition - lastNodePos;
			//print (dirToNode);

			string dirName = directionToName [dirToNode];
			nextDirections.Add (dirName);
			lastNodePos = path [i].WorldPosition;
			//print (dirName);
			//return dirName;
		}
		print (nextDirections.Count);
		print (nextDirections [0]);
		print (nextDirections [1]);
		print (nextDirections [2]);
		print (nextDirections [3]);
		return nextDirections;
		//nextDirections = null;
		//mySeeker.MoveTowards (target.transform.position);
	}
    /*
	public Character GetClosestEnemy (Node myself)
	{
		/*	return RoomManager.s.Teams
			.Single (team => !(team is TeamAi))
			.Characters.Min<Character> (character => {
			return Pathfinding.Instance.FindPath (myself, grid [character.Coord]).Count;
		});
*/
	/*	Character result = null;
		int minWay = int.MaxValue;
		foreach (Character character in RoomManager.s.Teams.Single (team => !(team is TeamAi)).Characters) {
			int distance = Pathfinding.Instance.FindPath (myself, grid [character.Coord]).Count;

			if (distance < minWay) {
				minWay = distance;
				result = character;
			}
		}	
		return result;*/
		/*
		int closestEnemyDistance = int.MaxValue;
		Enemy closestEnemy = null;
		List<Node> pathToEnemy = null;

		foreach (Enemy enemy in FindObjectsOfType<Enemy>()) {
			pathToEnemy = mySeeker.FindPath (enemy.transform.position);
			if (pathToEnemy == null)
				continue;
			
			int enemyDistance = pathToEnemy.Count;
			if (enemyDistance < closestEnemyDistance) {
				closestEnemy = enemy;
				closestEnemyDistance = enemyDistance;
			}
		}

		return closestEnemy;*/
/*	}
    */
/*	public void Test ()
	{
		if (Input.GetKey (KeyCode.A))
			print ("test");
	}*/

	/*
public List<Node> CalculatePath (Node start, Node target)
	{
		return Pathfinding.Instance.FindPath (start, target);
	}*/
/*}
*/