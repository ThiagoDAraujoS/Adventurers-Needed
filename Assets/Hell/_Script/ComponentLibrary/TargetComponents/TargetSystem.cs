
using Hell.Display;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hell.Rune.Target
{

    public abstract class TargetSystem : BaseTargetSystem
    {
        //-------------------------------------------------------------------------------------------------------------------------------------------
        public abstract List<Tile> GetTiles(Token token, Coordinate center, Direction dir = Direction.nothing);

        /// <summary>
        /// Get pawns in the area
        /// </summary>
        /// <param name="coords">the center of the area</param>
        /// <returns>the pawns</returns>
        public List<Pawn> GetTargetPawns(Token token, Coordinate center, Direction dir = Direction.nothing){
            return GetTargetPawns(token, GetTiles(token, center, dir));
        }

        /// <summary>
        /// Get pawns in a list, if list is null calculate it on the fly
        /// </summary>
        /// <param name="center">the center of the area</param>
        /// <param name="coords">the calculated list</param>
        /// <returns>the list of pawns</returns>
        public List<Pawn> GetTargetPawns(Token token, ref List<Tile> coords, Coordinate center, Direction dir = Direction.nothing) {
            if(coords == null)
                coords = GetTiles(token, center, dir);
            return GetTargetPawns(token, coords);
        }

        /// <summary>
        /// Get pawns in the area
        /// </summary>
        /// <param name="coords">the area</param>
        /// <returns>the pawns</returns>
        public List<Pawn> GetTargetPawns(Token token, List<Tile> coords ) {
            List<Pawn> result = new List<Pawn>();
            foreach (Tile tile in coords)
                if (tile.Pawn)
                    result.Add(tile.Pawn);
            return result;
        }

        public void Add(List<Tile> list, Coordinate coord)
        {
            if (Board.s.IsInsideBoard(coord))
                list.Add(Board.s[coord]);
        }
    }

   
}