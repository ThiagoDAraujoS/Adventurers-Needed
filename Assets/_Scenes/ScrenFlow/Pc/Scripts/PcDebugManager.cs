using UnityEngine;
using UnityEngine.SceneManagement;

public class PcDebugManager : MonoBehaviour
{
    [SerializeField]
    string testScene;

    [SerializeField]
    string  characterName;

    [SerializeField]
    int playerCount;

    [SerializeField]
    int teamCount;

    public int TeamSize { get { return Mathf.FloorToInt(playerCount / teamCount); } }

    void Start () {
        SocketManager sm = (new GameObject()).AddComponent<SocketManager>();
        sm.SpawnFakeSockets(playerCount);

        for (int i = 0; i < playerCount; i++){
            sm.Sockets[i].CharacterInfo = new CharacterInfo(i % teamCount,i/TeamSize, characterName);
            sm.Sockets[i].TabletInfo = new TabletInfo(null, null, i+1);
        }
        DontDestroyOnLoad(sm.gameObject);
        SceneManager.LoadScene(testScene);
	}
}
