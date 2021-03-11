using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using Photon;

public class ReadyButton : Photon.PunBehaviour, IPointerClickHandler
{
    private ActionQueue aQ;
    public PlanNEW gamePlan;
    public int count;
    public AudioClip sendPlanSound;
    private Image readyButton;
    private Text readyText;

    void Start()
    {
        aQ = FindObjectOfType<ActionQueue>();
        StartCoroutine(GetPlayerCount());
        readyButton = GetComponent<Image>();
        readyText = GetComponentInChildren<Text>();
        Color c = readyButton.color;
        c.a = 0;
        readyButton.color = c;
        readyText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ActionQueue.canSendActions == true)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = sendPlanSound;
            audio.Play();
            SendMessage();
            ActionQueue.canSendActions = false;
            //gameObject.SetActive(false);
            //TopBar.s.UpdateGameBrief("Swipe upwards to commit your action plan.");
            //TopBar.s.SetDialogOverlay(true);
        }
    }
    /// <summary>
    /// Wait until ServerProxyObject exists before sending Register command
    /// </summary>
    /// <returns> N/A </returns>
    IEnumerator GetPlayerCount()
    {
        while (ServerProxyObject.s == null)
            yield return null;

        count = PhotonNetwork.room.playerCount - 2;
    }


    private void SendMessage()
    {
        if (ActionQueue.canSendActions == true)
        {

            //Debug.LogError(aQ.plan.actions.Count);
            ClientProxyObject cpo = FindObjectOfType<ClientProxyObject>();
            //"Don't F this." - Tanner Steele
            gamePlan = aQ.plan;

            Debug.LogError("The count is: " + count);

            gamePlan.DebugMessage();

            string jsonPlan = JsonUtility.ToJson(gamePlan);

            if (ServerProxyObject.s != null)
            {
                ServerProxyObject.s.photonView.RPC("FinishPlanningEvent", ServerProxyObject.s.photonView.owner, PhotonNetwork.player.ID - 2, jsonPlan);
                if (InterfaceManager.s != null)
                    InterfaceManager.s.LockInterface();
            }

            aQ.ClearActionQueue(0);
            aQ.activeCard = null;
            Color c = readyButton.color;
            c.a = 0;
            readyButton.color = c;
            readyText.text = "";
        }
    }
}
