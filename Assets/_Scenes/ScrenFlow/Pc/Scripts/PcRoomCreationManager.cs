using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class PcRoomCreationManager : MonoBehaviour {

    public Button button;

    //the next scene name that will be loaded
    public string NextScene;

    public string Credits;

    //the room code used to create the new room
    private string roomCode;

    //if the room name is valid
    public bool validRoom = false;

    public Image creditsImage;

    //the server connection handler prefab
    public GameObject ServerConnectionHandler;

    //the loading image
    public GameObject loadingImage;

    public int roomSize = 4;

    public Unravel unravel;

    public Text messageText;

    public string RoomCode
    {
        get
        {
            return roomCode;
        }

        set
        {
            roomCode = value.ToLower();
            messageText.text = roomCode;
        }
    }

    public void Start() {
        if (FindObjectOfType<ServerConnectionHandler>() == null) {
            GameObject obj = Instantiate(ServerConnectionHandler);
            DontDestroyOnLoad(obj);
        }

        creditsImage.enabled = false;
    }

    public void SetRoomCode(string roomCode) {
        if (!string.IsNullOrEmpty(roomCode) && !PhotonNetwork.GetRoomList().Any(o => o.name == roomCode)) {
           // AkSoundEngine.PostEvent("Play_Start_Game", gameObject);
            button.interactable = true;
            validRoom = true;
            unravel.IsOpen = true;
        } else {
            button.interactable = false;
            validRoom = false;
            unravel.IsOpen = false;
        }
        this.RoomCode = roomCode;
    }

    public void ChangeScene() {
        SceneManager.LoadScene(NextScene);
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene(Credits);
    }
    public void CreateRoom() {
        if (validRoom) {
            AkSoundEngine.PostEvent("Play_Green_button", gameObject);
            PhotonNetwork.CreateRoom(RoomCode);
            StartCoroutine(WaitForPlayers());
        }
    }

    public void SetToWait(bool status) {
        loadingImage.SetActive(status);
    }

    public void SetExpectedPlayers(int amount) {
        roomSize = amount;
    }

    IEnumerator WaitForPlayers() {
        SetToWait(true);
        yield return new WaitWhile(() => ServerProxyObject.s == null);
        ServerProxyObject.s.Initialize(roomSize);
        yield return new WaitUntil(WaitForPlayersRoutine);
    }

    public bool WaitForPlayersRoutine() {
        bool result = false;
        if (ServerProxyObject.s.IsRoomFull) {
            DontDestroyOnLoad(FindObjectOfType<ServerProxyObject>());
            ChangeScene();
            result = true;
        }
        return result;
    }

    public void DestroyRoom() {
        if (PhotonNetwork.inRoom) {
            PhotonNetwork.LeaveRoom();
            SetToWait(false);

           // Destroy(FindObjectOfType<ServerProxyObject>());
           // Destroy(FindObjectOfType<SocketManager>());
           // this.gameObject.AddComponent<SocketManager>();
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("PC_Credits");
            //creditsImage.enabled = !creditsImage.enabled;
        }
    }

}

