using Hell.Display;

namespace Hell.Rune
{
    public class Drain : TokenDrivenBehaviour<MasterAttack>, IRule<ActionToken>
    {
        public int healAmount;

        public void Resolution(ActionToken token)
        {
            foreach (var item in Master.TargetPawns)
                if(item is Character && item.IsAlive)
                    token.Owner.currentLife += healAmount;
        }
    }
}