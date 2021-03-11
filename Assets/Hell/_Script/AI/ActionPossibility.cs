using System.Collections.Generic;
using Hell.Display;

namespace Hell.AI
{

    public struct ActionPossibility
    {
        public List<Coordinate> WayList { get; private set; }

        public int NumOfEnemies { get; private set; }

        public int NumOfFriendlies { get; private set; }

        public int Count{
            get{
                return 
                    ((RuneWorth.CurrentRune != null)? RuneWorth.CurrentRune.Count : 0) + 
                    ((WayList != null)? WayList.Count : 0 );
            }
        }

        public float TotalPotential
        {
            get{

            
                return 0;
            }
        }

        public float PotentialOverMove { get { return (TotalPotential) / ((float)Count); } }

        public RuneHeuristic RuneWorth { get; private set; }

        public ActionPossibility(RuneHeuristic runeWorth, List<Coordinate> wayList, int numOfEnemies, int numOfFriendlies)
        {
            RuneWorth = runeWorth;
            WayList = wayList;
            NumOfEnemies = numOfEnemies;
            NumOfFriendlies = numOfFriendlies;
        }

    }
}