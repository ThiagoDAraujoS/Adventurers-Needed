using UnityEngine;
using UnityEngine.UI;
using Hell;
public class Unravel : MonoBehaviour
{
    public bool isWise = false;
    public Image image;
    public AnimationCurve curve;
    public float speed;
    public float focus;

    public AudioClip startGameTextAudio;
    public AudioClip endGameTextAudio;

    public bool IsOpen {
        set {
            float aux = (value) ? 1 : 0;
            Debug.Log(aux + " " + focus);
            if (aux != focus)
            {
                Debug.Log("dsajlk"); 
                focus = aux;
                if (!isWise) {
                    if (aux == 1)
                        PlaySound(startGameTextAudio);
                    if (aux == 0)
                        PlaySound(endGameTextAudio);
                } else {
                    if (aux == 1)
                        AkSoundEngine.PostEvent("Play_Start_Game", gameObject);
                    if (aux == 0)
                        AkSoundEngine.PostEvent("Play_Start_game_REV_01", gameObject);
                }
            }
        } 
    }

    void FixedUpdate() {
        image.fillAmount = Mathf.Lerp(image.fillAmount, focus, speed);
    }

    public void PlaySound(AudioClip audioclip)
    {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = audioclip;
            audio.Play();
    }
}
