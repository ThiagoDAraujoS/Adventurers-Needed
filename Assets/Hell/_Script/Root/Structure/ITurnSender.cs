using UnityEngine;
using System.Collections.Generic;
using Hell.Display;

namespace Hell
{
    public interface ITurnSender
    {
        /// <summary>
        /// player places its pawns around
        /// </summary>
        void PlacePawns();

        /// <summary>
        /// the game places the player's pawns around
        /// </summary>
        void PlacePawnsAtRandom();

        /// <summary>
        /// call the planning phase
        /// </summary>
        void CallPlanning();
    }
}
