using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ChangeScenes : MonoBehaviour
{

    public string controllerScene;
    public List<string> levelScene;

	public Animator anim;
    public AudioClip transitionSound;

    private AudioSource audioSource;

    public List<Color> roomColors;

    private string levelToLoad;

    public static ChangeScenes s;

    void Awake()
    {
        #if UNITY_ANDROID
            anim.SetBool("IsClient", true);
        #endif
        #if UNITY_STANDALONE_WIN
            anim.SetBool("IsClient", false);
        #endif

        audioSource = GetComponent<AudioSource>();

        s = this;
    }

	public void LoadGameLevel (int index)
	{
        levelToLoad = levelScene[index]; 

        audioSource.PlayOneShot(transitionSound, 1);

		Invoke ("WaitToLoadLevel", transitionSound.length);
        anim.SetTrigger("FadeOut");
	}

    public void LoadControllerScene()
    {
        levelToLoad = controllerScene;

		Invoke ("WaitToLoadLevel", 1);
        anim.SetTrigger("FadeOut");
	}

	public void WaitToLoadLevel ()
    {
        Application.LoadLevel(levelToLoad);
	}
}
