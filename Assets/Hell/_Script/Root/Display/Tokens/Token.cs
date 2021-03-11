using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hell.Display
{
    public abstract class Token
    {
        private Pawn owner;
        private Direction direction;
        private float duration;

        protected float start;

        /// <summary>
        /// The time padding for this token
        /// </summary>
        protected float padding;

        /// <summary>
        /// The Real starting time (+padding)
        /// </summary>
        public float Start { get { return start + padding; } }

        /// <summary>
        /// when this token is going to end, useful when chaining movements
        /// </summary>
        public float End { get { return Start + Duration; } }
        
        /// <summary>
        /// The owner of the token
        /// </summary>
        public virtual Pawn Owner
        {
            get { return owner; }
            protected set { owner = value; }
        }

        /// <summary>
        /// The direction this action is headed
        /// </summary>
        public virtual Direction Direction
        {
            get { return direction; }
            protected set { direction = value; }
        }

        /// <summary>
        /// The duration of the token
        /// </summary>
        public virtual float Duration
        {
            get { return duration; }
            protected set { duration = value; }
        }

      /*  

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="settings">The settings to initialize this token</param>
        /// <param name="start">when it should start</param>
        public Token(Pawn owner, Direction direction, float duration, float padding = 0.0f, float start = 0.0f)
        {
            this.start = start;
            Owner = owner;
            this.padding = padding;
            this.duration = duration;
            this.direction = direction;
        }
        */

    }
}
