using UnityEngine;
using Hell.Display;

namespace Hell.Rune
{
    public class StartParticle : TokenDrivenBehaviour<Master>, IDisplay<Token>
    {
        [SerializeField]
        GameObject particlePrefab;

        public float duration = 2.0f;
        public void TimelineStart(Token token)
        {
            GameObject explosion = Instantiate(particlePrefab);
            explosion.transform.position = token.Owner.transform.position;
            Destroy(explosion, duration);
            explosion.transform.Rotate(0.0f, (int)token.Direction * 90.0f - 90.0f, 0.0f);
        }

        public void TimelineEnd(Token token)
        {
        }

        public void TimelineUpdate(Token token, float time)
        {
        }
        

    }

}