using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GameBackground : MonoBehaviour {

    public Camera cam;

    private float scale, lastScale;

	// Update is called once per frame
	void Update () {
        scale = cam.orthographicSize * 2;
        if (scale != lastScale)
        {
            transform.localScale = new Vector3(scale * cam.aspect, scale, 1);
            lastScale = scale;
        }
	}
}
