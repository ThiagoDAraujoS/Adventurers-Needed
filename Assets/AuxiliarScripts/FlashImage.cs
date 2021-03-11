using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlashImage : MonoBehaviour {

    public bool isActive = true;

    public Color color1;
    private Text reference;
    private Color color2;
    public float speed = 1;
    public AnimationCurve curve;

    public bool IsActive
    {
        get { return isActive;}
        set{
            isActive = value;
            if (!isActive)
                reference.color = color2;
        }
    }


    // Use this for initialization
    void Start () {
        reference = GetComponent<Text>();
        color2 = reference.color;
	}
	
	// Update is called once per frame
	void Update () {
        if(IsActive)
            reference.color = Color.Lerp(color2, color1, curve.Evaluate(Mathf.PingPong(Time.time * speed, 1)));
	}
}
