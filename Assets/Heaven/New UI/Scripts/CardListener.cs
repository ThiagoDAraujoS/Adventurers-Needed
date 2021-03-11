using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
//[RequireComponent (typeof(CardValues))]
public class CardListener : MonoBehaviour, IPointerClickHandler {

    private ActionQueue aQ;
    private ContextButtons cB;
    private TopBar tB;

    private CardArea cardArea;

    //Paths to children components
    public Image thumbnail;
    public Text title;
    public Text type;
    public Image aoePreview;
    public Text damage;
    public Text cost;
    public Text description;
    private Image background;

    //[HideInInspector]
    public CardValues cardValues;
    public CharacterValues charValues;

	// Use this for initialization
	void Start () {
        cardValues = GetComponent<CardValues>();
        charValues = GetComponent<CharacterValues>();

        if (cardValues != null && charValues != null)
            Debug.LogWarning("You should not have both character and card values on the same object");

        background = GetComponent<Image>();
        
        cardArea = GetComponentInParent<CardArea>();
        aQ = FindObjectOfType<ActionQueue>();
        cB = FindObjectOfType<ContextButtons>();
        tB = FindObjectOfType<TopBar>();
	}
	
	// Update is called once per frame
	void Update () {
        if(cardValues != null)
            RefreshCardInfo();
        if (charValues != null)
            RefreshCharacterInfo();
	}

    void RefreshCharacterInfo()
    {
        title.text = charValues.title;
        type.text = charValues.charClass.ToString();
        aoePreview.overrideSprite = charValues.thumbnail;
        damage.text = charValues.health.ToString();
        description.text = charValues.description.ToString();

        background.color = charValues.color;
    }

    void RefreshCardInfo()
    {
        thumbnail.overrideSprite = cardValues.thumbnail;
        title.text = cardValues.title;
        type.text = cardValues.type.ToString();
        aoePreview.overrideSprite = cardValues.preview;
        damage.text = cardValues.damage.ToString();
        cost.text = cardValues.cost.ToString();
        description.text = cardValues.description.ToString();
        
        background.color = cardValues.color;
    }

    void AnimateCard()
    {
        if (cardArea == null)
            return;

        if (cardArea.IsSelected)
        {
            cardArea.IsSelected = false;
        }
        else
        {
            aQ.DisableAllCards();
            cardArea.IsSelected = true;
        }     
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //DepricatedFunction();
    }

    void DepricatedFunction() {
        if (!Application.isPlaying || aQ.useDrag)
            return;

        //Check if actionqueue is full
        //if (aQ.actionCount + cardValues.cost > 4)
        //{
        //    tB.UpdateGameBrief("You do not have enough space to use that card.", Color.red);
        //    return;
        //}

        //Check if card is selected
        if (aQ.activeCard == cardValues)
        {
            cB.ShowDirections(false);
            aQ.activeCard = null;
        }
        else
        {
            aQ.activeCard = cardValues;
            cB.ShowDirections(true);
        }

        tB.UpdateGameBrief("Select a direction to use you attack.", cardValues.color);

        AnimateCard();
    }
}
