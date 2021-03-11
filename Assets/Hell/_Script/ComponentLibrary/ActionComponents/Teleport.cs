using System;
using Hell.Display;
using UnityEngine;
using Hell.Rune.Target;
using System.Collections;
using System.Linq;
namespace Hell.Rune
{
    public class Teleport : TokenDrivenBehaviour<MasterThresholdAttack>, IRule<ActionToken>, IDisplay<ActionToken>
    {
        public AnimationCurve centerPushCurve = null;
        public AnimTrigger bumpAnimation = AnimTrigger.Bump;
        public float bumpDuration = 0.0f;
        public float bumpPadding = 0.0f;

        public GameObject portalInPrefab, portalOutPrefab;
        public AnimTrigger inAnimation;
        public float startParticleDelay, endParticleDelay;

        //Teleport resolution
        public void Resolution(ActionToken token)
        {
            if (Master.CenterTile.Pawn != null && Master.CenterTile.Pawn != token.Owner)
            {
                Direction pushDirection = Direction.nothing;

                if (Board.s.IsWalkable(Master.TargetCenter + Direction.north))
                    pushDirection = Direction.north;

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.south))
                    pushDirection = Direction.south;

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.east))
                    pushDirection = Direction.east;

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.west))
                    pushDirection = Direction.west;

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.northeast))
                    pushDirection = Direction.northeast;

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.northwest))
                    pushDirection = Direction.northwest;

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.southeast))
                    pushDirection = Direction.southeast;

                else if (Board.s.IsWalkable(Master.TargetCenter + Direction.southwest))
                    pushDirection = Direction.southwest;

                Master.CenterTile.Pawn.Move(pushDirection,
                    new MoveToken.Pair(
                        new MoveToken.MoveSettings(
                            Board.s[Master.TargetCenter + pushDirection],
                            bumpDuration,
                            centerPushCurve,
                            bumpAnimation,
                            bumpPadding),
                        null,
                        pushDirection));
            }
            token.Owner.Tile = Master.CenterTile;
        }

        public void TimelineStart(ActionToken token) {
            StartCoroutine(SpawnParticles(token, startParticleDelay,portalInPrefab));
            token.Owner.SetAnim(inAnimation);
        }

        public void TimelineUpdate(ActionToken token, float time) { }

        public void TimelineEnd(ActionToken token) {
            token.Owner.transform.position = Master.TargetCenterPosition;
            StartCoroutine(SpawnParticles(token, endParticleDelay, portalOutPrefab));
        }

        IEnumerator SpawnParticles(ActionToken token, float delay, GameObject prefab) {
            yield return new WaitForSeconds(delay);
            Destroy(Instantiate(prefab, token.Owner.transform.position, token.Owner.transform.rotation), 5.0f);
        }
    }
}
