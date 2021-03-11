using System.Collections;
using UnityEngine;

namespace Hell.Display
{
    /// <summary>
    /// The move token
    /// </summary>
    public class MoveToken : AnimToken
    {
        /// <summary>
        /// To where it should move
        /// </summary>
        public Tile Destination { get; private set; }

        /// <summary>
        /// The lerping curve
        /// </summary>
        public AnimationCurve LerpCurve { get; private set; }

        /// <summary>
        /// Starting and ending points
        /// </summary>
        public Vector3 
            initialPosition,
            finalPosition;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="settings">The settings to build an move token</param>
        /// <param name="start">when it should start</param>
        public MoveToken(Pawn owner, Direction direction, MoveSettings settings, float start = 0.0f) : 
            base(owner, direction, settings, start)
        {
            Destination = settings.destination;
            LerpCurve = settings.lerpCurve;
        }

        /// <summary>
        /// Initialize the lerping process
        /// </summary>
        private void StartLerp()
        {
            initialPosition = Owner.transform.position;
            finalPosition = Destination.transform.position;
            Owner.Tile = Destination;
        }

        /// <summary>
        /// Lerp the pawn to its destination
        /// </summary>
        /// <param name="time"></param>
        private void LerpPawn(float time)
        {
            Owner.transform.position = 
                Vector3.Lerp(
                    initialPosition, 
                    finalPosition, 
                    LerpCurve.Evaluate(time));
        }

        /// <summary>
        /// Set animation and then start lerping
        /// </summary>
        /// <returns></returns>
        public override IEnumerator Activate()
        {
            Animate();
            StartLerp();
            yield return Stopwatch.PlayUntilReady(Duration, time => LerpPawn(time));
            Board.s.MoveTimeline.OnMove(this);
        }

        /// <summary>
        /// This class wraps all settings required to create a move token
        /// </summary>
        public class MoveSettings : AnimSettings
        {
            public Tile destination;
            public AnimationCurve lerpCurve;

            public MoveSettings(Tile destination, float duration, AnimationCurve lerpCurve,
                AnimTrigger? animation = null, float padding = 0.0f, float start = 0.0f) : 
                base(duration, animation, padding)
            {
                this.lerpCurve = lerpCurve;
                this.destination = destination;
            }
        }

        /// <summary>
        /// This struct binds toguether the settings for an move and a anim token
        /// if the pawn is meant to bump, it generates a anim token, else generates
        /// a move token
        /// </summary>
        public class Pair
        {
            public Pawn owner;
            public Direction direction;
            private MoveSettings moveSettings;
            private AnimSettings animSettings;

            public void SetDestination(Tile destination)
            {
                moveSettings.destination = destination;
            }

            public AnimToken GetAnimToken(float time = 0.0f){
                return new AnimToken(owner, direction, animSettings, time);
            }

            public MoveToken GetMoveToken(float time = 0.0f){
                return new MoveToken(owner,direction, moveSettings, time);
            }

            public MoveToken GetMoveToken(Tile tile, float time = 0.0f)
            {
                moveSettings.destination = tile;
                return new MoveToken(owner, direction, moveSettings, time);
            }

            public Pair(MoveSettings moveSettings, AnimSettings animSettings, Direction direction, Pawn owner = null)
            {
                this.direction = direction;
                this.owner = owner;
                this.moveSettings = moveSettings;
                this.animSettings = animSettings;
            }
        }
    }
}