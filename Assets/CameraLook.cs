using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour {

    [HideInInspector]
    private Vector3 lookPosition = new Vector3();

    [SerializeField]
    private bool lockX, lockY, lockZ;

	void Start () {
        lookPosition = transform.position;
    }

	void Update () {
        if(!lockX)
            lookPosition.x = Camera.main.transform.position.x;
        if (!lockY)
            lookPosition.y = Camera.main.transform.position.y;
        if (!lockZ)
            lookPosition.z = Camera.main.transform.position.z;

        transform.LookAt(lookPosition);
	}
}
