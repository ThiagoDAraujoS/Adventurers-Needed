using UnityEngine;
using Hell.Display;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Hell.Rune
{
    public class ParticleOnTiles : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public float delay;
        public GameObject particlePrefab;
        public List<GameObject> particleList;
        public void TimelineEnd(ActionToken token) {
            foreach (var item in particleList) {
                foreach (var p in item.GetComponents<ParticleSystem>()){
                    p.Stop();
                }
            Destroy(item, 5.0f);
            }
        }

        public void TimelineStart(ActionToken token) {
            StartCoroutine(CastParticles());
        }

        public void TimelineUpdate(ActionToken token, float time){}

        IEnumerator CastParticles()
        {
            yield return new WaitForSeconds(delay);
            particleList = new List<GameObject>();
            foreach (var item in Master.TargetTiles)
                particleList.Add(Instantiate(particlePrefab, item.transform.position, particlePrefab.transform.rotation) as GameObject);
        }
    }
}