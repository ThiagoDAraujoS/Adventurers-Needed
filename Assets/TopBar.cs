using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopBar : MonoBehaviour {

    public Text briefOutput;
    public Image briefBG;

    public Color flashColor;
    public Color initialColor;
    private bool flash;
    public float flashTime = 0.5f;
    private float timer = 0;
    public int flashCount;

    public GameObject overlay;

    void Awake()
    {
        initialColor = briefBG.color;
        briefOutput.text = "Choose a Card to use and drag it to a tile.";
    }

    void Update()
    {
        timer += Time.deltaTime;

        //if (flash)
        //{
            briefBG.color = Color.Lerp(initialColor, flashColor, Mathf.PingPong(timer, flashTime));
            flash = (timer < flashCount);
        //} 
    }

    public void UpdateGameBrief(string newText, Color inputColor)
    {
        flashColor = inputColor;
        briefOutput.text = newText;

        flash = true;
        timer = 0;
    }

    public void SetDialogOverlay(bool enabled)
    {
        overlay.SetActive(enabled);
    }
}
