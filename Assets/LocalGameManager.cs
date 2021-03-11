using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LocalGameManager : MonoBehaviour {

    public string NextScene;
    public InputField playerName;
    public Button switchScreens;
    public string playersFinalizedName;

    public void OnClick()
    {
        if (playerName.text != "" && PhotonNetwork.insideLobby)
        {
            playersFinalizedName = playerName.text;

            PhotonNetwork.player.name = playersFinalizedName;
            print(PhotonNetwork.player.name);

            DontDestroyOnLoad(FindObjectOfType<ConnectionHandler>());
            
            SceneManager.LoadScene(NextScene);
        }
    }
}