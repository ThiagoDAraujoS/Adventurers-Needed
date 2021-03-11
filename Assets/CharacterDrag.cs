using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;
using DG.Tweening;

public class CharacterDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public float lerpTime = 0.25f;
    public int yThreshold = 240;
    public int angleRange = 30;

    private Hand h;
    private TopBar tB;
    private CardPreview[] cP;

    private Vector3 initialRot;
    private RectTransform cardRect;
    private Vector2 initialPos, initialMouse;
    private float initialScale;

    [Range(0, 1f)]
    public float minScale;
    private float currentScale = 1;

    public float snapTime;

    private bool canDrag = true;

    public int cardIndex;

    // Use this for initialization
    void Start()
    {
        cardRect = GetComponent<RectTransform>();

        cP = FindObjectsOfType<CardPreview>();
        tB = FindObjectOfType<TopBar>();

        cardIndex = transform.parent.GetSiblingIndex();

        initialPos = cardRect.anchoredPosition;
        initialScale = cardRect.localScale.x;
        initialRot = cardRect.eulerAngles;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //initialPos = cardRect.anchoredPosition;
        initialMouse = Input.mousePosition;

        //cB.SetBackgroundColor(aQ.activeCard.color);

        tB.UpdateGameBrief("Select a team to join.", Color.magenta);
        cP[0].IsSelected = true;
        cP[1].IsSelected = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        DirectionAtMouse();

        //h.ChangeSiblingPosition(cardIndex, cardRect.anchoredPosition.x);

        cardRect.localScale = Vector3.one * (initialScale + 0.1f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("called");
        //Move to original position => cardRect.anchoredPosition = initialPos;

        //Lerp to original position
        DOTween.To(() => cardRect.anchoredPosition, x => cardRect.anchoredPosition = x, initialPos, snapTime);
        DOTween.To(() => cardRect.localScale, x => cardRect.localScale = x, Vector3.one * initialScale, snapTime);

        currentScale = initialScale;

        transform.DORotate(initialRot, lerpTime, RotateMode.Fast);

        tB.UpdateGameBrief("Select a direction to use you attack.", tB.initialColor);
        cP[0].IsSelected = false;
        cP[1].IsSelected = false;
    }

    void DirectionAtMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition), Vector2.zero);

        if (hit && hit.collider.GetComponent<DirectionInput>() != null)
        {
            DirectionInput dirIn = hit.collider.GetComponent<DirectionInput>();

            dirIn.figMask.ChangeDirection(dirIn.arrowDirection);

            //Snap to tiles
            cardRect.DOMove(dirIn.cardPos.position, snapTime);
            //DOTween.To(() => cardRect.localScale, x => cardRect.localScale = x, Vector3.one * initialScale, snapTime);

            //Rotate
            transform.rotation = Quaternion.Lerp(transform.rotation, dirIn.cardPos.rotation, 25 * Time.deltaTime);
            //transform.DORotate(Quaternion.ToEulerAngles(dirIn.cardPos.rotation), lerpTime, RotateMode.FastBeyond360);

        }
        else if (!hit)
        {
            transform.DORotate(initialRot, lerpTime, RotateMode.Fast);
            DOTween.To(() => cardRect.localScale, x => cardRect.localScale = x, Vector3.one, snapTime);

            //Follow Cursor
            cardRect.anchoredPosition = ((Vector2)Input.mousePosition - initialMouse) * CanvasScaleFix.s.scale + initialPos;
            DOTween.To(() => cardRect.anchoredPosition, x => cardRect.anchoredPosition = x, initialPos + new Vector2(ClampBasedOnAngle(), yThreshold), snapTime);
        }
    }

    private float ClampBasedOnAngle()
    {
        float degrees = Vector2.Angle(cardRect.anchoredPosition, initialPos);

        float xPos = cardRect.anchoredPosition.y / Mathf.Tan(angleRange);

        //print(cardRect.anchoredPosition.y + ", " + xPos);

        if (degrees > angleRange)
            cardRect.anchoredPosition = new Vector2(Mathf.Clamp(cardRect.anchoredPosition.x, -xPos, xPos), cardRect.anchoredPosition.y);

        return Mathf.Clamp(cardRect.anchoredPosition.x, -angleRange, angleRange);
    }
}
