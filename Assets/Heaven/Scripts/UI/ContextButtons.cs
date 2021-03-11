using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContextButtons : MonoBehaviour {

    private ActionQueue aQ;

    [HideInInspector]
    public Animator anim;

    public GameObject directionButtons;
    public GameObject confirmButtons;

    public Sprite selectedDirection;

    private Color initialColor, targetColor;
    private float lerpTime = 0;
    private Image background;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        background = GetComponent<Image>();
        initialColor = targetColor = background.color;
        aQ = FindObjectOfType<ActionQueue>();
	}

    void Update()
    {
        lerpTime += Time.deltaTime * 2;
        background.color = Color.Lerp(background.color, targetColor, lerpTime);
    }

    public void ShowDirections(bool show)
    {
        anim.SetBool("Directions", show);
        anim.SetTrigger("Activate");

        if (show)
        {
            SetBackgroundColor(aQ.activeCard.color);
        }
        else
        {
            SetBackgroundColor(initialColor);
        }

        lerpTime = 0;
    }

    public void SetBackgroundColor(Color newColor) {
        targetColor = newColor;
        lerpTime = 0;
    }
}
