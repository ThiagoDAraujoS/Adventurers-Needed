using UnityEngine;
using System.Collections;
using Hell.Display;
using System;

namespace Hell.Rune
{
    public class MegaPush : TokenDrivenBehaviour<MasterAttack>, IRule<ActionToken>
    {
        public float delay;

        public float duration = 0;
        public AnimationCurve lerpCurve = null;
        public AnimTrigger? walkAnimation = null;
        public AnimTrigger? bumpAnimation = null;
        public float walkPadding = 0;
        public float bumpPadding = 0;

        public void Resolution(ActionToken token)
        {
            int distance;
            Coordinate pawnCoordinate;

            foreach (Pawn pawn in Master.TargetPawns)
            {
                distance = 0;
                pawnCoordinate = pawn.Coord;

                bool loop;
                do {
                    loop = false;
                    if (Board.s.IsWalkable(pawnCoordinate + token.Direction)) {
                        pawnCoordinate += token.Direction;
                        distance++;
                        loop = true;
                    }
                } while (loop);

                float start = 0.0f;
                for (int i = 0; i < distance; i++) {
                    start = token.Owner.Move(token.Direction,
                        new MoveToken.Pair(
                            new MoveToken.MoveSettings(null, duration, lerpCurve, walkAnimation, walkPadding),
                            new AnimToken.AnimSettings(duration, bumpAnimation, bumpPadding),
                            token.Direction), null, start, o => o.tileName != "IceEffect");
                }
            }
        }
    }
}