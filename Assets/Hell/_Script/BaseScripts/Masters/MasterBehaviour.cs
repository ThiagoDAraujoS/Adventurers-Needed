using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Hell.Display
{
    /// <summary>
    /// Basic Act class, each class has 
    /// </summary>TokenyT
    public abstract class MasterBehaviour<TokenType> : Master
        where TokenType : Token
    {
        protected virtual void OnResolve(TokenType token) { }

        /// <summary>
        /// Run start routine
        /// </summary>
        public void RunStart(TokenType token)
        {
            foreach (IDisplay act in displayComponents)
                if(act is IDisplay<Token>)
                    (act as IDisplay<Token>).TimelineStart(token);
                else if(act is IDisplay<TokenType>)
                    (act as IDisplay<TokenType>).TimelineStart(token);
        }

        /// <summary>
        /// Run the update routine
        /// </summary>
        /// <param name="time">time elapsed</param>
        public void RunUpdate(TokenType token, float time)
        {
            foreach (IDisplay act in displayComponents)
                if (act is IDisplay<Token>)
                    (act as IDisplay<Token>).TimelineUpdate(token, time);
                else if (act is IDisplay<TokenType>)
                    (act as IDisplay<TokenType>).TimelineUpdate(token, time);
        }

        /// <summary>
        /// Run end routine
        /// </summary>
        public void RunEnd(TokenType token)
        {
            foreach (IDisplay act in displayComponents)
                if (act is IDisplay<Token>)
                    (act as IDisplay<Token>).TimelineEnd(token);
                else if (act is IDisplay<TokenType>)
                    (act as IDisplay<TokenType>).TimelineEnd(token);
        }

        //@TODO This go up in the activation, should be in the reader
        /// <summary>
        /// Run the resolutionRoutine
        /// </summary>
        public void RunResolution(TokenType token)
        {
            OnResolve(token);

            foreach (IRule act in ruleComponents)
                if (act is IRule<Token>)
                    (act as IRule<Token>).Resolution(token);
                else if (act is IRule<TokenType>)
                    (act as IRule<TokenType>).Resolution(token);
        }
    }

    [Serializable]
    public class UnityFloatEvent : UnityEvent<float>{}

    /// <summary>
    /// Resolution layer enumeration,
    /// defines they layer names what will be taken in consideration when all skills are fired
    /// </summary>
    public enum ActResolutionLayer
    {
        /// <summary>
        /// Utility skills are fired first, they consist in skills that move players around or buff them
        /// </summary>
        utility,

        /// <summary>
        /// Combative skills are the second type of skills checked, they consist in damage-base skills
        /// </summary>
        combative,

        /// <summary>
        /// Move is just a basic movement
        /// </summary>
        move,

        /// <summary>
        /// wait is the last one, actually is not even taken in consideration, since this action means.. do nothing
        /// </summary>
        wait,

    }

}