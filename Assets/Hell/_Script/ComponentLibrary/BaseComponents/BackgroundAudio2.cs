using UnityEngine;
using System.Collections;

public class BackgroundAudio2 : MonoBehaviour {

    public string audioStart;
    // Use this for initialization
    void Awake () {
        if (!string.IsNullOrEmpty(audioStart))
        {
            AkSoundEngine.PostEvent(audioStart, gameObject);
        }
    }
}
