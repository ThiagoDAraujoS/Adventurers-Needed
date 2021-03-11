using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PcConnectToPhotonManager : Photon.PunBehaviour {

    public float duration;
    public string NextScene;
    public GameObject ServerConnectionHandler;


    void Start () {
        if (FindObjectOfType<ServerConnectionHandler>() == null)
        {
            //GameObject obj = Instantiate(ServerConnectionHandler);
            //DontDestroyOnLoad(obj);
        }

        //StartCoroutine(Wait());
        SceneManager.LoadScene(NextScene);
    }

	IEnumerator Wait() {
        while (!PhotonNetwork.insideLobby)
            yield return null;

        SceneManager.LoadScene(NextScene);
    }
}
