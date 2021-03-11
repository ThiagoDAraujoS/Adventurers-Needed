using UnityEngine;
using System.Collections;

public class InputData : MonoBehaviour {

    public Vector3 areaSize;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}
