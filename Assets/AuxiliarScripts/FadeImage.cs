using UnityEngine;
using System.Collections;
using Hell;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour {
    public bool fadeIn;
    public float duration;
	void Start()
    {
        Image image = GetComponent<Image>();
        Color target = new Color(image.color.r, image.color.g, image.color.b, (fadeIn) ? 0 : 1);
        StartCoroutine(Stopwatch.PlayUntilReady(duration, t => image.color = Color.Lerp(image.color, target, t)));
	}
}
