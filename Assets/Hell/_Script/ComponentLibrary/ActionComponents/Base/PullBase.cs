using UnityEngine;
using Hell.Display;
using System.Linq;

namespace Hell.Rune
{
    public abstract class PushBase : TokenDrivenBehaviour<MasterAttack>, IRule<ActionToken>
    {
        [SerializeField]
        protected AnimTrigger
                moveAnimation;

        [SerializeField]
        protected float
            moveDuration,
            movePadding;

        [SerializeField]
        protected AnimationCurve
            moveCurve;

        [SerializeField]
        protected AnimTrigger
            bumpAnimation;

        [SerializeField]
        protected float
            bumpDuration,
            bumpPadding;

        public bool isPush = true;

        protected Direction direction;


        public void Resolution(ActionToken token)
        {
            Initialize(token);

            PushBehaviour(token);
        }
         
        private void Initialize(ActionToken token)
        {
            direction = token.Direction;

            if (!isPush)
                direction = token.Direction.Invert();
        }

        protected float Push(ActionToken token, float start = 0.0f)
        {
            float result = 0.0f;
            if (direction != Direction.nothing)
                foreach (Pawn pawn in Master.TargetPawns.OrderByDescending(p => p.Coord.Distance(Master.TargetCenter)))
                    result = Mathf.Max(result, pawn.Move(direction,
                        new MoveToken.Pair(
                            new MoveToken.MoveSettings(null, moveDuration, moveCurve, moveAnimation, movePadding),
                            new AnimToken.AnimSettings(bumpDuration, bumpAnimation, bumpPadding),
                            token.Direction), null, start));
            return result;
        }

        public abstract void PushBehaviour(ActionToken token);
    }
}