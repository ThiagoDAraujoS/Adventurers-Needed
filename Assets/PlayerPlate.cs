using UnityEngine;
using System.Collections;
using Hell;

public class PlayerPlate : MonoBehaviour {

    public Socket myPlayer;
    public string character;

    
    public PlayerPlate(Socket mySocket)
    {
        print("Constructing player plate..");
        myPlayer = mySocket;
    }

    public void Update()
    {
        if (myPlayer != null && myPlayer.PawnInfo.MyPawn != null)
        {
            print("I'm " + myPlayer.CharacterInfo.characterName);
            print("My health is " + myPlayer.PawnInfo.MyPawn.currentLife);
        }
    }

    void OnEnable()
    {
        myPlayer.PawnInfo.OnDamage += UpdateHealth;
    }

    void OnDisable()
    {
        myPlayer.PawnInfo.OnDamage -= UpdateHealth;
    }

    public void UpdateHealth(int newHealth)
    {
        //check if dead too
        print(newHealth + " = my new health.");
        //update their health on the screen
    }

    public void ConstructPlate()
    {

    }

    public void SubmittedActions(int myOrder)
    {
        //show their order on screen
    }
}
