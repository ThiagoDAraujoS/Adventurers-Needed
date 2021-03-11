using UnityEngine;
using Hell.Display;
using System.Linq;

namespace Hell.Rune
{
    public class ConcentricPull : TokenDrivenBehaviour<MasterAttack>, IRule<ActionToken>
    {
        [SerializeField]
        float
            padding = 1.5f,
            pushDuration = 1.5f,
            bumpDuration = 0.5f,
            paddingRandomFactor = 0.1f;

        [SerializeField]
        AnimationCurve
            pushCurve;

        [SerializeField]
        AnimTrigger
            pushAnimation;

        public void Resolution(ActionToken token)
        {
            // Direction direction;
            foreach (Pawn pawn in Master.TargetPawns/*.OrderBy(p => p.Coord.Distance(Master.TargetCenter))*/)
            {

                MoveToken auxToken;
                Vector3 coordinate = -1*(pawn.Coord.ToVector3(0.0f) - Master.TargetCenter.ToVector3(0.0f));
                Direction direction = Coordinate.FromVector3(coordinate.normalized);
                Coordinate result = (coordinate + (coordinate.normalized * 1.1f) + Master.TargetCenter.ToVector3(0.0f));
                if (Board.s.IsWalkable(result))
                {
                    //set pawn's tile
                    pawn.Tile = Board.s[result];

                    //craft move token
                    auxToken = new MoveToken(
                        pawn,
                        direction,
                        new MoveToken.MoveSettings(
                            Board.s[result],
                            pushDuration,
                            pushCurve,
                            pushAnimation,
                            padding + ((Random.value * paddingRandomFactor) - (paddingRandomFactor / 2)),
                            token.Start));

                    //stash tile effect tokens
                    pawn.FireTileEffects(auxToken.End, direction);

                    //stash move token
                    Board.s.MoveTimeline.AddToken(auxToken);

                }
            }
        }
    }
}