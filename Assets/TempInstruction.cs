using UnityEngine;
using System.Collections;

public class TempInstruction : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitForThreePleyers());
	}

    IEnumerator WaitForThreePleyers()
    {
        while (ServerProxyObject.s == null)
            yield return null;

        yield return new WaitUntil(() => PhotonNetwork.room.playerCount >= 4 );
        Destroy(this.gameObject);
    }
}
