using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FlatHealthBar : MonoBehaviour {

    public int startHealth = 10;

    [Range(0, 10)]
    public int health;
    private int lastHealth = -1;

    public GameObject healthObject;

    // Use this for initialization
    void Start()
    {
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health != lastHealth)
        {
            SetBarImages();
            lastHealth = health;
        }
    }

    void PlayerDied()
    {
        print("Some player has been killed");
    }

    void SetBarImages()
    {
        //Destroy old images
        int children = transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }

        //render new ones
        for (int i = 0; i < health; i++)
        {
            GameObject healthBar = Instantiate(healthObject, Vector3.zero, Quaternion.identity) as GameObject;
            healthBar.transform.SetParent(this.transform, false);
            healthBar.transform.name = "2D Bar (" + i + ")";
        }

        foreach (UIHealth uiHealth in GetComponentsInChildren<UIHealth>())
        {
            uiHealth.ChangeColor((float)health / (float)startHealth);
        }
    }
}
