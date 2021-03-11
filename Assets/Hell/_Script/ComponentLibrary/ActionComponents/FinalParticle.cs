using UnityEngine;
using System.Collections;
using Hell;
using Hell.Display;

namespace Hell.Rune
{
    public class FinalParticle : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public GameObject projectilePrefab;
        public float timer;
        public void TimelineStart(ActionToken token) { }
        public void TimelineEnd(ActionToken token)
        {
            Destroy(Instantiate(projectilePrefab, Master.TargetCenterPosition, projectilePrefab.transform.rotation), 2.0f);
          //  GameObject explosion = Instantiate(projectilePrefab);

          //  explosion.transform.position = Master.TargetCenterPosition;
          //  Destroy(explosion, 2.0f);

        }
        public void TimelineUpdate(ActionToken token, float time) { }
    }

}