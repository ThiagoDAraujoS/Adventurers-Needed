using UnityEngine;
using System.Collections;
using Hell.Display;

public class DisplayIntent : MonoBehaviour {

    public Direction output;

    public float xPosition;
    public float zPosition;

    [SerializeField]
    private float angle, distance;

    private Transform offset;

	// Use this for initialization
	void Start () {
        offset = GetComponentInChildren<TrailRenderer>().transform;
	}
	
	// Update is called once per frame
	void Update () {
        //Mathf.Sqrt(sideALength * sideALength + sideBLength * sideBLength);

        xPosition = offset.localPosition.x;
        zPosition = offset.localPosition.z;

        distance = Vector3.Distance(transform.position, offset.position);

        CalculateDirection();
	}

    public void CalculateDirection()
    {
        angle = Mathf.Atan2(xPosition, zPosition) * Mathf.Rad2Deg;

        if (angle >= 135 || angle <= -135)
        {
            output = Direction.south;
        }
        else if (angle >= -45 && angle <= 45)
        {
            output = Direction.north;
        }
        else if (angle > 45 && angle < 135)
        {
            output = Direction.east;
        }
        else if (angle > -135 && angle < -45)
        {
            output = Direction.west;
        }
        else
        {
            output = Direction.nothing;
        }
    }
}
