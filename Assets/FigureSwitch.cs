using UnityEngine;
using System.Collections.Generic;

public class FigureSwitch : MonoBehaviour {

    public List<GameObject> figurePrefabs;

    private int count = 0;

	// Use this for initialization
	void Start () {
        ChangeFigure();
	}

    public void SwapFigure()
    {
        GetComponent<Animator>().SetTrigger("Swap");
    }

    public void ChangeFigure()
    {
        GameObject newFigure = Instantiate(figurePrefabs[count]) as GameObject;
        Transform originalFig = transform.FindChild("Figurine");

        newFigure.transform.position = originalFig.position;
        newFigure.transform.localEulerAngles = new Vector3(0, 180, 0);
        newFigure.transform.parent = originalFig;

        for (int i = originalFig.childCount - 1; i > 0 ; i--)
        {
            if (originalFig.GetChild(i) != newFigure.transform && originalFig.GetChild(i).name != "Player Name")
                Destroy(originalFig.GetChild(i).gameObject);
        }

        //Reset new character color
        if (FindObjectOfType<SetColor>() != null)
            FindObjectOfType<SetColor>().ChangeMaterialColor();

        //Update class to send over network
        if(FindObjectOfType<PlayerInput>() != null)
            FindObjectOfType<PlayerInput>().playerInfo.characterType = count;

        count++;
        if (count >= figurePrefabs.Count)
            count = 0;
    }
}
