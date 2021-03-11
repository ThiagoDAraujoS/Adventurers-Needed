using UnityEngine;

public class Tranlate : MonoBehaviour {

    public Vector3 distance;
    public AnimationCurve speed;
    private Vector3 aux;
    private float time;
    public float timeScale;
	void Start () {
        time = Time.time;
	}
	void Update () {
        transform.Translate(distance * (speed.Evaluate((Time.time-time) * timeScale)));
	}
}
