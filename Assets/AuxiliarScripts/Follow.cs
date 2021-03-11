using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public enum Setting
    {
        nothing,
        position,
        rotation,
        positionAndRotation,
    }

    public Setting setting;

    public Transform target;
    public Transform[] swappableTargets;
    public float
        moveSpeed,
        spinSpeed;


    public void SetTarget(Transform focus)
    {
        target = focus;
    }

    public void Initialize(Setting setting, Transform target, float moveSpeed, float spinSpeed)
    {
        this.setting = setting;
        this.target = target;
        this.moveSpeed = moveSpeed;
        this.spinSpeed = spinSpeed;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        if(((int)setting & (int)Setting.position) == (int)Setting.position)
            transform.position = Vector3.Lerp(transform.position, target.position, moveSpeed);
        else if (((int)setting & (int)Setting.rotation) == (int)Setting.rotation)
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, spinSpeed);
	}
}
