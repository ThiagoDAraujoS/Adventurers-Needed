using UnityEngine;
using System.Collections;
using Hell;
using DG.Tweening;

//[ExecuteInEditMode]
public class FigureMask : MonoBehaviour {

    public Transform templerTrans;
    public Direction facing;
    private Direction lastFacing = Direction.nothing;



    public int offset = 0;

    void Start()
    {
        facing = Direction.west;
    }

	// Update is called once per frame
	void Update () {
	    if(facing != lastFacing) {
            lastFacing = facing;
            ChangeDirection(facing);
        }
	}

    public void ChangeDirection(Direction newDir)
    {
        float newAngle = 0;


        switch (newDir)
        {
            case Direction.north:
                newAngle = 0 + offset;
                break;
            case Direction.east:
                newAngle = 90 + offset;
                break;
            case Direction.south:
                newAngle = 180f + offset;
                break;
            case Direction.west:
                newAngle = 270f + offset;
                break;
        }      
          
        //templerTrans.rotation = Quaternion.Euler(new Vector3(0, newAngle, 0));

        Quaternion lerpRot = Quaternion.Euler(new Vector3(0, newAngle, 0));

        templerTrans.DOLocalRotateQuaternion(lerpRot, 0.25f);
    }
}
