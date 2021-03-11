using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PcCharacterSelectManager : MonoBehaviour {

    public static bool gameId = true;

    //public string nextScene;
    public List<string> listOfScenes;

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitForPlayers());
	}

	public IEnumerator WaitForPlayers()
    {
        yield return new WaitUntil(() => SocketManager.s.Sockets.All(s => !string.IsNullOrEmpty(s.CharacterInfo.characterName)));
        ServerProxyObject.s.RedirectToGameControllers();

        print(listOfScenes.Count);
        AkSoundEngine.StopAll();
        gameId = !gameId;
        SceneManager.LoadScene((gameId) ? listOfScenes[0] : listOfScenes[1]);
    }
}
