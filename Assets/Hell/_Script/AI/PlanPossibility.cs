using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hell;
using Hell.Display;
using Hell.Rune.Target;

namespace Hell.AI
{

    public struct PlanPossibility
    {
        public List<Coordinate> WayList { get; private set; }

        public PlayerAction CurrentRune { get; private set; }

        public int NumOfEnemies { get; private set; }

        public int NumOfFriendlies { get; private set; }

        public int RuneWeight { get; private set; }

        public int TotalDamage { get { return (NumOfEnemies - NumOfFriendlies) * RuneWeight; } }

        public int Count{
            get{
                return 
                    ((CurrentRune!= null)? CurrentRune.Count : 0) + 
                    ((WayList != null)? WayList.Count : 0 );
            }
        }

        public float DamageOverMove { get { return ((float)TotalDamage) / ((float)Count); } }

        public PlanPossibility(List<Coordinate> wayList, PlayerAction currentRune, int runeWeight, int numOfEnemies, int numOfFriendlies) : this()
        {
            WayList = wayList;
            CurrentRune = currentRune;
            RuneWeight = runeWeight;
            NumOfEnemies = numOfEnemies;
            NumOfFriendlies = numOfFriendlies;
        }

    }
}