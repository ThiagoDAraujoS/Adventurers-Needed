using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hand : MonoBehaviour {

    private RectTransform handArea;

    private int offset;
    public float handWidth;

    public List<RectTransform> siblingRects;

    void Awake()
    {
        handArea = GetComponent<RectTransform>();
        handWidth = handArea.rect.width;

        foreach (CardValues cV in GetComponentsInChildren<CardValues>())
        {
            siblingRects.Add(cV.GetComponent<RectTransform>());
        }
        //siblingRects.OrderBy(o => o.GetComponent<Transform>().parent.GetSiblingIndex());

        offset = Mathf.RoundToInt( handWidth / siblingRects.Count * 2 );
    }

    public void ChangeSiblingPosition(int index, float xPosition)
    {

    }
}
