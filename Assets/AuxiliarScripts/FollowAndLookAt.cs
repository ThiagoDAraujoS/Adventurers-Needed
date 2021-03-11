using UnityEngine;
using System.Collections;

public class FollowAndLookAt : MonoBehaviour
{

    public Transform target;
    public float
        moveSpeed,
        spinSpeed;


    public void SetTarget(Transform focus)
    {
        target = focus;
    }

    public void Initialize(Transform target, float moveSpeed, float spinSpeed)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
        this.spinSpeed = spinSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, spinSpeed);
    }
}
