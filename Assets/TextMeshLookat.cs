using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextMeshLookat : MonoBehaviour {

    public TextMesh thisText;
	
	// Update is called once per frame
	void Update () {
        if(thisText != null)
            thisText.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
	}
}
