using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float lerpTime = 0.25f;
    public int yThreshold = 240;
    public int angleRange = 30;

    private Hand h;
    private ActionQueue aQ;
    private TopBar tB;
    private ContextButtons cB;

    private Vector3 initialRot;
    private RectTransform cardRect;
    private Vector2 initialPos, initialMouse;
    private float initialScale;

    [Range (0, 1f)]
    public float minScale;
    private float currentScale = 1;

    public float snapTime;

    private bool canDrag = true;

    public int cardIndex;

    public AudioClip cannotUseSound;
    public AudioClip cardSound;

    public Image ready;
    public Text readyText;

    // Use this for initialization
    void Start () {
        cardRect = GetComponent<RectTransform>();

        h = FindObjectOfType<Hand>();
        aQ = FindObjectOfType<ActionQueue>();
        tB = FindObjectOfType<TopBar>();
        cB = FindObjectOfType<ContextButtons>();

        cardIndex = transform.parent.GetSiblingIndex();

        initialPos = cardRect.anchoredPosition;
        initialScale = cardRect.localScale.x;
        initialRot = cardRect.eulerAngles;
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        //initialPos = cardRect.anchoredPosition;
        initialMouse = Input.mousePosition;

        aQ.activeCard = GetComponent<CardValues>();
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = cardSound;
        audio.Play();
        if (aQ.actionCount + aQ.activeCard.cost > 4)
        {
            tB.UpdateGameBrief("You do not have enough space to use that card.", Color.red);
            return;
        }

        //cB.SetBackgroundColor(aQ.activeCard.color);

        tB.UpdateGameBrief("Select a direction to use your attack.", aQ.activeCard.color);
    }

    public void OnDrag(PointerEventData eventData)
    {
        DirectionAtMouse();

        //h.ChangeSiblingPosition(cardIndex, cardRect.anchoredPosition.x);

        cardRect.localScale = Vector3.one * (initialScale + 0.1f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Move to original position => cardRect.anchoredPosition = initialPos;

        //Lerp to original position
        DOTween.To(() => cardRect.anchoredPosition, x => cardRect.anchoredPosition = x, initialPos, snapTime);
        DOTween.To(() => cardRect.localScale, x => cardRect.localScale = x, Vector3.one * initialScale, snapTime);


        if (aQ.actionCount + aQ.activeCard.cost > 4)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = cannotUseSound;
            audio.Play();
            if (aQ.actionCount >= 4)
            {
                ready = ready.GetComponent<Image>();
                Color c = ready.color;
                c.a = 100;
                ready.color = c;
                readyText.text = "Enter Actions";
            }
            tB.UpdateGameBrief("You do not have enough space to use that card.", Color.red);
        }
        else if (aQ.mousedOver)
        {

            aQ.AddActionToQueue();
        }

        currentScale = initialScale;

        transform.DORotate(initialRot, lerpTime, RotateMode.Fast);

        tB.UpdateGameBrief("Choose a Card to use and drag it to a tile.", Color.black);

    }

    void DirectionAtMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition), Vector2.zero);

        aQ.mousedOver = false;

        if (hit && hit.collider.GetComponent<DirectionInput>() != null)
        {
            DirectionInput dirIn = hit.collider.GetComponent<DirectionInput>();

            dirIn.figMask.ChangeDirection(dirIn.arrowDirection);

            aQ.inputDirection = dirIn.arrowDirection;
            aQ.clickedDirection = dirIn.arrow;
            aQ.mousedOver = true;

            //Snap to tiles
            cardRect.DOMove(dirIn.cardPos.position, snapTime);
            //DOTween.To(() => cardRect.localScale, x => cardRect.localScale = x, Vector3.one * initialScale, snapTime);

            //Rotate
            transform.rotation = Quaternion.Lerp(transform.rotation, dirIn.cardPos.rotation, 25 * Time.deltaTime);
            //transform.DORotate(Quaternion.ToEulerAngles(dirIn.cardPos.rotation), lerpTime, RotateMode.FastBeyond360);
            
        }
        else if(!hit)
        {
            aQ.ClearActionQueue(aQ.actionCount);
            transform.DORotate(initialRot, lerpTime, RotateMode.Fast);
            DOTween.To(() => cardRect.localScale, x => cardRect.localScale = x, Vector3.one, snapTime);

            //Follow Cursor
            cardRect.anchoredPosition = ((Vector2)Input.mousePosition - initialMouse) * CanvasScaleFix.s.scale + initialPos;
            DOTween.To(() => cardRect.anchoredPosition, x => cardRect.anchoredPosition = x, initialPos + new Vector2(ClampBasedOnAngle(), yThreshold), snapTime);

            //ClampBasedOnAngle();
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
