using Hell.Display;
using System.Collections.Generic;
using UnityEngine;

namespace Hell
{
    public abstract class TokenDrivenBehaviour<MasterType> : MonoBehaviour 
        where MasterType : Master
    {
        /// <summary>
        /// Master's component reference
        /// </summary>
        protected MasterType Master { get; private set; }

        /// <summary>
        /// Initialization method
        /// </summary>
        protected virtual void Initialize()
        {
            Master = GetComponent<MasterType>();
        }
    }

    public interface IRule<TokenType> : IRule where TokenType : Token
    {
        /// <summary>
        /// Runs all the resolutions of the component, move players, damage people...
        /// </summary>
        void Resolution(TokenType token);


    }

    public interface IDisplay<TokenType> : IDisplay where TokenType : Token
    {
        /// <summary>
        /// What happen when this act start to be played
        /// </summary>
        void TimelineStart(TokenType token);

        /// <summary>
        /// What happen during this act's play
        /// </summary>
        void TimelineUpdate(TokenType token, float time);

        /// <summary>
        /// What happen when the end of this act is reached
        /// </summary>
        void TimelineEnd(TokenType token);

    }

    public interface IDisplay{}

    public interface IRule{}
}



