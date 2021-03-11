using UnityEngine;
using System.Collections;

public class Size : MonoBehaviour {

    public float size;
    public AnimationCurve speed;
    private Vector3 aux;
    private float time;
    public float timeScale;

    void Start()
    {
        time = Time.time;
    }
    void Update()
    {
        transform.localScale *= size * speed.Evaluate((Time.time - time) * timeScale);
    }
}


