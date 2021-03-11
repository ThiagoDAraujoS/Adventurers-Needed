using UnityEngine;
using System.Collections.Generic;
using Hell;
using Hell.Display;
using System;

namespace Hell.Rune
{
    public class DiagonalProjectile : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public GameObject projectilePrefab;

        protected GameObject[] projectile;

        public int range;

        public AnimationCurve movementCurve;

        public float castingHeight;
        public float castingHeightFinal = 0.0f;

        private Direction dir;

        private Vector3 StartingPoint {
            get { return Master.StartingPosition + (Vector3.up * castingHeight); }
        }

        private Vector3 EndingPoint {
            get { return Vector3.up * castingHeightFinal; }
        }

        public void TimelineStart(ActionToken token)
        {
            projectile = new GameObject[2];
            GameObject auxRef;
            for (int i = 0; i < 2; i++)  {
                auxRef = Instantiate(projectilePrefab, StartingPoint, Quaternion.identity) as GameObject;
                projectile[i] = auxRef;
                auxRef.transform.Rotate(0.0f, dir.ToAngle45() - 90.0f, 0.0f);
            }
        }

        public void TimelineEnd(ActionToken token)
        {
            foreach (var item in projectile) {
                Destroy(item, 5.0f);
            }
        }

        public void TimelineUpdate(ActionToken token, float time)
        {
            projectile[0].transform.position = Vector3.Lerp(StartingPoint, token.Owner.Coord + (Direction.northwest.QuadrantRotate(token.Direction.GetQuadrant()).toCoord()*range), time);
            projectile[1].transform.position = Vector3.Lerp(StartingPoint, token.Owner.Coord + (Direction.northeast.QuadrantRotate(token.Direction.GetQuadrant()).toCoord() * range), time);

            for (int i = 0; i < 2; i++)
                    projectile[i].transform.Translate(Vector3.up * movementCurve.Evaluate(time) * 2);
                
        }

    }
}
