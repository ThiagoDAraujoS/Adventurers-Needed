using System.Collections.Generic;
using System.Linq;
using Hell.Display;
using UnityEngine;

namespace Hell.Rune.Target
{
    public class CircularAoe : TargetSystem
    {
        public float range;
        public override List<Tile> GetTiles(Token token, Coordinate center, Direction dir = Direction.nothing)
        {
            Vector3 centerTilePosition =
                (Board.s.IsInsideBoard(center)) ?
                    Board.s[center].transform.position :
                    center.ToVector3(token.Owner.transform.position.y);

            return Board.s.board.Where(t => Vector2.Distance(
                new Vector2(t.transform.position.x, t.transform.position.z),
                new Vector2(centerTilePosition.x, centerTilePosition.z)) <= range).ToList();
        }
    }
}
