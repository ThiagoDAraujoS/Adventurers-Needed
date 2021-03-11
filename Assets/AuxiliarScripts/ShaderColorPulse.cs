using UnityEngine;
using System.Collections;

public class ShaderColorPulse : MonoBehaviour {
	public Projector trackedMaterial;
	public Color color1, color2;
	public float speed;

	void Update() {
		trackedMaterial.material.color = Color.Lerp(color1, color2, Mathf.Sin(Time.time * speed));
	}
}
