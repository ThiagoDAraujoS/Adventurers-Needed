using UnityEngine;
using System.Collections;
using Hell.Display;
using Hell.Rune.Target;
using Hell.Rune;
using System.Collections.Generic;

namespace Hell
{
    public class MasterDiagonalAttack : MasterAttack
    {
        public bool[] directions { get { return new bool[] { shootNorthwest, shootNortheast, shootSouthwest, shootSoutheast }; } } 
        public bool
            shootNorthwest = true,
            shootNortheast = true,
            shootSouthwest = false,
            shootSoutheast = false;

        [HideInInspector]
        public override Coordinate TargetCenter {
            get{
                if (shootNortheast && Northwest != null)
                    return Northwest.TargetCenter;
                else if (shootNorthwest && Northeast != null)
                    return Northeast.TargetCenter;
                else if (shootSoutheast && Southeast != null)
                    return Southeast.TargetCenter;
                else
                    return Southwest.TargetCenter;
            }
        }
        public override Vector3 TargetCenterPosition {
            get {
                if (shootNortheast && Northwest != null)
                    return Northwest.TargetPosition;
                else if (shootNorthwest && Northeast != null)
                    return Northeast.TargetPosition;
                else if (shootSoutheast && Southeast != null)
                    return Southeast.TargetPosition;
                else
                    return Southwest.TargetPosition;
            }
        }

        public DiagonalAttack[] Diagonal;

        public DiagonalAttack Northwest {
            get { return Diagonal[0];}
            set{ Diagonal[0] = value;}      
        }
        public DiagonalAttack Northeast {
            get { return Diagonal[1]; }
            set { Diagonal[1] = value; }
        }
        public DiagonalAttack Southwest {
            get { return Diagonal[2]; }
            set { Diagonal[2] = value; }
        }
        public DiagonalAttack Southeast {
            get { return Diagonal[3]; }
            set { Diagonal[3] = value; }
        }

        protected override void OnResolve(ActionToken token)
        {
            Diagonal = new DiagonalAttack[4];

            TargetTiles = new List<Tile>();
            TargetPawns = new List<Pawn>();

            //set the starting position
            StartingPosition = token.Owner.transform.position;
            if (shootNorthwest) {
                Northwest = new DiagonalAttack(Direction.northwest);
                Northwest.ColectData(token, range, targetSystem, TargetTiles, TargetPawns, canTargetSelf);
            }
            if (shootNortheast) {
                Northwest = new DiagonalAttack(Direction.northwest);
                Northwest.ColectData(token, range, targetSystem, TargetTiles, TargetPawns, canTargetSelf);
            }
            if (shootSoutheast) {
                Southwest = new DiagonalAttack(Direction.southwest);
                Southwest.ColectData(token, range, targetSystem, TargetTiles, TargetPawns, canTargetSelf);
            }
            if (shootSouthwest) {
                Southeast = new DiagonalAttack(Direction.southeast);
                Southeast.ColectData(token, range, targetSystem, TargetTiles, TargetPawns, canTargetSelf);
            }
        }
    }
    public class DiagonalAttack
    {
        public Direction mainDirection;
        public Coordinate TargetCenter { get; protected set; }
        public Vector3 TargetPosition { get; protected set; }
        public Direction localDirection;

        public DiagonalAttack(Direction main){
            mainDirection = main;
        }

        public void ColectData(ActionToken token, int range, TargetSystem targetSystem, List<Tile> targetTiles, List<Pawn> targetPawns, bool canTargetSelf)
        {
            localDirection = mainDirection.QuadrantRotate(token.Direction.GetQuadrant());
            TargetCenter = token.Owner.Coord + (Direction.northwest.toCoord() * range);
            TargetPosition = Board.s.TheoreticalTilePosition(TargetCenter, token.Owner.transform.position.y);
            List<Tile> tiles = targetSystem.GetTiles(token, TargetCenter, localDirection);
            if (!canTargetSelf)
                tiles.Remove(token.Owner.Tile);
            List<Pawn> pawns = targetSystem.GetTargetPawns(token, ref tiles, TargetCenter, localDirection);
            if (!canTargetSelf)
                pawns.Remove(token.Owner);

            targetTiles.AddRange(tiles);
            targetPawns.AddRange(pawns);
        }
    }
}