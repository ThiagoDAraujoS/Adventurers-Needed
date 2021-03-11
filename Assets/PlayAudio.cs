using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayAudio : MonoBehaviour {

    public AudioClip arrowAudio;
    public AudioClip pepperpinchAudio;
    public AudioClip mcleranAudio;
    public AudioClip freyjaAudio;
    public AudioClip finchAudio;
    public AudioClip glyAudio;
    public AudioClip ryuichiAudio;
    public AudioClip yamichiAudio;
    public AudioClip carcinusCrabAudio;
    public AudioClip lionidasAudio;
    public AudioClip charSelectAudio;

    public AudioClip pepperpinch;
    public AudioClip mcleran;
    public AudioClip freyja;
    public AudioClip finch;
    public AudioClip gly;
    public AudioClip ryuichi;
    public AudioClip yamichi;
    public AudioClip carcinusCrab;
    public AudioClip lionidas;
    private Text charText;

    public void OnCharSelectArrow()
    {
        AudioSource audioComp = GetComponent<AudioSource>();
        audioComp.clip = arrowAudio;
        audioComp.Play();
    }

    public void OnSelectCharacter()
    {
        AudioSource audioComp = GetComponent<AudioSource>();
        audioComp.clip = charSelectAudio;
        audioComp.Play();
    }

    public void PlayName()
    {
        foreach (Transform t in transform)
        {
            if (t.name == "Image")
            {
                charText = GetComponentInChildren<Text>();
                AudioSource charAudio = GetComponent<AudioSource>();
                switch (charText.text)
                {
                    case "Yamichi":
                        charAudio.clip = yamichi;
                        charAudio.Play();
                        break;
                    case "Pepperpinch":
                        charAudio.clip = pepperpinch;
                        charAudio.Play();
                        break;
                    case "McLeran":
                        charAudio.clip = mcleran;
                        charAudio.Play();
                        break;
                    case "Freyja":
                        charAudio.clip = freyja;
                        charAudio.Play();
                        break;
                    case "Finch":
                        charAudio.clip = finch;
                        charAudio.Play();
                        break;
                    case "Gly":
                        charAudio.clip = gly;
                        charAudio.Play();
                        break;
                    case "Ryuuichi":
                        charAudio.clip = ryuichi;
                        charAudio.Play();
                        break;
                    case "Carcinus Crab":
                        charAudio.clip = carcinusCrab;
                        charAudio.Play();
                        break;
                    case "Lionidas":
                        charAudio.clip = lionidas;
                        charAudio.Play();
                        break;
                }
            }
        }
    }


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
                        audioComp.clip = yamichiAudio;
                        audioComp.Play();
                        break;
                    case "Pepperpinch":
                        audioComp.clip = pepperpinchAudio;
                        audioComp.Play();
                        break;
                    case "McLeran":
                        audioComp.clip = mcleranAudio;
                        audioComp.Play();
                        break;
                    case "Freyja":
                        audioComp.clip = freyjaAudio;
                        audioComp.Play();
                        break;
                    case "Finch":
                        audioComp.clip = finchAudio;
                        audioComp.Play();
                        break;
                    case "Gly":
                        audioComp.clip = glyAudio;
                        audioComp.Play();
                        break;
                    case "Ryuuichi":
                        audioComp.clip = ryuichiAudio;
                        audioComp.Play();
                        break;
                    case "Carcinus Crab":
                        audioComp.clip = carcinusCrabAudio;
                        audioComp.Play();
                        break;
                    case "Lionidas":
                        audioComp.clip = lionidasAudio;
                        audioComp.Play();
                        break;
                }
            }
        }
    }
}
