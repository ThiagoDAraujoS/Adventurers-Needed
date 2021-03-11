using System.Collections;
using UnityEngine;

namespace Hell.Display
{
    /// <summary>
    /// All Tokens played by the boardtimeline need to inherit from this one
    /// </summary>
    public abstract class BoardToken : Token
    {
        /// <summary>
        /// If this token is running
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="settings">The settings to initialize this token</param>
        /// <param name="start">when it should start</param>
        public BoardToken(Pawn owner, Direction direction,  BoardSettings settings, float start = 0.0f)
        {
            Direction = direction;
            this.start = start;
            Owner = owner;
            IsRunning = false;
            padding = settings.padding;
            Duration = settings.duration;
        }

        /// <summary>
        /// Invoke this token
        /// </summary>
        public void Play(){
            Board.s.MoveTimeline.StartCoroutine(PlayCoroutine());
        }

        /// <summary>
        /// What this token does when it starts
        /// </summary>
        public abstract IEnumerator Activate();

        /// <summary>
        /// The play routine
        /// </summary>
        public IEnumerator PlayCoroutine()
        {
            IsRunning = true;
            if (Start > 0.0f)
                yield return new WaitForSeconds(Start);
            yield return Activate();
            IsRunning = false;
        }

        /// <summary>
        /// This class holds all settings to initialize one of these tokens
        /// the purpose of this class is to make more easy to create tokens
        /// </summary>
        public class BoardSettings
        {
            public float duration;
            public float padding = 0.0f;

            public BoardSettings(float duration, float padding = 0.0f){
                this.duration = duration;
                this.padding = padding;
            }
        }
    }
}
