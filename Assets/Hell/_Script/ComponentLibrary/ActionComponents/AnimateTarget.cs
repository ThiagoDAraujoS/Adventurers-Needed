using UnityEngine;
using System.Collections;
using Hell;
using System;
using Hell.Display;

namespace Hell.Rune
{
    public class AnimateTarget : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public AnimTrigger animType;
        public float speedFactor;
        public void TimelineStart(ActionToken token)
        {
            foreach (Pawn p in Master.TargetPawns)
                StartCoroutine(Animate(p, Vector3.Distance(p.transform.position, token.Owner.transform.position) * speedFactor));

        }

        IEnumerator Animate(Pawn p, float time)
        {
            yield return new WaitForSeconds(time);
            p.SetAnim(animType);
        }

        public void TimelineEnd(ActionToken token) { }

        public void TimelineUpdate(ActionToken token, float time) { }
    }

}