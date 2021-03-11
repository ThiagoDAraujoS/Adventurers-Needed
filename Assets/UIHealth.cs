using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour {

    public Gradient healthColor;

    public LayoutElement layout;
    public Image barSprite;

    public void ChangeColor(float normal)
    {
        barSprite.color = healthColor.Evaluate(normal);
    }
}
