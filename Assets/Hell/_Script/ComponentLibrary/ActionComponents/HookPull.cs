using UnityEngine;
using System.Collections;
using Hell.Display;
using System;

namespace Hell.Rune
{
    public class HookPull : PushBase
    {
        public override void PushBehaviour(ActionToken token)
        {
            isPush = false;
            float start = 0.0f;
            if (Master.TargetTiles.Count > 0)
                for (int i = 0; i < Master.TargetTiles[0].coord.Distance(token.Owner.Coord); i++)
                    start = Push(token, start);
        }
    }

}