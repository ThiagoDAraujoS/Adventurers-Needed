using UnityEngine;
using System.Collections;
using System;
using Hell.Display;

namespace Hell.Rune
{
    public class SpawnTemporaryPawn : SpawnPawn, IRule<ActionToken>
    {
        public int duration;

        public override void PosSpawnPawn(Pawn pawn) {
            TemporaryPawn comp = pawn.gameObject.AddComponent<TemporaryPawn>();
            comp.pawn = pawn;
            comp.invoker = pawn as Character;
            comp.timer = duration + 1;
            comp.finalParticlePrefab = finalParticlePrefab;
        }
    }
}