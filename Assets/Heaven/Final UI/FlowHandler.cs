using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FlowHandler : MonoBehaviour {

    public Text overlayText;

    private List<Transform> MenuChildren;
    private Animator anim;

	// Use this for initialization
	void Awake () {
        MenuChildren = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
		{
            MenuChildren.Add(transform.GetChild(i));
		}
        anim = GetComponent<Animator>();
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Skip Intro", true);
        }
    }

    public void DisableChildren()
    {
        overlayText.text = "";
        foreach (Transform t in MenuChildren)
        {
            if(t.gameObject.activeSelf)
                t.gameObject.SetActive(false);
        }
    }

    public void SetOverlayText(string newText)
    {
        overlayText.text = newText;
    }

    public void LoadSceneByString(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
