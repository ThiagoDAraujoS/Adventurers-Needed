using UnityEngine;
using System.Collections;
using Hell;
using Hell.Display;
using System;

namespace Hell.Rune
{
    public class Projectile : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public GameObject projectilePrefab;

        protected GameObject projectile;

        public AnimationCurve movementCurve;
        public float delay;
        public bool HasDelay { get { return delay > 0; } }
        public float castingForward = 0.0f;
        public float castingHeight;
        public float castingHeightFinal = 0.0f;
        private Direction dir;
        public void TimelineStart(ActionToken token)
        {
            dir = token.Direction;
            StartCoroutine(CastProjectile(token));
        }
        private Vector3 StartingPoint
        {
            get{ return Master.StartingPosition + (Vector3.up * castingHeight) + (dir.toCoord().ToVector3() * castingForward); }
        }

        IEnumerator CastProjectile(ActionToken token)
        {
            yield return new WaitForSeconds(delay);
            projectile = Instantiate(projectilePrefab, StartingPoint, Quaternion.identity) as GameObject;
            projectile.transform.Rotate(0.0f, (int)dir * 90.0f - 90.0f, 0.0f);
        }

        private Vector3 EndingPoint
        {
            get { return Master.TargetCenterPosition + (Vector3.up * castingHeightFinal); }
        }


        public void TimelineEnd(ActionToken token)
        {
            Destroy(projectile, 5.0f);
        }

        public void TimelineUpdate(ActionToken token, float time)
        {
            if (HasDelay)
                time = ((time * Master.actDuration) - delay) / (Master.actDuration - delay);


            if (projectile != null)
            {
                projectile.transform.position = Vector3.Lerp(StartingPoint, EndingPoint, time);
                projectile.transform.Translate(Vector3.up * movementCurve.Evaluate(time) * 2);
            }
        }
    }
}