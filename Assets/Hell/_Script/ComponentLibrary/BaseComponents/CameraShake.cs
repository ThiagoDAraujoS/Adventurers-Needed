using UnityEngine;
using System.Collections;
using Hell.Display;

namespace Hell.Rune
{ 
    public class CameraShake : TokenDrivenBehaviour<MasterAct>, IDisplay<Token>
    {

        private Vector3 cameraCenter;
        private GameObject camera;
        public bool playAtStart = false;
        public float factor, delay, duration;

        public AnimationCurve shakeCurve;

        private IEnumerator ShakeRoutine(float factor, float duration, float delay) {
            yield return new WaitForSeconds(delay);
            yield return Stopwatch.PlayUntilReady(duration, time => camera.transform.position = cameraCenter + (Random.insideUnitSphere * shakeCurve.Evaluate(time)) * factor);
            Camera.main.transform.position = cameraCenter;
        }

        private void Start() {
            camera = FindObjectOfType<Camera>().gameObject;
            cameraCenter = camera.transform.position;
        }

        public void TimelineStart(Token token) {
            if(playAtStart)
                StartCoroutine(ShakeRoutine(factor, duration, delay));
        }

        public void TimelineEnd(Token token) {
            if (!playAtStart)
                StartCoroutine(ShakeRoutine(factor, duration, delay));
        }

        public void TimelineUpdate(Token token, float time) { }

    }
}
