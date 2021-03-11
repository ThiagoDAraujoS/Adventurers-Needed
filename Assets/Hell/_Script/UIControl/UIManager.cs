using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Hell.UI
{
    public class UIManager : MonoBehaviour
    {
        public enum LifeUIState { Free, Hidden, Unhidden }

        public static UIManager s;
        public static UIManager S
        {
            get {
                if(s == null) {
                    FindObjectOfType<UIManager>().Initialize();
                    if(s== null)
                        (new GameObject()).AddComponent<UIManager>().Initialize();
                }
                return s;
            }
        }
        public List<UILifeInteger> LifeUIList {
            get {
                if (lifeUIList == null)
                    lifeUIList = new List<UILifeInteger>();
                return lifeUIList;
            }
            set { lifeUIList = value; }
        }
        public LifeUIState GlobalState {
            get { return globalState; }
            set { 
                if(value == LifeUIState.Hidden)
                    SetAllLifeUI(false);

                if (value == LifeUIState.Unhidden)
                    SetAllLifeUI(true);

                globalState = value;
            }
        }
        public void SetAllLifeUI(bool value)
        {
            Debug.Log("Start To Set all UI");
            foreach (var item in LifeUIList)
            {
                item.UIColor = value;
            }
        }

        private List<UILifeInteger> lifeUIList;
        private LifeUIState globalState;
        void Initialize()
        {
            s = this;
            GlobalState = LifeUIState.Unhidden;
        }
    }
}
