using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerClickHandler {

    [HideInInspector]
    public bool canJoin = true;
    private Text buttonFeedback;

    private ChangeScenes cS;

    private bool waiting, starting;

    public float interval;
    private float timer = 0;
    private int count = 0;

    void Start()
    {
        buttonFeedback = GetComponentInChildren<Text>();
        UpdateButtonText(0);
        cS = FindObjectOfType<ChangeScenes>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (starting && timer > interval)
        {
            buttonFeedback.text = "Begin in " + count;

            timer = 0;

            if (count == 0)
                #if UNITY_ANDROID
                    cS.LoadControllerScene();  
                #else
                    cS.LoadGameLevel(0);
                #endif

            else
                count--;
        }
        else if (waiting && timer > interval / 2)
        {
            string elipsis = "";
            for (int i = 0; i < count; i++)
            {
                elipsis += ".";
            }

            count++;
            timer = 0;

            buttonFeedback.text = "Waiting" + elipsis;

            if (count > 3)
                count = 0;
        }
    }

    public void UpdateButtonText(int playerCount)
    {
        #if UNITY_ANDROID
            if(playerCount < 3) {
                buttonFeedback.text = "Leave Room";
                return;
            }   
        #endif

        //Reset Values
        waiting = false;

        switch (playerCount)
        {
            case 0:
                buttonFeedback.text = "Launch App";
                break;
            case 1:
            case 2:
                waiting = true;
                starting = false;
                count = 0;
                break;
            case 3:
                starting = true;
                waiting = false;
                count = 3;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Only hand evetnts on android
        #if UNITY_ANDROID
            FindObjectOfType<LobbyList>().WaitRoomToggle(false);
            PhotonNetwork.LeaveRoom();
        #endif

        //Use as a back button
    }
}
