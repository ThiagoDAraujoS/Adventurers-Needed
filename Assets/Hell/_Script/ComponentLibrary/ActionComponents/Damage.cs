using System;
using Hell.Display;
using UnityEngine;
using System.Collections;

namespace Hell.Rune
{
    public class Damage : TokenDrivenBehaviour<MasterAttack>, IRule<ActionToken>
    {
        /// <summary>
        /// The Damage
        /// </summary>
        public int power;

        public int Power
        {
            get {
                int Damage = power;
                return Power;
            }
        }
        public void Resolution(ActionToken token)
        {
            foreach (var item in Master.TargetPawns)
                item.currentLife -= power;
        }
    }
}

