using UnityEngine;
using System.Collections;
using Hell;

public class ManagerObject : MonoBehaviour {
    public static ManagerObject s;
	// Use this for initialization
    void Awake()
    {
        s = this;

    }

    /*
    public RoomManager getRM()
    {
        //return RoomManager.s;
    }
    */

	void Start () {
        //print(getRM());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
