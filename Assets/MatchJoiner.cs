using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchJoiner : MonoBehaviour {
    public InputField roomToJoin;
    public Image waitImage;

	// Use this for initialization
	void Start () {
        waitImage.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        if (roomToJoin.text != "")
        {
            try
            {
                PhotonNetwork.JoinRoom(roomToJoin.text);

                waitImage.enabled = true;
            }
            catch
            {
                print("Room doesn't exist! Sorry!");
            }
        }
    }
}
