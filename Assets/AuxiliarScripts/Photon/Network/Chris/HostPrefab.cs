using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HostPrefab : MonoBehaviour {

    public List<Sprite> roomIcons;

    [Range(0, 3)]
    public int playerCount;
    private int lastPlayerCount = 0;
    public int roomID;

    public Text playerDisplay;
    public Image thumbnail;

    public StartButton beginButton;

    private FigurineHandler figHandler;
    private RectTransform bgSize;

    // Use this for initialization
    void Awake()
    {
        figHandler = FindObjectOfType<FigurineHandler>();
        bgSize = GetComponent<RectTransform>();

        ShowMenuElements(false);

        float wait = 2;

        #if UNITY_ANDROID
            wait = wait / 5;
        #endif

        Invoke("SetRoomID", wait);

        lastPlayerCount = playerCount;
    }

    // Update is called once per frame
    void Update()
    {
     
        if (PhotonNetwork.room != null)
            playerCount = PhotonNetwork.room.playerCount - 1;



        if (lastPlayerCount != playerCount)
        {
            playerDisplay.text = "Players " + playerCount + "/3";

            #if !UNITY_ANDROID
                SetPlayerFigurines();
            #endif
           
            lastPlayerCount = playerCount;

            print("ass count" + lastPlayerCount);

            beginButton.UpdateButtonText(playerCount);
        }
    }

    public void SetRoomID() {

       //transform.name = "name: " + PhotonNetwork.room.name;

        ShowMenuElements(true);

        int indexOffset = 0;

        #if !UNITY_ANDROID
            indexOffset = 1;
        #endif

        //TODO Fix out of range bug -- WHY Tanner??!
        thumbnail.overrideSprite = roomIcons[PhotonNetwork.countOfRooms - indexOffset];
    }

    private void ShowMenuElements(bool show)
    {
        if (show)
            bgSize.sizeDelta = new Vector2(580, 640);
        else
            bgSize.sizeDelta = Vector2.zero;
    }

    private void SetPlayerFigurines()
    { 
        if (lastPlayerCount > playerCount) //Count has reduced
        {
            for (int i = lastPlayerCount - 1; i >= playerCount; i--)
            {
                figHandler.HideFigures(i);
            }
        }
        else //count has increased
        {
            for (int i = lastPlayerCount; i < playerCount; i++)
            {
                figHandler.ShowFigures(i);
            }
        }
    }
}
