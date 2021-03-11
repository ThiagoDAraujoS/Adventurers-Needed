using UnityEngine;
using System.Collections;
using Hell;

public class TemporaryPawn : MonoBehaviour {

    public Character invoker;
    public float timer;
    public Pawn pawn;
    public GameObject finalParticlePrefab;
    public float destructionDelay;
    public void OnEnable(){
        RoomManager.s.TurnEngine.Timeline.StartingPlayerAct += TimeCounter;
    }

    public void OnDisable(){
        RoomManager.s.TurnEngine.Timeline.StartingPlayerAct -= TimeCounter;
    }

    public void TimeCounter(Character character) {
        if (character == invoker) {
            timer--;
            if (timer <= 0) {
                pawn.Tile = null;
                pawn.currentLife = 0;
            //    Destroy(gameObject, destructionDelay);
                if (finalParticlePrefab != null)
                    Destroy(Instantiate(finalParticlePrefab, transform.position, transform.rotation),5.0f);
            }
        }
    }
}
