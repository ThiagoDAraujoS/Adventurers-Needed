using Hell.Display;
using UnityEngine;

namespace Hell.Rune
{
    /// <summary>
    /// This act component makes the player walk, during this walk it does animate aswell
    /// if the player bumps into something isntead of walking it stays in the same place and
    /// animate a bump
    /// </summary>
    public sealed class MoveMe : TokenDrivenBehaviour<Master>, IRule<Token>
    {
        public AnimTrigger
            walk = AnimTrigger.Walk,
            bump = AnimTrigger.Bump,
            bumpbump = AnimTrigger.BumpBump,
            slide = AnimTrigger.Slide;

        public AnimationCurve
            moveCurve,
            bumpPushCurve;

        public float
            movePadding,
            bumpPadding,
            pushPadding,
            pushBumpPadding;

        public void Resolution(Token token)
        {
            token.Owner.Move(token.Direction,
                new MoveToken.Pair(
                    new MoveToken.MoveSettings(null, Master.actDuration, moveCurve, walk, movePadding),
                    new AnimToken.AnimSettings(Master.actDuration, bump, bumpPadding), token.Direction),
                new MoveToken.Pair(
                    new MoveToken.MoveSettings(null, Master.actDuration, bumpPushCurve, slide, pushPadding),
                    new AnimToken.AnimSettings(Master.actDuration, bumpbump, pushBumpPadding), token.Direction),
                token.Start);
        }

        public void LateBoardResolution(Token token) { }
    }
}