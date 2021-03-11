using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerRuler : MonoBehaviour {
    public PcRoomCreationManager prcm;
    public int targetPlayer;
    public Transform needleFocus;
    public Follow needle;
	void OnMouseOver()
    {
        Debug.Log("Bla");
        if(Input.GetMouseButton(0))
        {
            prcm.SetExpectedPlayers(targetPlayer);
            needle.target = needleFocus;
        }
    }
}
