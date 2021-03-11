using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System;

public class Seeker : MonoBehaviour
{
	public float speed;

	private List<Node> myNodes;

	//private Sequence moveSequence;

	private Tween moveTween;

	private const int NODE_DISTANCE = 1;

	/// <summary>
	/// Moves towards the targetposition
	/// </summary>
	/// <param name="position">Position.</param>
	/*public void MoveTowards (Vector3 position)
	{
		if (moveTween != null && moveTween.IsPlaying ()) {
			Debug.Log ("MoveTowards is called while the Seeker is already moving! This is not allowed.");
			return;
		}
		myNodes = Pathfinding.Instance.FindPath() CalculatePath (position);
		FindObjectOfType<Grid> ().path = myNodes;
		if (myNodes == null || myNodes.Count <= 1) {
			return;
		}
		moveTween = transform.DOMove (myNodes [1].WorldPosition, NODE_DISTANCE / speed);
		moveTween.Play ();
	}*/

	//	public List<Node> CalculatePath (Vector3 position)
	//	{
	//	return Pathfinding.Instance.FindPath (this.transform.position, position);
	//	}

	void Update ()
	{
		
	}
}
