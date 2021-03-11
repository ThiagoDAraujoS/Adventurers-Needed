using Hell;
using Hell.Display;
using System.Collections;
using UnityEngine;

namespace Hell.Rune
{
    public class Audio : TokenDrivenBehaviour<MasterAct>, IDisplay<Token>//ActVisualisation
    {
        public float startDelay = 0.0f;
        public string audioStart;
       // public bool stopAtEnd = false;

        public float endDelay = 0.0f;
        public string audioStop;

        public void TimelineStart(Token token) {
            if (!string.IsNullOrEmpty(audioStart))
                StartCoroutine(PlayAudio(startDelay, audioStart));
        }

        public IEnumerator PlayAudio(float delay, string audio)
        {
            yield return new WaitForSeconds(delay);
            AkSoundEngine.PostEvent(audio, gameObject);
        }

        public void TimelineUpdate(Token token, float time)
        { }

        public void TimelineEnd(Token token) {
  /*          if (stopAtEnd && !string.IsNullOrEmpty(audioStart))
            {
                StartCoroutine(PlayAudio(endDelay, audioStop));
            }*/

            if (!string.IsNullOrEmpty(audioStop))
                StartCoroutine(PlayAudio(endDelay, audioStop));
        }

    }
}