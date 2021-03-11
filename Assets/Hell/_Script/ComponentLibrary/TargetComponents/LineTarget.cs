using UnityEngine;
using System.Collections;
using Hell.Rune.Target;
using Hell;
using System;
using System.Collections.Generic;
using Hell.Display;

public class LineTarget : TargetSystem
{
    public override List<Tile> GetTiles(Token token, Coordinate center, Direction dir = Direction.nothing)
    {
        List<Tile> tileList = new List<Tile>();
        Coordinate c = token.Owner.Coord + token.Direction;
        for (int i = 0;
            i < Master.range && !c.Equals(center) && Board.s.IsInsideBoard(c);
            i++, c += token.Direction)
            tileList.Add(Board.s[c]);
        if(Board.s.IsInsideBoard(center))
            tileList.Add(Board.s[center]);
        return tileList;
    }
}
