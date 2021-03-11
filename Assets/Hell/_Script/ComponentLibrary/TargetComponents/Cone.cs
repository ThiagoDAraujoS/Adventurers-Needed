using System.Collections.Generic;
using Hell.Display;

namespace Hell.Rune.Target
{
    public class Cone : TargetSystem
    {
        public float 
            widthFactor,
            thickness;

        public override List<Tile> GetTiles(Token token, Coordinate center, Direction dir = Direction.north)
        {
            List<Tile> result = new List<Tile>();
            Coordinate spineCoord;
            float fanSize = thickness;

            for (int i = 0; i < Master.range; i++ )
            {
                fanSize += widthFactor;
                spineCoord = token.Owner.Coord + (token.Direction.toCoord() * (i+1));
                Add(result, spineCoord);
                for (int f = 0; f < fanSize; f++) {
                    Add(result, spineCoord + (token.Direction.Perpendicular().toCoord() * f));
                    Add(result, spineCoord + (token.Direction.Perpendicular().Invert().toCoord() * f));
                }
            }
            return result;
        }
    }
}