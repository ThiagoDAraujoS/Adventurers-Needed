using UnityEngine;
using System.Collections;

public class CardArea : MonoBehaviour {

    [SerializeField]
    private int siblingIndex;
    private Animator anim;
    private bool isSelected;
    public bool IsSelected {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;
            anim.SetBool("isSelected", isSelected);
        }
    }

	// Use this for initialization
	void Start () {
        siblingIndex = transform.GetSiblingIndex();
        anim = GetComponent<Animator>();
	}

    public void SetCardDepth(int index)
    {
        if (index > 0)
        {
            transform.SetAsLastSibling();
        }
        else
        {
            transform.SetSiblingIndex(siblingIndex);
        }
    }
}
