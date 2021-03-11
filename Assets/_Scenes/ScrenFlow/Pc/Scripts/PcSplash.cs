using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PcSplash : MonoBehaviour {

    public static PcSplash s;
    void Start() {
        s = this;
    }

    public string NextScene;
    public GameObject ServerConnectionHandler;

    public GameObject
        anAnimator,
        readyUpAnimator,
        centerOfTheScene,
        tabletConnectionAnimator;

    public void SpawnAnimation(GameObject animation) {
        if (animation != null) {
            GameObject go = Instantiate(animation);
            go.transform.parent = transform;
            go.transform.position = centerOfTheScene.transform.position;
        }
    }

    public void InitializeConnection() {
        SpawnAnimation(tabletConnectionAnimator);
        if (FindObjectOfType<ServerConnectionHandler>() == null) {
            GameObject obj = Instantiate(ServerConnectionHandler);
            DontDestroyOnLoad(obj);
        }

        StartCoroutine(WaitForConnection());
    }

    IEnumerator WaitForConnection() {
        yield return new WaitUntil(() => PhotonNetwork.insideLobby);
        SceneManager.LoadScene(NextScene);
    }

    public void SwapScene() {
        if(PhotonNetwork.insideLobby)
            SceneManager.LoadScene(NextScene);
    }

}
