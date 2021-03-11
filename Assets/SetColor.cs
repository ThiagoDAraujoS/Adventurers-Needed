using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SetColor : MonoBehaviour {

    public List<Color> mainColor;
    public List<Color> accentColor;

    private PlayerInput pI;

    private Image background;
    private Text accentText;

    public int colorIndex = 0;

    public ParticleSystem sprayPaint;
    public Transform figureParent;

    private bool secondPass;

	// Use this for initialization
	void Awake () {
        pI = FindObjectOfType<PlayerInput>();
        background = GetComponent<Image>();
        accentText = GetComponentInChildren<Text>();
	}

    public void ChangeBackground()
    {
        colorIndex++;

        if (colorIndex >= mainColor.Count)
            colorIndex = 0;

        background.color = mainColor[colorIndex];
    }

    public void ChangeColorPair()
    {
        colorIndex++;

        if (colorIndex >= mainColor.Count)
            colorIndex = 0;

        background.color = mainColor[colorIndex];
        accentText.color = accentColor[colorIndex];

        //Update class to send over network
        FindObjectOfType<PlayerInput>().playerInfo.color1 = mainColor[colorIndex];
        FindObjectOfType<PlayerInput>().playerInfo.color1 = accentColor[colorIndex];

        SprayFigure();

        ChangeMaterialColor();

        secondPass = true;
        Invoke("SprayFigure", 0.25f);
        
    }

    public void ChangeMaterialColor()
    {
        FindObjectOfType<FigurineHandler>().ChangeFigureColor(0, mainColor[colorIndex], accentColor[colorIndex]);
    }

    private void SprayFigure() {
        ParticleSystem ps = Instantiate(sprayPaint) as ParticleSystem;
        ps.transform.parent = figureParent;
        ps.transform.localPosition = new Vector3(0, 3, 0);

        if(!secondPass)
            ps.startColor = mainColor[colorIndex];
        else
            ps.startColor = accentColor[colorIndex];

        secondPass = false;

        Destroy(ps.gameObject, 1.0f);
    }
}
