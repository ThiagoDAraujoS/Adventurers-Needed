using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NameInput : MonoBehaviour {

    public string inputText = "Username";
    private TouchScreenKeyboard keyboard;
    public Text outputText;

    public int maxLength = 8;

    void Update()
    {
        if (keyboard != null && keyboard.done)
        {
            outputText.text = inputText;
        }
    }

    public IEnumerator KeyboardListener()
    {
        outputText.text = "";

        keyboard = TouchScreenKeyboard.Open(inputText, TouchScreenKeyboardType.Default);

        if (keyboard != null)
        {
            yield return new WaitUntil(() => { return keyboard.done; });
            inputText = keyboard.text;

            if (inputText.Length > maxLength)
                inputText = inputText.Substring(0, maxLength) + "...";
            else if (inputText == "")
                inputText = "Username";

            //Update class to send over network
            FindObjectOfType<PlayerInput>().playerInfo.playerName = inputText;
        }
    }

    public void OpenKeyboard()
    {
        StartCoroutine(KeyboardListener());
    }
}
