using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserAction : MonoBehaviour, IPointerClickHandler {

    private ActionQueue aQ;

    public int id;
    public Direction direction;
    public Image thumbnail;
    public Image directions;
    public bool isVisible = true;

    private LayoutElement layout;
    public int actionIndex;

    public enum CardType
    {
        FIREBALL,
        MOVE,
        WAIT
    }

    public AudioClip undoQueueSound;

    // Use this for initialization
    void Start () {
        directions.color = Color.clear;
        layout = GetComponent<LayoutElement>();
        aQ = FindObjectOfType<ActionQueue>();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (aQ.actionCount >= 1)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = undoQueueSound;
            audio.Play();
            aQ.ClearActionQueue(actionIndex);
        }
    }

    public void ChangeWidth(int scalar)
    {
        layout.minWidth = 280 * scalar;

        isVisible = (scalar == 0);
    }
}
