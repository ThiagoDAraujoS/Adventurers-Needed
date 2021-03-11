using UnityEngine;
using System.Collections;
using Hell;
using System;
using Hell.Display;

namespace Hell.Rune
{
    public class AnimateTargetBase : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public bool playAtStart = false;
        public AnimTrigger animType;
        public float delay;

        public void TimelineStart(ActionToken token) {
            if(playAtStart)
                StartCoroutine(Animate(delay));
        }

        public void TimelineEnd(ActionToken token)
        {
            if (!playAtStart)
                StartCoroutine(Animate(delay));
        }

        public void TimelineUpdate(ActionToken token, float time) { }

        IEnumerator Animate(float time) {
            yield return new WaitForSeconds(time);
            foreach (Pawn p in Master.TargetPawns)
                p.SetAnim(animType);
        }

    }

}