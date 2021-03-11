using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActionQueue : MonoBehaviour
{
    //Use to toggle between modes.
    public bool useDrag = true;

    private bool highlighted = false;
    public bool mousedOver
    {
        get
        {
            return highlighted;
        }
        set
        {
            if (highlighted != value)
                HighlightActions();

            highlighted = value;
        }
    }

    public float hoverScale = 1.2f;

    public PlanNEW plan;

    public static int PLAYER_ID;

    public CardValues activeCard;

    public Sprite emptyAction;

    [HideInInspector]
    public Sprite clickedDirection;

    [HideInInspector]
    public List<UserAction> actions;

    public Direction inputDirection;

    public int actionCount;

    public GameObject readyButton;
    public AudioClip fullActionQueue;
    public AudioClip newAction;

    private Image ready;
    public Text readyText;

    public static bool canSendActions;

    // private ContextButtons cB;

    //Thiago's cde to result AQ Bug
    public int Index(int index)
    {
        int result = 0;

        int i = 0;

        while (result < index)
        {
            result += plan.actions[i].Cost;
            i++;
        }

        return i;
    }

    void Start()
    {
        plan = new PlanNEW(PLAYER_ID);

        int count = actionCount = 0;

        foreach (UserAction aD in GetComponentsInChildren<UserAction>())
        {
            actions.Add(aD);
            actions[count].actionIndex = count;
            count++;
        }
        // cB = FindObjectOfType<ContextButtons>();
    }

    private void HighlightActions()
    {
        if (activeCard == null)
            return;

        PlaceImageInAction(actionCount);

        //if (highlighted)
        //{
        //    ResetActionSizes();
        //}
        //else if (actionCount + activeCard.cost <= 4)
        //{
        //    for (int i = 0; i < activeCard.cost; i++)
        //    {
        //        actions[i + actionCount].transform.localScale = Vector3.one * hoverScale;
        //        actions[i + actionCount].GetComponent<Image>().color = activeCard.color;
        //    }
        //}
        //else
        //{
        //    for (int i = actionCount; i < 4; i++)
        //    {
        //        actions[i].transform.localScale = Vector3.one * (1 / hoverScale);
        //        actions[i].GetComponent<Image>().color = Color.red;
        //    }
        //}
    }

    private void ResetActionSizes()
    {
        foreach (UserAction uA in actions)
        {
            uA.transform.localScale = Vector3.one;
            uA.GetComponent<Image>().color = Color.white;
        }
    }

    public void AddActionToQueue()
    {
        //if (actionCount + activeCard.cost == 4)
        //{
        //    DisableAllCards();
        //}

        plan.AddAction(activeCard.id, inputDirection, activeCard.cost);

        PlaceImageInAction(actionCount);

        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = newAction;
        audio.Play();
        actionCount += activeCard.cost;
        Debug.Log(actionCount);
        if (actionCount >= 4)
        {
            canSendActions = true;
            audio.clip = fullActionQueue;
            audio.Play();
            ready = readyButton.GetComponent<Image>();
            Color c = ready.color;
            c.a = 100;
            ready.color = c;
            readyText.text = "Submit Actions";
            //readyButton.SetActive(true);
        }
    }

    private void PlaceImageInAction(int newCount)
    {
        if (newCount + activeCard.cost > 4)
        {
            canSendActions = true;
            return;
        }

        for (int i = 0; i < activeCard.cost; i++)
        {
            if (i == 0)
            {
                actions[newCount].thumbnail.overrideSprite = activeCard.thumbnail;
                actions[newCount].directions.overrideSprite = clickedDirection;
                actions[newCount].directions.color = activeCard.color;
                actions[newCount].directions.color = Color.red;
                actions[newCount].ChangeWidth(activeCard.cost);
            }
            else
            {
                //Remove from the end
                actions[newCount].ChangeWidth(0);
            }

            newCount++;
        }

        //Return card to original state after using a wait
        if (activeCard.title == "Wait")
        {
            actions[newCount - 1].directions.color = Color.clear;
        }
    }


    public void ClearActionQueue(int index)
    {
        if (index > actionCount)
            return;

        actionCount = index;

        for (int i = 3; i >= index; i--)
        {
            actions[i].thumbnail.overrideSprite = emptyAction;
            actions[i].directions.color = Color.clear;
            actions[i].ChangeWidth(1);
        }

        for (int i = plan.actions.Count - 1; i >= Index(index); i--)
        {
            plan.actions.RemoveAt(i);
        }
        canSendActions = false;
        ready = readyButton.GetComponent<Image>();
        Color c = ready.color;
        c.a = 0;
        ready.color = c;
        readyText.text = "";
        //cB.anim.SetBool("ListFull", false);

    }

    public void DisableAllCards()
    {
        foreach (CardArea cArea in FindObjectsOfType<CardArea>())
        {
            if (cArea != null && cArea.IsSelected)
                cArea.IsSelected = false;
        }
    }
}
