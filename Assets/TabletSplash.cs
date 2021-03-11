using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TabletSplash : MonoBehaviour {

    public static TabletSplash s;

    void Start()
    {
        s = this;
    }

    public string NextScene;
    public GameObject ClientConnectionObject;

    public GameObject
        anAnimator,
        readyUpAnimator,
        centerOfTheScene;
    //      tabletConnectionAnimator;

    public void InitializeConnection()
    {
        // GameObject go = Instantiate(tabletConnectionAnimator);
        // go.transform.parent = transform;
        // go.transform.position = centerOfTheScene.transform.position;
        if (FindObjectOfType<ConnectionHandler>() == null)
        {
            GameObject obj = Instantiate(ClientConnectionObject);
            DontDestroyOnLoad(obj);
        }

        StartCoroutine(WaitForConnection());
    }

    IEnumerator WaitForConnection()
    {
        yield return new WaitUntil(() => PhotonNetwork.insideLobby);
        SceneManager.LoadScene(NextScene);
    }

    public void SwapScene()
    {
        if (PhotonNetwork.insideLobby)
            SceneManager.LoadScene(NextScene);
    }

}