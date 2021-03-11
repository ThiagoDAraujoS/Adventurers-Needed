using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LobbyList : MonoBehaviour {

    [HideInInspector]
    public List<RoomPrefab> roomPrefabs;

    public GameObject waitRoom;
    public StartButton startButton;

    private NoLobby noLobby;

    public List<Sprite> roomCodes;

    private bool inWaitRoom = false;

    //public bool[] roomStatus = new bool[5];

	// Use this for initialization
	void Start () {

        noLobby = GetComponentInChildren<NoLobby>();
        noLobby.gameObject.SetActive(false);

        int count = 0;

        foreach (RoomPrefab room in GetComponentsInChildren<RoomPrefab>())
        {
            roomPrefabs.Add(room);
            room.thumbnail.overrideSprite = roomCodes[count];
            count++;
        }

        WaitRoomToggle(false);
    }

    void Update()
    {
        if (!inWaitRoom)
            CreateRoomLobby();
    }

    public void CreateRoomLobby()
    {
        //RoomInfo[] stuff = PhotonNetwork.GetRoomList();

        inWaitRoom = false;

        DisableRoomList();

        int roomCount = PhotonNetwork.countOfRooms;

        if (roomCount <= 5 && roomCount > 0)
        {
            for (int i = 0; i < roomCount; i++)
            {
                roomPrefabs[i].gameObject.SetActive(true);
            }
        }
        else
        {
            noLobby.gameObject.SetActive(true);
        }
    }

    public void EnterWaitRoom(int index)
    {
        waitRoom.transform.GetChild(0).GetComponent<Image>().overrideSprite = roomCodes[index];
        PhotonNetwork.JoinRoom("Room" + index);

        WaitRoomToggle(true);
    }

    public void WaitRoomToggle(bool enter)
    {
        inWaitRoom = enter;

        waitRoom.SetActive(inWaitRoom);
        startButton.gameObject.SetActive(inWaitRoom);

        if (inWaitRoom)
            DisableRoomList();
        else
            CreateRoomLobby();
    }

    private void DisableRoomList()
    {
        foreach (RoomPrefab rP in roomPrefabs)
        {
            rP.gameObject.SetActive(false);
        }
        noLobby.gameObject.SetActive(false);
    }
}
