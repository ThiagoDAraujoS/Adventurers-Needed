using UnityEngine;
using System.Collections;
using Hell;
using UnityEngine.UI;

public class HealthBarControl : MonoBehaviour
{
    Pawn pawn;
    Image spriteIcon;
    float padding;
	void Start () {
        pawn = transform.parent.GetComponent<Pawn>();
	}
	
	void Update () {
	    
	}

    public void GenerateSprites()
    {
       // float startingPosition = padding * pawn.li
    }
}
