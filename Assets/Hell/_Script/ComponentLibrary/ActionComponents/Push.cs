using Hell.Display;

namespace Hell.Rune
{
    public class Push : PushBase//TokenDrivenBehaviour<MasterAttack>, IRule<ActionToken>
    {
        public override void PushBehaviour(ActionToken token)
        {
            Push(token);
        }
    }
}