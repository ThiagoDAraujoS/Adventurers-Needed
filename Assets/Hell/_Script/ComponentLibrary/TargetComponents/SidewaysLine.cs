using UnityEngine;
using System.Collections;
using Hell.Rune.Target;
using Hell;
using System;
using System.Collections.Generic;
using Hell.Display;

public class SidewaysLine : TargetSystem
{
    public override List<Tile> GetTiles(Token token, Coordinate center, Direction dir = Direction.nothing)
    {
        Coordinate[] coordinate = new Coordinate[3];

        coordinate[1] = token.Owner.Coord;

        switch (dir)
        {
            case Direction.north: case Direction.south:
                coordinate[0] = token.Owner.Coord + Direction.east;
                coordinate[2] = token.Owner.Coord + Direction.west;
                break;

            case Direction.east: case Direction.west:
                coordinate[0] = token.Owner.Coord + Direction.north;
                coordinate[2] = token.Owner.Coord + Direction.south;
                break;
        }

        List<Tile> tileList = new List<Tile>();

        foreach (var item in coordinate)
        {
            if (Board.s.IsInsideBoard(item))
                tileList.Add(Board.s[item]);
        }

        return tileList;
        /*
        Coordinate c = token.Owner.Coord + token.Direction;
        Coordinate l = c + (token.Direction + -1);
        Coordinate r = c + (token.Direction + 1);
        for (int i = 0;
            i < Master.range && !c.Equals(center) && Board.s.IsInsideBoard(c);
            i++, c += token.Direction)
            tileList.Add(Board.s[c]);

        if (Board.s.IsInsideBoard(center) && Board.s.IsInsideBoard(l) && Board.s.IsInsideBoard(r))
        {
            tileList.Add(Board.s[center]);
            tileList.Add(Board.s[l]);
            tileList.Add(Board.s[r]);
        }  
            return tileList;*/
    }
}
