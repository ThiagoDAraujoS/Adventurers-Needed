using UnityEngine;
using System.Collections;
using Photon;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public enum ClientUIState
{
    WAITING_FOR_OTHER_PLAYERS,
    NONE
}

public class ClientProxyObject : Photon.PunBehaviour
{

    public static List<ClientProxyObject> singletonList;
    public static ClientProxyObject _instance;
    private PlanNEW playerPlan;
    private bool commitedPlan;
    private ClientUIState currentState;
    public int playerNumber;
    public int MyTeam { get; private set; }

    private void Awake()
    {
        if (singletonList == null)
            singletonList = new List<ClientProxyObject>();
        singletonList.Add(this);

        _instance = this;

        DontDestroyOnLoad(transform.gameObject);
    }


    private void Start()
    {
        InvokeRepeating("CheckForServer", 4.0f, 0.1f);

    }

    private void CheckForServer()
    {
        if (ServerProxyObject.s == null)
        {
            //Application.Quit();
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("TB_Main");
        }
    }


    [PunRPC]
    public void FinishPlanning()
    {

    }

    public void SendPlan()
    {
        ServerProxyObject.s.photonView.RPC("CallPlanning", PhotonTargets.MasterClient, "Hey");
    }

    [PunRPC]
    public void ProvideResponse()
    {
        Debug.LogError("I got your message!!");
    }

    [PunRPC]
    public void TakeDamage(int amountTaken)
    {
        Debug.LogError("O I AM DEAD! " + amountTaken);
        //Handheld.Vibrate();
    }

    [PunRPC]
    public void UnlockTablet()
    {
        InterfaceManager.s.UnlockInterface();

#if UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }

    [PunRPC]
    public void SetTeam(int team, int teamsize)
    {
        MyTeam = team;
        TbCharacterSelectManager.s.SetTeam(MyTeam, teamsize);
    }


    [PunRPC]
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    [PunRPC]
    public void ReceiveUpdatedPlayerSelect(string characterSelected)
    {
        TbCharacterSelectManager.s.SetAvaliability(characterSelected, false);
        Debug.LogError(characterSelected + " was selected.");
    }

    IEnumerator PopulateTabletInterface()
    {
        Debug.LogError("CALLED POPULATE TABLET INTERFACE");

        ClientProxyObject localProxy = FindObjectOfType<ClientProxyObject>();
        BackgroundObject bgo = FindObjectOfType<BackgroundObject>();

        while (localProxy == null && bgo == null)
            yield return null;

        Debug.LogError(localProxy.playerNumber + " = PLAYER ID");

        if (bgo != null)
        {
            switch (localProxy.playerNumber)
            {
                case (0):
                    bgo.background.color = Color.green;
                    break;
                case (1):
                    bgo.background.color = Color.red;
                    break;
                case (2):
                    bgo.background.color = Color.blue;
                    break;
                default:
                    bgo.background.color = Color.gray;
                    break;
            }
        }
    }

    [PunRPC]
    public void ReceiveExecutionOrder(int orderId)
    {
        Debug.LogError("MY ORDER IS " + orderId);
        InterfaceManager.s.DisplayTurnOrder(orderId);
    }

    [PunRPC]
    public void CallPlanningEvent()
    {

    }

    [PunRPC]
    public void ReceiveMessage(string messageSent)
    {
        Debug.LogError(messageSent);
    }

    [PunRPC]
    public void ShowDeathScreen()
    {
        InterfaceManager.s.DeadScreen();
#if UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }

}