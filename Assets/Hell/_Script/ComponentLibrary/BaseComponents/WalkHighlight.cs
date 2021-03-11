using UnityEngine;
using Hell.Display;

namespace Hell.Rune
{
    public class WalkHighlight : TokenDrivenBehaviour<MasterAct>, IDisplay<Token>
    {
        public GameObject prefab;
        GameObject aux;
        public void TimelineStart(Token token)
        {
            aux = Instantiate(prefab) as GameObject;
            aux.transform.position = token.Owner.Tile.transform.position;
        }
        public void TimelineEnd(Token token)
        {
            Destroy(aux);
        }

        public void TimelineUpdate(Token token, float time) { }
    }
}