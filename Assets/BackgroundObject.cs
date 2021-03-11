using UnityEngine;
using System.Collections;
using Photon;

public class BackgroundObject : Photon.PunBehaviour {

    public Material background;
    public TeamColor colorList;
	// Use this for initialization
    void Awake()
    {

    }

    void Start () {
        StartCoroutine(SetBackgroundColor());
    }

    IEnumerator SetBackgroundColor()
    {
        ClientProxyObject cpoReference = FindObjectOfType<ClientProxyObject>();
        yield return new WaitUntil(() => cpoReference != null);

        int myTeamID = ClientProxyObject.singletonList[PhotonNetwork.player.ID-2].MyTeam;

        GetComponent<Renderer>().material.SetColor("_Color", colorList.list[myTeamID]);
        /*
        switch (myTeamID)
        {
            case (0):
                GetComponent<Renderer>().material.SetColor("_Color", colorList.list[0]);
                break;
            case (1):
                GetComponent<Renderer>().material.SetColor("_Color", colorList.list[1]);
                break;
            case (2):
                GetComponent<Renderer>().material.SetColor("_Color", colorList.list[2]);
                break;
            case (3):
                GetComponent<Renderer>().material.SetColor("_Color", colorList.list[3]);
                break;
            default:
                break;
        }*/
    }
	
	// Update is called once per frame
	void Update () {
	}
}
