using UnityEngine;
using System.Collections;

public class CanvasScaleFix : MonoBehaviour {

    public static CanvasScaleFix s;
    public float scale;

    public float canvasWidth;

    void Awake()
    {
        s = this;
        scale = canvasWidth / (float)Screen.width;
    }
	
	void Update ()
    {
    #if UNITY_EDITOR
        scale = canvasWidth / Screen.width;
    #endif
    }
}
