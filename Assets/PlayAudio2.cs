using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayAudio2 : MonoBehaviour {

    public AudioClip pepperpinchselectAudio;
    public AudioClip mcleranselectAudio;
    public AudioClip freyjaselectAudio;
    public AudioClip finchselectAudio;
    public AudioClip glyselectAudio;
    public AudioClip ryuichiselectAudio;
    public AudioClip yamichiselectAudio;
    public AudioClip carcinusCrabselectAudio;
    public AudioClip lionidasselectAudio;
    private Text charText;

    public void OnCharacterName()
    {
        foreach (Transform t in transform)
        {
            if (t.name == "Image")
            {
                charText = GetComponentInChildren<Text>();
                AudioSource audioComp = GetComponent<AudioSource>();
                switch (charText.text)
                {
                    case "Yamichi":
                        audioComp.clip = yamichiselectAudio;
                        audioComp.Play();
                        break;
                    case "Pepperpinch":
                        audioComp.clip = pepperpinchselectAudio;
                        audioComp.Play();
                        break;
                    case "McLeran":
                        audioComp.clip = mcleranselectAudio;
                        audioComp.Play();
                        break;
                    case "Freyja":
                        audioComp.clip = freyjaselectAudio;
                        audioComp.Play();
                        break;
                    case "Finch":
                        audioComp.clip = finchselectAudio;
                        audioComp.Play();
                        break;
                    case "Gly":
                        audioComp.clip = glyselectAudio;
                        audioComp.Play();
                        break;
                    case "Ryuuichi":
                        audioComp.clip = ryuichiselectAudio;
                        audioComp.Play();
                        break;
                    case "Carcinus Crab":
                        audioComp.clip = carcinusCrabselectAudio;
                        audioComp.Play();
                        break;
                    case "Lionidas":
                        audioComp.clip = lionidasselectAudio;
                        audioComp.Play();
                        break;
                }
            }
        }
    }
}
