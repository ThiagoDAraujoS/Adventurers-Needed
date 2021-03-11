using UnityEngine;
using System.Collections;

public class BackgroundAudio5 : MonoBehaviour {

    public string audioStart;
    // Use this for initialization
    void Awake () {
        if (!string.IsNullOrEmpty(audioStart))
        {
            AkSoundEngine.PostEvent(audioStart, gameObject);
            new WaitForSeconds(4);
            AkSoundEngine.SetState("GoblinKing", "GK_2_4");
        }
    }
}
