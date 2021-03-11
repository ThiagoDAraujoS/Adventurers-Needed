using Hell;
using Hell.Display;
using UnityEngine;

namespace Hell.Rune
{
    /// <summary>
    /// This component act has the designation to animate its owner pawn during the act
    /// </summary>
    public class Animate : TokenDrivenBehaviour<MasterAct>, IDisplay<Token>//ActVisualisation
    {
        public AnimTrigger baseClip;

        public void TimelineStart(Token token)
        {
            token.Owner.SetAnim(baseClip);
        }

        public void TimelineEnd(Token token) { }

        public void TimelineUpdate(Token token, float time) { }

    }

}