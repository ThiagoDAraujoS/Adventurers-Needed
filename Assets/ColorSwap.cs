using UnityEngine;
using System.Collections;

public class ColorSwap : MonoBehaviour
{
	public Color[] color;

	// Use this for initialization
	void Start ()
	{
		color [0] = Random.ColorHSV ();
		color [1] = Random.ColorHSV ();

		foreach (var item in GetComponentsInChildren<Renderer>()) {
			item.material.SetColor ("_Color1", color [0]);
			item.material.SetColor ("_Color2", color [1]);
		}
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
