using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Hell;
using System.Collections.Generic;
namespace Hell.UI
{
    public class UILifeInteger : MonoBehaviour
    {
        public float delay;
        public Text text;
        Pawn pawn;
        public IEnumerator Show(int amount)
        {

            if (UIManager.S.GlobalState == UIManager.LifeUIState.Free)
                UIColor  = true;

            yield return new WaitForSeconds(delay * 0.5f);
            if(amount < 0)
                AkSoundEngine.PostEvent("Play_Damage", gameObject);
            if(amount > 0)
                AkSoundEngine.PostEvent("Play_Heal_01", gameObject);

            text.text = GenerateString((text.text.Length + amount));

            yield return new WaitForSeconds(delay * 1.0f);

            if (UIManager.S.GlobalState == UIManager.LifeUIState.Free)
                UIColor  = false;
        }

        public bool UIColor { set{  focusColor = (value) ? unhiddenHeartColor : hiddenHeartColor; } }

        string GenerateString(int amount)
        {
            string result = "";
            for (int i = 0; i < amount; i++)
                result += "♥";
            return result;
        }
        public Color unhiddenHeartColor;
        public Color hiddenHeartColor;
        private Color focusColor;
        public float speed = 2;
        void Update()
        {
            text.color = Color.Lerp(text.color, focusColor, speed * Time.deltaTime);
        }
        void Start()
        {
            UIManager.S.LifeUIList.Add(this);
            focusColor = unhiddenHeartColor;
            pawn = GetComponent<Pawn>();
            pawn.OnDamage += Damage;
            pawn.OnHeal += Heal;
        }

        void OnDestroy()
        {
            if(UIManager.S != null && UIManager.S.LifeUIList.Contains(this))
                UIManager.S.LifeUIList.Remove(this);
            pawn.OnDamage -= Damage;
            pawn.OnHeal -= Heal;
        }

        void Damage(int amount)
        {
            StartCoroutine(Show(-amount));
        }
        void Heal(int amount)
        {
            StartCoroutine(Show(+amount));
        }
    }
}