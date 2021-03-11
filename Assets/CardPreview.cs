using UnityEngine;
using System.Collections;

public class CardPreview : MonoBehaviour {

    private Animator anim;
    private bool isSelected;
    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;
            anim.SetBool("onScreen", isSelected);
        }
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwitchCardValues(int index)
    {
        print(index + "called");
    }
}
