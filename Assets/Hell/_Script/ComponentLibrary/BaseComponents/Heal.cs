using System;
using Hell.Display;
using UnityEngine;

namespace Hell.Rune
{
    public class Heal : TokenDrivenBehaviour<Master>, IRule<Token>
    {
        public int healthPower;

        public void Resolution(Token token)
        {
            token.Owner.currentLife += healthPower;
        }
    }
}