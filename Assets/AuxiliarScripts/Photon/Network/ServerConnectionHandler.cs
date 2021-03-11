using UnityEngine;
using System.Collections;
using Photon;
using UnityEngine.UI;
using System;

/// <summary>
/// Class to handle initialization of Photon on host PC
/// </summary>
public class ServerConnectionHandler : Photon.PunBehaviour
{
    public static ServerConnectionHandler s;
    public event Action DoneConnecting;
    public Text debugText;

    //deprecated
    private const int MAXIMUM_CONNECTIONS_ALLOWED = 4;
    private const int MAXIMUM_ROOMS_ALLOWED = 5;

    private bool createdRoom = false;

    //Communication with menu UI
    private HostPrefab hostPrefab;
    private bool inMenu;


    void Awake()
    {

        s = this;

        hostPrefab = FindObjectOfType<HostPrefab>();
        inMenu = (hostPrefab != null);
    }

    void Start()
    {
        print("Connecting.");
        PhotonNetwork.ConnectUsingSettings("0.22");
    }

    void Update()
    {
     /*   if(PhotonNetwork.inRoom)
        {
            print("MAX PLAYER COUNT IS : " + PhotonNetwork.room.maxPlayers);
            print("CURRENT PLAYER COUNT IS : " + PhotonNetwork.room.playerCount);
        }*/
    }

    void OnGUI()
    {

     //   GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString()); 
    }

    public override void OnJoinedRoom()
    {
        //debugText.color = new Color(0f, 255f, 0f, 100f);
        PhotonNetwork.Instantiate("ServerProxyPrefab", Vector3.zero, Quaternion.identity, 0);
        //PhotonNetwork.Instantiate("ManagerObject", Vector3.zero, Quaternion.identity, 0);

        //PhotonNetwork.room.set

        if (DoneConnecting != null)
            DoneConnecting();


    }

    /// <summary>
    /// Called when a server instance of the game is run
    /// </summary>
    public override void OnJoinedLobby()
    {
    }

    /// <summary>
    /// ->!!! Ivo: Updated createdRoom bool to this check because the room was still null when createdRoom true
    /// ->!!! Thiago: I changed the loophole that sank into an infinite update cicle to a one time only coroutine 
    /// </summary>
    IEnumerator OpenRoom()
    {
        //If the count of rooms is diferrent from the maximum rooms allowed 
        //Thiago -> Tanner, should u not change this from "!=" to "<", != could created weird bugs
        if (PhotonNetwork.countOfRooms < MAXIMUM_ROOMS_ALLOWED)
        {
            //Create a room
            PhotonNetwork.CreateRoom("Room11111");

            //TODO If the lobby list is not null
            //if (hostPrefab != null)

            //Se the master client
            PhotonNetwork.SetMasterClient(PhotonNetwork.player);
        }

        //wait while the room is not created
        yield return new WaitWhile(() => PhotonNetwork.room == null);
        //as soon as the room is created keep going

        //if the player count explode the maximun conections
        //Thiago -> again inst better to have "x > y" than "x == y+1"
        if (PhotonNetwork.room.playerCount > MAXIMUM_CONNECTIONS_ALLOWED)

            //close the room
            PhotonNetwork.room.open = false;
    }
}