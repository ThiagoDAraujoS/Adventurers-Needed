using UnityEngine;
using System.Collections;
using Hell;
using Hell.Display;

namespace Hell.Rune
{
    public class PartileOnPawns : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public bool playOnStart = false;
        public float delay = 0.0f;
        public GameObject projectilePrefab;
        public float timer;

        public void TimelineStart(ActionToken token) {
            if (playOnStart)
                StartCoroutine(CastParticles(token));
        }

        public void TimelineEnd(ActionToken token) {
            if (!playOnStart)
                StartCoroutine(CastParticles(token));
        }

        public void TimelineUpdate(ActionToken token, float time) { }

        IEnumerator CastParticles(ActionToken token){
            yield return new WaitForSeconds(delay);
            GameObject particleRef;
            for (int i = 0; i < Master.TargetPawns.Count; i++)
            {
                particleRef = Instantiate(projectilePrefab, Master.TargetPawns[i].transform.position, projectilePrefab.transform.rotation) as GameObject;
                particleRef.transform.Rotate(0.0f, (int)token.Direction * 90.0f - 90.0f, 0.0f);
                Destroy(particleRef, 5.0f);
            }

            
        }
    }

}