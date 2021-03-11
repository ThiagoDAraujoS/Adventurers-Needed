using Hell.Display;
using System.Linq;

namespace Hell.Rune
{
    public class SetTiles : TokenDrivenBehaviour<MasterAttack>, IRule<ActionToken>
    {
        public MasterTile masterTile;
        bool initialized = false;
        public void Resolution(ActionToken token)
        {
            if (!initialized)
            {
                masterTile.gameObject.InitializeComponent<MasterTile>();
                initialized = true;
            }
            foreach (var item in Master.TargetTiles)
                if(!item.effects.Any(o => o.tileName == masterTile.name))
                    item.effects.Add(masterTile);
        }
    }
}
