using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditsManager : MonoBehaviour {
    public string newScene;
    public void ChangeScene()
    {
        SceneManager.LoadScene(newScene);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("PC_Credits");
            //creditsImage.enabled = !creditsImage.enabled;
        }
    }
}
