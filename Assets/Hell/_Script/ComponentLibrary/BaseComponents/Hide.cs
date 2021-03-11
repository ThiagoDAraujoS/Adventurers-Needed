using Hell.Display;
using UnityEngine;

namespace Hell.Rune
{
    public class Hide : TokenDrivenBehaviour<Master>, IDisplay<Token>
    {
        [SerializeField]
        public bool 
            hide,
            hideOnStart;

        public float padding;

        [HideInInspector]
        public Renderer[] rendererArray;

        public void SetHide() {
            foreach (Renderer r in rendererArray)
                r.enabled = !hide;
        }

        public void TimelineStart(Token token) {
            rendererArray = token.Owner.GetComponentsInChildren<Renderer>();
            if(hideOnStart)
                StartHiddingProcess();
        }

        public void TimelineEnd(Token token) {
            if(!hideOnStart)
                StartHiddingProcess();
        }

        public void StartHiddingProcess() {
            if (padding > 0)
                Invoke("SetHide", padding);
            else
                SetHide();
        }

        public void TimelineUpdate(Token token, float time) { }
    }
}