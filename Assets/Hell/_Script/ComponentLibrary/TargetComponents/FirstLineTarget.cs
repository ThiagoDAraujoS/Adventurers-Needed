using Hell.Display;
using System.Collections.Generic;
namespace Hell.Rune.Target
{
    public class FirstLineTarget : LineTarget
    {
        public override List<Tile> GetTiles(Token token, Coordinate center, Direction dir = Direction.nothing)
        {
            Tile target = null;
            int minDistance = int.MaxValue;
            int aux;
            foreach (Tile tile in base.GetTiles(token, center, dir))
            {
                if (tile.Pawn != null)
                {
                    aux = tile.coord.Distance(token.Owner.Coord);
                    if (aux < minDistance)
                    {
                        minDistance = aux;
                        target = tile;
                    }
                }
            }

            List<Tile> result = new List<Tile>();
            if (target != null)
                result.Add(target);
            return result;
        }
    }

}