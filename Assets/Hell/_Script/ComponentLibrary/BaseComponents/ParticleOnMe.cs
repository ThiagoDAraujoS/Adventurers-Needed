using Hell.Display;
using UnityEngine;

namespace Hell.Rune
{
    public class ParticleOnMe : TokenDrivenBehaviour<Master>, IDisplay<Token>
    {
        public GameObject prefab;
        private GameObject reference;
        public void TimelineStart(Token token){
            reference = Instantiate(prefab, token.Owner.transform.position, prefab.transform.rotation) as GameObject;
        }

        public void TimelineEnd(Token token){
            Destroy(reference);
        }

        public void TimelineUpdate(Token token, float time) { }
    }
}
