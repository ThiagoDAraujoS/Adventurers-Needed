using UnityEngine;
using System.Collections;
using Photon;
using System;

/// <summary>
/// Class to handle initalization of Photon client on tablet interface device
/// </summary>
[Serializable]
public class ConnectionHandler : Photon.PunBehaviour
{
    //Communication with menu UI
    private LobbyList lobbyList;
    private bool inMenu;
    private ClientProxyObject localProxy;

    /// <summary>
    /// Initialize PhotonNetwork connection!
    /// </summary>
    public void Awake()
    {
        //Keep object persistent through scenes
        DontDestroyOnLoad(transform.gameObject);

        lobbyList = FindObjectOfType<LobbyList>();

        inMenu = (lobbyList != null);


        PhotonNetwork.ConnectUsingSettings("0.22");
    }

    /// <summary>
    /// Print relevant connection information to screen for debugging purposes. Temporary.
    /// </summary>
    private void OnGUI()
    {
     //   GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    /// <summary>
    /// Handles what to do when player has initially joined the room.
    /// Use this to set up things relevant to the player!
    /// </summary>
    public override void OnJoinedRoom()
    {
        StartCoroutine(RegisterSelfToServer());
    }

    /// <summary>
    /// Wait until ServerProxyObject exists before sending Register command
    /// </summary>
    /// <returns> N/A </returns>
    IEnumerator RegisterSelfToServer()
    {
        while (ServerProxyObject.s == null)
            yield return null;

        GameObject localProxyGO = PhotonNetwork.Instantiate("ClientProxyPrefab", new Vector3(0, 0, 0), Quaternion.identity, 0);
        localProxy = localProxyGO.GetComponent<ClientProxyObject>();

        Debug.LogError(PhotonNetwork.room.playerCount);

        //localProxy.playerNumbr = PhotonNetwork.player.ID-2;
        localProxy.playerNumber = PhotonNetwork.player.ID-2;
        Debug.LogError(localProxy.playerNumber);
        BackgroundObject bgo = FindObjectOfType<BackgroundObject>();

        Debug.LogError("CALLED!");
        ServerProxyObject.s.photonView.RPC("RegisterClientConnection", ServerProxyObject.s.photonView.owner, PhotonNetwork.player);

    }

    /// <summary>
    /// Join the room that was clicked.
    /// </summary>
    public override void OnJoinedLobby()
    {
        print("ROOMS AVAILABLE : " + PhotonNetwork.countOfRooms);
       // PhotonNetwork.JoinRoom("Room11111");
        //lobbyList.CreateRoomLobby(PhotonNetwork.countOfRooms);
    }
}