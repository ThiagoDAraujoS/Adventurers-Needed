using UnityEngine;
using System.Collections;

public class CreateGameAudio : MonoBehaviour {

    public AudioClip characterSize;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCharacterSizeSelect()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = characterSize;
        audio.Play();
    }
}
