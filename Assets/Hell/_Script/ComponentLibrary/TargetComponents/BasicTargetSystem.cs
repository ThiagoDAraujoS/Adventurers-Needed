using UnityEngine;
using System.Collections;
using Hell.Rune.Target;
using System.Collections.Generic;
using Hell;
using Hell.Display;

public class BasicTargetSystem : TargetSystem
{
    public override List<Tile> GetTiles(Token token, Coordinate center, Direction dir = Direction.nothing)
    {
        List<Tile> result = new List<Tile>();
        if (Board.s.IsInsideBoard(center))
            result.Add(Board.s[center]);
        return result;
    }
}
