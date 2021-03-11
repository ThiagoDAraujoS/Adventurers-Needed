using UnityEngine;
using System.Collections;
using Hell.Display;
using System;

namespace Hell.Rune
{
    public class Art : TokenDrivenBehaviour<MasterTile>
    {
        public GameObject prefab;
        private GameObject reference;
        private void Start() {
            Debug.Log("Spanwed");
            StartCoroutine(Binder());
        }

        IEnumerator Binder() {
            Debug.Log("waiting");
            yield return new WaitUntil(() => Master.host != null);
            Debug.Log("dsaiuh");
            reference = Instantiate(prefab, Master.host.transform.position, prefab.transform.rotation) as GameObject;
        }

        private void OnDestroy() {
            Debug.Log("destroyed");
            Destroy(reference);
        }
    }
}
