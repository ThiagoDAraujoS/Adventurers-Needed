using UnityEngine;
using System.Collections;

public class BackgroundAudio3 : MonoBehaviour {

    public string audioStart;
    // Use this for initialization
    void Awake () {
        if (!string.IsNullOrEmpty(audioStart))
        {
            AkSoundEngine.PostEvent(audioStart, gameObject);
            AkSoundEngine.SetState("MistyField", "MF_1");
            // AkSoundEngine.SetState(L"MistyField", L"MF_2_3");

            StartCoroutine(PlaySound());
        }
    }
    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(2.0f);
        AkSoundEngine.SetState("MistyField", "MF_2_3");

    }
}
