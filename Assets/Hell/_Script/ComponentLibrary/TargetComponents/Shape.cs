using System.Collections.Generic;
using Hell.Display;

namespace Hell.Rune.Target
{
    public class Shape : TargetSystem
    {
        //[HideInInspector]
        public Coordinate[] shape = new Coordinate[7]{
//new Coordinate(4,-2),new Coordinate(4,-1),new Coordinate(4, 0),new Coordinate(4, 1), new Coordinate(4, 2),
                     new Coordinate(3,-1),new Coordinate(3, 0),new Coordinate(3, 1),
                     new Coordinate(2,-1),new Coordinate(2, 0),new Coordinate(2, 1),
                                          new Coordinate(1, 0)};

        public override List<Tile> GetTiles(Token token, Coordinate center, Direction dir = Direction.nothing)
        {
            // Debug.Log(dir);
            List<Tile> result = new List<Tile>();
            Coordinate newCoord;
            foreach (Coordinate coord in shape)
            {
                // GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //  go.transform.position =spin(dir, coord).ToVector3(0.0f) + center.ToVector3(0.0f);
                //  Destroy(go, 3.0f);

                newCoord = spin(dir, coord) + center;
                if (Board.s.IsInsideBoard(newCoord))
                    result.Add(Board.s[newCoord]);
            }
            return result;
        }

        public Coordinate spin(Direction d, Coordinate c)
        {
            switch (d)
            {
                case Direction.north:
                    c = new Coordinate(c.y, c.x);
                    break;
                case Direction.east:
                    break;
                case Direction.south:
                    c = new Coordinate(c.y, -1 * c.x);
                    break;
                case Direction.west:
                    c = new Coordinate(-1 * c.x, c.y);
                    break;
            }
            return c;
        }

    }
}