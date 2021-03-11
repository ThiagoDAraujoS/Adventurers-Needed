using UnityEngine;
using Hell.Display;

namespace Hell.Rune
{
    public class ProjectileToTargets : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public bool sendNullProjectile;
        public GameObject projectilePrefab;
        [HideInInspector]
        public GameObject[] projectile;
        public AnimationCurve movementCurve;
        public float delay;
        public bool HasDelay { get { return delay > 0; } }
        public float castingForward = 0.0f;
        public float castingHeight;
        public float castingHeightFinal = 0.0f;
        private Direction dir;
        public float duration = 5.0f;
        private bool IsNullProjectile { get { return sendNullProjectile && Master.TargetPawns.Count == 0; } }
        private bool wasShot = false;

        private Vector3 StartingPoint
        {
            get { return Master.StartingPosition + (Vector3.up * castingHeight) + (dir.toCoord().ToVector3(0.0f) * castingForward); }
        }
        private Vector3 EndingPoint(Vector3 p)
        {
            return p + (Vector3.up * castingHeightFinal);
        }
        public void TimelineStart(ActionToken token)
        {
            dir = token.Direction;
            if (HasDelay)
                Invoke("CastProjectile", delay);
            else
                CastProjectile();
        }
        public void CastProjectile()
        {
            wasShot = true;
            if (IsNullProjectile)
            {
                projectile = new GameObject[1];
                projectile[0] = Instantiate(projectilePrefab, StartingPoint, Quaternion.identity) as GameObject;
                projectile[0].transform.Rotate(dir.angle());
            }
            else
            {
                projectile = new GameObject[Master.TargetPawns.Count];
                for (int i = 0; i < Master.TargetPawns.Count; i++)
                {
                    projectile[i] = Instantiate(projectilePrefab, StartingPoint, Quaternion.identity) as GameObject;
                    projectile[i].transform.Rotate(dir.angle());
                }
            }
        }

        public void TimelineEnd(ActionToken token)
        {
            foreach (var item in projectile)
                Destroy(item, duration);
            projectile = null;
        }

        public void TimelineUpdate(ActionToken token, float time)
        {
            if (HasDelay)
            {
                time = ((time * Master.actDuration) - delay) / (Master.actDuration - delay);
            }
            if (wasShot && projectile != null)
            {
                if (IsNullProjectile)
                {
                    projectile[0].transform.position = Vector3.Lerp(StartingPoint, EndingPoint(Master.TargetCenterPosition), time);
                    projectile[0].transform.Translate(Vector3.up * movementCurve.Evaluate(time) * 2);
                }
                else
                {
                    for (int i = 0; i < projectile.Length; i++)
                    {
                        projectile[i].transform.position = Vector3.Lerp(StartingPoint, EndingPoint(Master.TargetPawns[i].transform.position), time);
                        projectile[i].transform.Translate(Vector3.up * movementCurve.Evaluate(time) * 2);
                    }
                }
            }
        }
    }
}