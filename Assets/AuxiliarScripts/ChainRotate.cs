using UnityEngine;
using Hell.Display;

namespace Hell.Rune
{
    public class ChainRotate : ProjectileToTargets, IDisplay<ActionToken>
    {
        public ParticleSystem[] pSystem = null;
        public new void TimelineStart(ActionToken token) {
            base.TimelineStart(token);
            foreach (var p in projectile)
                foreach (var sys in p.GetComponentsInChildren<ParticleSystem>())
                {
                    Debug.Log(sys.startRotation3D);
                    sys.startRotation3D += token.Direction.angle();
                }

        }
    }
}
