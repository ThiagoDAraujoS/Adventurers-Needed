using UnityEngine;
using System.Collections;
using System;
using Hell.Display;

namespace Hell.Rune
{
    public class SpawnPawn : TokenDrivenBehaviour<MasterAttack>, IRule<ActionToken>
    {
        public GameObject pawnPrefab;
        public AnimTrigger birthAnimation;
        public GameObject finalParticlePrefab;
        public void Resolution(ActionToken token)
        {
            PreSpawnPawn();
            Pawn pawn = Spawn(token);
            PosSpawnPawn(pawn);
        }

        public virtual void PosSpawnPawn(Pawn pawn) { }

        public virtual void PreSpawnPawn()  {   }

        public Pawn Spawn(ActionToken token) {

            //if (Board.s.IsInsideBoard(Master.TargetCenter))
            // {
            Tile birthTile = Board.s[Master.TargetCenter];

            if (!Board.s.IsWalkable(Master.TargetCenter)) {

                        if (Board.s.IsWalkable(Master.TargetCenter + Direction.north))
                    birthTile = Board.s[Master.TargetCenter + Direction.north];

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.south))
                    birthTile = Board.s[Master.TargetCenter + Direction.south];

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.east))
                    birthTile = Board.s[Master.TargetCenter + Direction.east];

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.west))
                    birthTile = Board.s[Master.TargetCenter + Direction.west];

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.northeast))
                    birthTile = Board.s[Master.TargetCenter + Direction.northeast];

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.northwest))
                    birthTile = Board.s[Master.TargetCenter + Direction.northwest];

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.southeast))
                    birthTile = Board.s[Master.TargetCenter + Direction.southeast];

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.southwest))
                    birthTile = Board.s[Master.TargetCenter + Direction.southwest];
            }

            Pawn pawn = pawnPrefab.Instantiate<Pawn>(birthTile.transform.position, token.Owner.transform.rotation, (token.Owner as Character).Color, RoomManager.s.transform);
            pawn.Tile = birthTile;
            pawn.transform.position = birthTile.transform.position;
            pawn.GetComponentInChildren<Animator>().SetTrigger(birthAnimation.ToString());
            return pawn;
           // }
        }
    }
}
 