using UnityEngine;
using System.Collections;
using Hell.Display;
using System;

namespace Hell.Rune
{
    public class MoveToTarget : TokenDrivenBehaviour<MasterThresholdAttack>, IRule<ActionToken>
    {
        public float moveDuration, movePadding, bumpDuration, bumpPadding;
        public AnimationCurve curve;
        public AnimTrigger moveAnimation, bumpAnimation;


        public float duration = 0;
        public AnimationCurve lerpCurve = null;
        public AnimTrigger? walkAnimation = null;
        public float walkPadding = 0;

        public void Resolution(ActionToken token)
        {
            Coordinate target = (Master.TargetPawns.Count == 0) ? Master.TargetCenter : Master.TargetPawns[0].Coord;
            Coordinate coord = target - token.Owner.Coord;

            int distance;

            switch (token.Direction) {
                case Direction.north: case Direction.south:
                    distance = Mathf.Abs(coord.y);
                    break;
                case Direction.east: case Direction.west:
                    distance = Mathf.Abs(coord.x);
                    break;
                default:
                    distance = 0;
                    break;
            }

            float start = 0.0f;

            Predicate<MasterTile> tileValidation = o => o.tileName != "IceEffect";

            for (int i = 0; i < distance; i++) {
                start = token.Owner.Move(token.Direction,
                    new MoveToken.Pair(
                        new MoveToken.MoveSettings(null, moveDuration, curve, moveAnimation, movePadding),
                        new AnimToken.AnimSettings(bumpDuration, bumpAnimation, bumpPadding),
                        token.Direction), null, start, (i == distance-1)? null :  tileValidation);
                
            }


            foreach (Pawn pawn in Master.TargetPawns) {
                distance = 0;
                coord = pawn.Coord;

                bool loop;
                do{
                    loop = false;
                    if (Board.s.IsWalkable(coord + token.Direction))
                    {
                        Debug.Log("distance + 1");
                        coord += token.Direction;
                        distance++;
                        loop = true;
                    }
                } while (loop);

                for (int i = 0; i < distance; i++) {
                    start = pawn.Move(token.Direction,
                        new MoveToken.Pair(
                            new MoveToken.MoveSettings(null, duration, lerpCurve, walkAnimation, walkPadding),
                            new AnimToken.AnimSettings(duration, bumpAnimation, bumpPadding),
                            token.Direction), null, start, tileValidation);
                }
            }
        }
    }
}