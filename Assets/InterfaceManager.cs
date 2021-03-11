using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InterfaceManager : MonoBehaviour {
    public static InterfaceManager s;
    public Image lockScreenImage;
    public Image toolTip;
    public Text toolTipText;
    public Text turnOrderText;
    public Image cardToolTip;
    public Image deathImage;
    public GameObject figureSpawner;
    public AudioClip FirstAudio;

    public void Awake()
    {
        s = this;
        UnlockInterface();
        LockSolelyInterface();
    }

    public void LockSolelyInterface()
    {
        cardToolTip.enabled = true;
        figureSpawner.SetActive(false);
        lockScreenImage.enabled = false;
        deathImage.enabled = false;
    }

    public void UnlockInterface()
    {
        cardToolTip.enabled = false;
        figureSpawner.SetActive(true);
        lockScreenImage.enabled = false;
        if (toolTipText && toolTip)
        {
            toolTip.enabled = false;
            toolTipText.enabled = false;
        }
        if(turnOrderText)
            turnOrderText.enabled = false;
    }

    public void LockInterface()
    {
        figureSpawner.SetActive(false);
        //cardToolTip.enabled = true;
        lockScreenImage.enabled = true;
    }

    public void DeadScreen()
    {
        figureSpawner.SetActive(false);
        deathImage.enabled = true;
    }

    public void ShowToolTip()
    {
        toolTip.enabled = true;
        toolTipText.enabled = true;
    }

    public void DisplayTurnOrder(int myOrder)
    {
        turnOrderText.enabled = true;
        turnOrderText.text = myOrder.ToString();
        if(myOrder.ToString() == "1")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = FirstAudio;
            audio.Play();
        }
    }
}
