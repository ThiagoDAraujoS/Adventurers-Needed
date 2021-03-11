using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class ColorByRoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Image>().color = ChangeScenes.s.roomColors[0];
	}
}
