using UnityEngine;
using System.Collections.Generic;

public class FigurineHandler : MonoBehaviour {

    public Animator[] figurines;

    private bool isTablet;

	// Use this for initialization
	void Start () {
        figurines = GetComponentsInChildren<Animator>();

        #if UNITY_ANDROID
            isTablet = true;
        #endif

        int count = 0;

        foreach (Animator a in figurines)
        {
            ShowByPlatform(count, isTablet);
            count++;
        }
    }

    private void ShowByPlatform(int index, bool shown)
    {
        figurines[index].SetBool("isShown", shown);
    }

    public void ShowFigures(int index)
    {
        figurines[index].SetBool("isShown", true);
    }

    public void HideFigures(int index)
    {
        figurines[index].SetBool("isShown", false);
    }
    Color target1;
    Color target2;
    int lerpIndex;

    public Shader colorSwap;

    void Update()
    {
        //TODO possibly lerp later
        foreach (var item in figurines[lerpIndex].GetComponentsInChildren<Renderer>())
        {
            if (item.material.shader == colorSwap)
            {
                item.material.SetColor("_Color1", Color.Lerp(item.material.GetColor("_Color1"), target1, 1 * Time.deltaTime));
                item.material.SetColor("_Color2", Color.Lerp(item.material.GetColor("_Color2"), target2, 1 * Time.deltaTime));
            }
        }
    }
    public void ChangeFigureColor(int index, Color color1, Color color2)
    {
        lerpIndex = index;
        target1 = color1;
        target2 = color2;
        /*
        //TODO possibly lerp later
        foreach (var item in figurines[index].GetComponentsInChildren<Renderer>())
        {
            item.material.SetColor("_Color1", color1);
            item.material.SetColor("_Color2", color2);
        }*/
    }
}
