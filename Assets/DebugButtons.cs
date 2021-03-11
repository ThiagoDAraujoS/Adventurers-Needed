using UnityEngine;
using System.Collections;

public class DebugButtons : MonoBehaviour {

    private ChangeScenes cS;

    void Start()
    {
        cS = FindObjectOfType<ChangeScenes>();
    }

    #if UNITY_EDITOR
    void OnGUI() {

        if (GUI.Button(new Rect(10, 10, 100, 30), "Mobile Join"))
            cS.LoadControllerScene();  

        if (GUI.Button(new Rect(10, 50, 100, 30), "PC Join"))
            cS.LoadGameLevel(0);
    }
    #endif
}
