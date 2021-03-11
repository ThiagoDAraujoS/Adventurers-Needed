using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class IsoHealthBar : MonoBehaviour {

    [Range (1, 2)]
    public float tileOffset = 1.1f;

    public int startHealth = 5;

    [Range(0, 8)]
    public int health = 6;
    private int lastHealth = -1;

    public GameObject healthObject;

	// Use this for initialization
	void Start () {
        health = startHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (health != lastHealth)
        {
            SetBarImages();
            lastHealth = health;
        }

        transform.localRotation = Quaternion.LookRotation(Camera.main.transform.parent.forward);
	}

    void PlayerDied() {
        print("Some player has been killed");
    }

    void SetBarImages() {
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
            healthBar.transform.parent = this.transform;
            healthBar.transform.name = "3D Bar (" + i + ")";
            healthBar.transform.localScale = Vector3.one;
            healthBar.transform.localRotation = Quaternion.identity;
            healthBar.transform.localPosition = new Vector3(i * tileOffset, 0, 0);
        }

        float scalar = (-health * tileOffset)  * transform.localScale.x;
        transform.localPosition = new Vector3(scalar / 2, -health * 0.11f, 0);
    }
}
