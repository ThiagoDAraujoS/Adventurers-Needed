using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Hell;

[RequireComponent (typeof(CardValues))]
public class WaitInput : MonoBehaviour, IPointerClickHandler {

    private ActionQueue aQ;

    [HideInInspector]
    public CardValues values;

	// Use this for initialization
	void Start () {
        values = GetComponent<CardValues>();
        aQ = FindObjectOfType<ActionQueue>();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        aQ.inputDirection = Direction.nothing;
    }
}
