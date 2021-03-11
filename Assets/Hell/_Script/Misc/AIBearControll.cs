using UnityEngine;
using Hell.Display;
using System.Linq;
namespace Hell.Rune
{
    [RequireComponent(typeof(Character))]
    public class AIBearControll : MonoBehaviour
    {
        public int beamRange;
        Character owner;
        PlayerAction skill;
        void Start(){
            owner = GetComponent<Character>();
         //   targetSystem = skill.GetComponentInChildren<TargetSystem>();
            skill = owner.Runes[0];
        }

        void OnEnable() {
            TurnEngine.s.OnLoadPlan += CreatePlan;
        }
        void OnDisable() {
            TurnEngine.s.OnLoadPlan -= CreatePlan;
        }

        public Plan CreatePlan() {
            //Initialize plan
            Plan finalPlan = new Plan(owner);
            Direction target;
            Coordinate fakePosition = owner.Coord;

            while(finalPlan.PlanSize < 4)
            {
                //if player is within range attack
                target = GetTarget(fakePosition);

                //check if has ppl in range
                if (target != Direction.nothing)
                {
                    //if yes, has enouth ap
                    Debug.Log("Plan size " + finalPlan.PlanSize + " skill size " + skill.Size + " " + (finalPlan.PlanSize >= skill.Size));
                    if (4 - finalPlan.PlanSize <= skill.Size)
                    {
                        finalPlan.AddAction(2, Direction.north);
                    }
                    else
                    {
                        //attack
                        finalPlan.AddAction(0, target);
                    }
                }
                //does not has ppl in range
                else
                {
                    //move
                    finalPlan.AddAction(1, GetMovement(ref fakePosition));
                }

            }
            Debug.Log(finalPlan.ToString());
            return finalPlan;
        }

        private Direction GetTarget(Coordinate center) {
            //set final Direction to target
            Direction finalDirection = Direction.nothing;

            //auxiliar coordinate
            Coordinate auxCoord;

            //for each range in the beam
            for (int r = 0; r <= beamRange && finalDirection == Direction.nothing; r++)

                //for each avaliable direction
                for (int i = 0; i < 4 && finalDirection == Direction.nothing; i++) {

                    //save coordinate
                    auxCoord = center + (((Direction)i).toCoord() * r);

                    //if its inside the board
                    if (Board.s.IsInsideBoard(auxCoord) && 

                        //and have a pawn alocated there
                        Board.s[auxCoord].Pawn != null &&

                        //and that pawn is a character
                        Board.s[auxCoord].Pawn is Character &&

                        //and it has a different color them my team
                       (Board.s[auxCoord].Pawn as Character).Color != owner.Color)

                        //set it as its target
                        finalDirection = (Direction)i;
                }

            //return the direction to the target
            return finalDirection;
        }

        private Direction GetMovement(ref Coordinate center) {
            //default value = random
            Direction result = Direction.nothing;

            //get nearest character
            Character target = GetNearestCharacter(center);

            //set to move in its direction
            if (target.Coord.x > center.x)
                result = Direction.east;
            else if (target.Coord.x < center.x)
                result = Direction.west;
            else if (target.Coord.y > center.y)
                result = Direction.north;
            else if (target.Coord.y < center.y)
                result = Direction.south;

            if (result != Direction.nothing) 
                UpdateCoordinate(ref center, result);

            return result;
        }

        private Character GetNearestCharacter(Coordinate center) {
            //final result
            Character result = null;

            //min distance found
            int minDistance = int.MaxValue;
            
            //for each pawn
            foreach (Pawn pawn in Pawn.Pawns.Where(p => p is Character && (p as Character).Color != owner.Color))
                //if its a character, and the distance is shorter than the last previously selected
                if (center.Distance(pawn.Coord) < minDistance) {
                    //record previous distance
                    minDistance = center.Distance(pawn.Coord);
                    //save character
                    result = pawn as Character;
                }
            //return character

            return result;
        }
        private void UpdateCoordinate(ref Coordinate coord, Direction dir)
        {
            Coordinate newCoordinate;
            bool blocked = false;

            do {
                newCoordinate = coord + dir;
                blocked = true;
                if (Board.s.IsWalkable(newCoordinate))
                {
                    coord = newCoordinate;
                    blocked = false;
                }
            } while (!blocked && Board.s[coord].effects.Any(o => o.tileName == "IceTile"));
        }
    }
}