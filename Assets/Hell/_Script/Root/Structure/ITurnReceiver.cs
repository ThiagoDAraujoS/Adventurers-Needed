using UnityEngine;
using System.Collections.Generic;
using Hell.Display;

namespace Hell
{
    interface ITurnReceiver
    {
        /// <summary>
        /// Finish to place the pawns and prepare the engine for the next phase
        /// MUST be called at the end the placing pawns
        /// </summary>
        void FinishPlacingPawns();

        /// <summary>
        /// Finish placing pawns at random,
        /// MUST be called at the end of the placing pawns at random
        /// </summary>
        void FinishPlacingPawnsAtRandom();

        /// <summary>
        /// Finishs the planning phase
        /// MUST be called at the end of the planing phase
        /// </summary>
        void FinishPlanning(List<Plan> plan);
    }
}
