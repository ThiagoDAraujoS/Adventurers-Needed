using UnityEngine;
using System.Collections;

public class CharacterSelectTracker : MonoBehaviour {

    public Transform target;
    public float speed;
    public GameObject playerBase;
    void Update() {
        if (target != null) {
            if (Vector3.Distance(transform.position, target.position) > 0.1f)
                transform.position = Vector3.Lerp(transform.position, target.position, speed);
            if (Vector3.Distance(transform.localScale, target.localScale) > 0.01f)
                transform.localScale = Vector3.Lerp(transform.localScale, target.localScale, speed);
        }
    }
}
