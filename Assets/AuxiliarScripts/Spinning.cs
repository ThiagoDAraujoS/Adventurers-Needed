using UnityEngine;
using System.Collections;

public class Spinning : MonoBehaviour {

    public Vector3 rotation;
    public bool useRandomValues = false;
    public Vector3 rotationMax;
    public AnimationCurve speed;
    private Vector3 aux;
    private float time;
    public float timeScale;
    void Start()
    {
        if (useRandomValues)
            aux = new Vector3(Random.Range(rotation.x, rotationMax.x),
                              Random.Range(rotation.y, rotationMax.y),
                              Random.Range(rotation.z, rotationMax.z));
        else
            aux = rotation;

        time = Time.time;
    }
    void Update()
    { 
        transform.Rotate(aux * (speed.Evaluate((Time.time - time) * timeScale)));
    }

}
