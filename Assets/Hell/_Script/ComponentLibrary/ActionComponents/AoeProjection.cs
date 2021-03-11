using UnityEngine;
using System.Collections.Generic;
using Hell.Display;
using Hell.Rune.Target;

namespace Hell.Rune
{
    public class AoeProjection : TokenDrivenBehaviour<MasterAttack>, IDisplay<ActionToken>
    {
        public GameObject prefab;
        List<GameObject> projectors;

        public void TimelineStart(ActionToken token){
            projectors = new List<GameObject>();

            GameObject aux;
            foreach (var item in Master.TargetTiles)
            {
                aux = Instantiate(prefab) as GameObject;
                projectors.Add(aux);
                aux.transform.position = item.transform.position;
            }
        }
        public void TimelineEnd(ActionToken token)
        {
            foreach (var item in projectors){
                Destroy(item);
            }

        }

        public void TimelineUpdate(ActionToken token, float time) { }
    }
}