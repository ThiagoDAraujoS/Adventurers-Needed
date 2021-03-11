using UnityEngine;
using System.Collections;
using Hell;
using Hell.Display;
using System;
using System.Linq;

namespace Hell.Rune
{
    public class Knockback : TokenDrivenBehaviour<MasterAttack>, IRule<ActionToken>
    {
        [SerializeField]AnimTrigger
            moveAnimation;
        [SerializeField] float
            moveDuration,
            movePadding;
        [SerializeField] AnimationCurve
            moveCurve;


        [SerializeField] AnimTrigger 
            bumpAnimation;
        [SerializeField] float
            bumpDuration,
            bumpPadding;


        [SerializeField] AnimTrigger
            pushAnimation;
        [SerializeField] float
            pushDuration,
            pushPadding;
        [SerializeField] AnimationCurve
            pushCurve;


        [SerializeField] AnimTrigger
            bumpPushAnimation;
        [SerializeField] float
            bumpPushDuration,
            bumpPushPadding;

        public bool forwardDir;


        public void Resolution(ActionToken token)
        {
            Direction direction = token.Direction;

            if (!forwardDir)
                direction = token.Direction.Invert();

            if (direction != Direction.nothing)
                foreach (Pawn pawn in Master.TargetPawns.OrderByDescending(p => p.Coord.Distance(Master.TargetCenter)))
                    pawn.Move(direction,
                        new MoveToken.Pair(
                            new MoveToken.MoveSettings(null, moveDuration, moveCurve, moveAnimation, movePadding),
                            new AnimToken.AnimSettings(bumpDuration, bumpAnimation, bumpPadding),
                            token.Direction),
                        new MoveToken.Pair(
                            new MoveToken.MoveSettings(null, pushDuration, pushCurve, pushAnimation, pushPadding),
                            new AnimToken.AnimSettings(bumpPushDuration, bumpPushAnimation, bumpPushPadding),
                            token.Direction));
        }
    }
}