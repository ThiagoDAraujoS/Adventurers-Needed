using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hell;
using Photon;
using System.Linq;
public class SocketCountReader : Photon.MonoBehaviour
{

    public Text text;
    public int connectionCounter = 0;
    public void SetConnectionCounter(int n)
    {
        connectionCounter = n;
        text.text = connectionCounter.ToString();
    }


    public void Update()
    {
     //   if (PhotonNetwork.room != null)
      //      text.text = (PhotonNetwork.room.playerCount - 1).ToString();

    }

    void OnEnable()
    {
        SocketManager.s.OnSocketConnected += ChangeCounter;
    }
    void OnDisable()
    {
        SocketManager.s.OnSocketConnected -= ChangeCounter;
    }
    private void ChangeCounter(Socket s)
    {
        SetConnectionCounter(SocketManager.s.Sockets.Count(socket => socket.TabletInfo != null));
    }
    void Start()
    {
        StartCoroutine(UpdateText());
    }
    IEnumerator UpdateText()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            if (PhotonNetwork.room != null)
                text.text = (PhotonNetwork.room.playerCount - 1).ToString();
        }

    }

}
