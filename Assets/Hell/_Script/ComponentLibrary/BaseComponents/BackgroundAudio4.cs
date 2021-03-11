using UnityEngine;
using System.Collections;

public class BackgroundAudio4 : MonoBehaviour {

    public string audioStart;
    // Use this for initialization
    void Awake () {
        if (!string.IsNullOrEmpty(audioStart))
        {
            AkSoundEngine.PostEvent(audioStart, gameObject);
            // AkSoundEngine.SetState(L"MistyField", L"MF_2_3");

            StartCoroutine(PlaySound());
        }
    }
    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(2.0f);
        AkSoundEngine.SetState("GoblinKing", "GK_2_4");

    }
}
