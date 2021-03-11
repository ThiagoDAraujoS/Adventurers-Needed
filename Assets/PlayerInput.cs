using UnityEngine;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour {

    public PlayerCustomization playerInfo = new PlayerCustomization();

    //Cache values to be sent over the network

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.gameObject);
	}
}
