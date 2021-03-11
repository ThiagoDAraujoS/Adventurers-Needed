using UnityEngine;
using System.Collections;
using System;
using Hell.Display;

namespace Hell.Rune
{
    public class SpawnUniquePawn : SpawnPawn, IRule<ActionToken>
    {
        Pawn pawnReference;
        public override void PreSpawnPawn(){
            if (pawnReference != null)
            {
                Destroy(Instantiate(pawnReference.deathParticlePrefab, pawnReference.transform.position, pawnReference.transform.rotation), 5.0f);
                Destroy(pawnReference.gameObject);
            }
        }
        public override void PosSpawnPawn(Pawn pawn) {

            pawnReference = pawn;
        }
    }
}