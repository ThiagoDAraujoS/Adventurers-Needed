using UnityEngine;

public class Spin : MonoBehaviour {

    public Vector3 spin;
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(spin);
	}
}
