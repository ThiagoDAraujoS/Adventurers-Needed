using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DirectionInput : MonoBehaviour, IPointerClickHandler
{
    public RectTransform cardPos;
    public FigureMask figMask;
    private ActionQueue aQ;
    public Sprite arrow;
    public Direction arrowDirection;

    private FigureMask fM;
    private Image background;


    void Start()
    {
        aQ = FindObjectOfType<ActionQueue>();
        fM = FindObjectOfType<FigureMask>();
        background = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        aQ.clickedDirection = arrow;
        aQ.inputDirection = arrowDirection;
        fM.facing = arrowDirection;

        foreach (DirectionInput dI in FindObjectsOfType<DirectionInput>())
        {
            dI.DisableButtons();
        }

        background.color = Color.red;

        //aQ.AddActionToQueue();
    }

    public void DisableButtons()
    {
        background.color = Color.grey;
    }
}
