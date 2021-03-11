using System.Collections;
using UnityEngine;

namespace Hell.Display
{
    /// <summary>
    /// The animation token, it displays a animation on the board
    /// </summary>
    public class AnimToken : BoardToken
    {
        /// <summary>
        /// The animation
        /// </summary>
        public AnimTrigger? animation = null;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="settings">the animation settings</param>
        /// <param name="start">when it should start</param>
        public AnimToken(Pawn owner, Direction direction, AnimSettings settings, float start = 0.0f) : base(owner, direction, settings, start){
            animation = settings.animation;
        }

        protected void Animate()
        {
            if (animation != null)
                Owner.SetAnim(animation.Value);
        }

        /// <summary>
        /// Set the animation and wait for its duration
        /// </summary>
        public override IEnumerator Activate()
        {
            Animate();
            yield return new WaitForSeconds(Duration);
        }

        /// <summary>
        /// This class wraps all settings to build on animation token
        /// </summary>
        public class AnimSettings : BoardSettings
        {
            /// <summary>
            /// The animation tag
            /// </summary>
            public AnimTrigger? animation = null;

            public AnimSettings(float duration, AnimTrigger? animation = null, float padding = 0.0f) :
                base(duration, padding){
                this.animation = animation;
            }
        }
    }
}
