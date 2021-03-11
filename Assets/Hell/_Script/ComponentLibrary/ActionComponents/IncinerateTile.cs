using System.Linq;
using Hell.Display;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Hell.Rune
{
    public class IncinerateTile : TokenDrivenBehaviour<MasterAttack>, IRule<ActionToken>
    {
        public int damage;
        public GameObject artprefab;
        public float delay;

        public GameObject triggerParticle;
        public AnimTrigger animTrigger;
        [HideInInspector]
        public List<DamageAtTheEndOfTurn> fireTiles;

        public void Resolution(ActionToken token){
            StartCoroutine(InstantiateFireTiles());
        }

        IEnumerator InstantiateFireTiles(){
            foreach (DamageAtTheEndOfTurn dateot in fireTiles)
                Destroy(dateot);
            fireTiles = new List<DamageAtTheEndOfTurn>();
            yield return new WaitForSeconds(delay);
            foreach (Tile tile in Master.TargetTiles)
                if (tile.state != Tile.State.solid && !(tile.effects.Any(e => e.tileName == "IceEffect"))) 
                    fireTiles.Add(DamageAtTheEndOfTurn.Build(tile, damage, artprefab, animTrigger, triggerParticle));
        }
    }
}