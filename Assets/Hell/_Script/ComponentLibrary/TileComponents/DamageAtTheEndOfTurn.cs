using UnityEngine;
using System.Collections;
using Hell.Display;
using Hell;
using System;

namespace Hell.Rune
{
    public class DamageAtTheEndOfTurn : MonoBehaviour
    {
        public Tile tile;
        public int damageAmount;
        public GameObject art;
        public GameObject triggerParticle;
        public AnimTrigger animTrigger;
        void OnEnable(){
            RoomManager.s.TurnEngine.Timeline.EndingPlayerAct += Damage;
        }
        void OnDisable(){
            RoomManager.s.TurnEngine.Timeline.EndingPlayerAct -= Damage;
        }
        public void Damage(Character c) {
            if (tile.Pawn == c){
                tile.Pawn.currentLife -= damageAmount;
                if(triggerParticle != null)
                    Destroy(Instantiate(triggerParticle, tile.transform.position, triggerParticle.transform.rotation), 5.0f);
                tile.Pawn.SetAnim(animTrigger);
                AkSoundEngine.PostEvent("Play_Burrn", gameObject);
            }
        }
        public void OnDestroy(){
            Destroy(art);
        }

        public static DamageAtTheEndOfTurn Build(Tile host, int damage, GameObject tileArtPrefab,AnimTrigger animTrigger,GameObject triggerParticle) {
            DamageAtTheEndOfTurn dateot = host.gameObject.AddComponent<DamageAtTheEndOfTurn>();
            dateot.tile = host;
            dateot.damageAmount = damage;
            dateot.art = Instantiate(tileArtPrefab, dateot.transform.position, tileArtPrefab.transform.rotation) as GameObject;
            dateot.art.transform.parent = dateot.transform;
            dateot.animTrigger = animTrigger;
            dateot.triggerParticle = triggerParticle;
            return dateot;
        }
    }
}
