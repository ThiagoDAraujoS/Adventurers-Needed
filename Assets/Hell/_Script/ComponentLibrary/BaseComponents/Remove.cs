using Hell.Display;

namespace Hell.Rune
{
    public class Remove :  TokenDrivenBehaviour<Master>, IRule<Token>
    {
        public int duration;
        public bool remove;
        public void Resolution(Token token) { 
            Board.s[token.Owner.Coord].Pawn = (remove)? null : token.Owner;
       //     TurnEngine.s.Timeline.re
        }
    }
}