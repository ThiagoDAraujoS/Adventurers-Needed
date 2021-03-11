using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NoLobby : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogWarning("Launch Web Browser.");
        Application.OpenURL("https://readyup.wordpress.com/");
    }
}
