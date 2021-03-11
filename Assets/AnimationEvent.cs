using UnityEngine;
using System.Collections;

public class AnimationEvent : MonoBehaviour
{
    public void PrintEvent()
    {
        AkSoundEngine.PostEvent("Play_Movement", gameObject);
    }
}