using System.Collections.Generic;
using System.Linq;
using Hell.Display;
namespace Hell.Rune.Target
{
    public class AreaOfEffect : TargetSystem
    {
        public int range;
        public override List<Tile> GetTiles(Token token, Coordinate center, Direction dir = Direction.nothing)
        {
            return Board.s.board.Where(t => t.coord.Distance(center) <= range).ToList();
        }
    }
}