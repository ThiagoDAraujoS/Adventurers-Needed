using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RoomPrefab : MonoBehaviour {

    [Range (0, 3)]
    public int playerCount;
    public int roomIndex;

    public Text playerDisplay;
    public Text buttonDisplay;
    public Image thumbnail;
	
	// Update is called once per frame
	void Update () {
        playerDisplay.text = "Players " + playerCount + "/3";
	}
}
