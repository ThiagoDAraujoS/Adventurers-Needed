using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

namespace ScreenFlow.Tablet
{
    public class TbMainMenuManager : Photon.PunBehaviour
    {
        public string NextScene;
        public string roomCode;
        public BoolUnityEvent OnNameMatch;
        public bool validRoom = false;
        public GameObject ClientConnectionObject;
        public InputField inputField;
        public AudioClip gemSound;

        public void Start()
        {
        }

        public void Update()
        {
            SetRoomCode(inputField.text);

        }

        public void PlayGemSound()
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = gemSound;
            audio.Play();
        }

        public void SetRoomCode(string roomCode)
        {
            validRoom = PhotonNetwork.GetRoomList().Any(o => o.name == roomCode);                
            OnNameMatch.Invoke(validRoom);
            this.roomCode = roomCode;
        }

        public void ChangeScene()
        {
            SceneManager.LoadScene(NextScene);
        }

        public void JoinRoom()
        {
            if(validRoom)
            {
                PhotonNetwork.JoinRoom(roomCode);
                StartCoroutine(WaitForPlayers());
            }
        }

        IEnumerator WaitForPlayers()
        {
            yield return new WaitUntil(() => ServerProxyObject.s != null);
            yield return new WaitUntil(() => ServerProxyObject.s.IsRoomFull);

            DontDestroyOnLoad(FindObjectOfType<ServerProxyObject>());

            ChangeScene();
        }
    }
    [Serializable]
    public class BoolUnityEvent : UnityEvent<bool>
    {

    }
}