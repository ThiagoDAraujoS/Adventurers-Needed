using UnityEngine;
using System.Collections.Generic;
using Hell.Display;
using System.Linq;
namespace Hell.Rune
{
    public class BreathParticles : Projectile, IDisplay<ActionToken>
    {
        List<ParticleSystem> systemList;
        public float factor;
        private bool wasInitialized;

        public new void TimelineStart(ActionToken token)
        {
            wasInitialized = false;
            base.TimelineStart(token);
        }

        private void InitializeBreath()
        {
            systemList = projectile.GetComponentsInChildren<ParticleSystem>().ToList();

            ParticleSystem main = projectile.GetComponent<ParticleSystem>();

            if (main != null)
                systemList.Add(main);
            Debug.Log(systemList.Count);

            wasInitialized = true;
        }

        public new void TimelineUpdate(ActionToken token, float time)
        {
            base.TimelineUpdate(token, time);
            if(projectile != null)
            {
                if (!wasInitialized)
                    InitializeBreath();
            
                foreach (ParticleSystem s in systemList)
                {
                    switch (s.shape.shapeType)
                    {
                        case ParticleSystemShapeType.Box:
                            Debug.Log("skdjas");
                            var ps = s.shape;
                            Vector3 test = new Vector3(ps.box.x, ps.box.y, ps.box.z *factor);
                            ps.box = test;
                            break;

                        /*case ParticleSystemShapeType.Sphere:
                              break;
                        */

                        default:
                            break;
                    }
                }
            }
          //  system[0].shape.shapeType = ParticleSystemShapeType.
        }
    }
}
