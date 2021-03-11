using UnityEngine;
using System.Collections;
using Hell;
using Hell.Display;

namespace Hell.Rune
{
    public class ParticleOnTarget : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public bool castOnStart = false;
        public bool spinWithHero = false;

        public GameObject projectilePrefab;
        public float timer;
        public void TimelineStart(ActionToken token) {
            if (castOnStart)
                CastParticle(token);
        }
        public void TimelineEnd(ActionToken token) {
            if(!castOnStart)
                CastParticle(token);
        }
        public void TimelineUpdate(ActionToken token, float time) { }

        private void CastParticle(ActionToken token)
        {
            GameObject particle = Instantiate(projectilePrefab, Master.TargetCenterPosition, projectilePrefab.transform.rotation) as GameObject;
            if(spinWithHero)
                particle.transform.Rotate(0.0f, (int)token.Direction * 90.0f - 90.0f, 0.0f);
            Destroy(particle, 5.0f);
        }
    }
}