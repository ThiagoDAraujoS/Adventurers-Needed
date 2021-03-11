using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuneThumbnail : MonoBehaviour {

    public Sprite runeThumb;

	// Use this for initialization
	void Awake () {
        runeThumb = gameObject.GetComponent<Image>().sprite;
	}
}
