using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Photon;

public class CharacterSelectHandler : Photon.MonoBehaviour {
    public Image lockScreenImage;

    public void Start()
    {
        lockScreenImage.enabled = false;
    }

    public void SelectCharacter(string characterName)
    {
        int playerID = PhotonNetwork.player.ID - 2;
        Debug.LogError(playerID + " - PLAYER IDENTIFICATION");

        ServerProxyObject.s.photonView.RPC("ChooseCharacter", PhotonTargets.MasterClient, playerID, characterName);
        lockScreenImage.enabled = true;
    }
}