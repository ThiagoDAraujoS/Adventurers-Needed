using UnityEngine;
using System.Collections;
using Hell.Display;
using Hell.Rune.Target;
using Hell.Rune;
using System.Collections.Generic;

namespace Hell
{
    public class Master : MonoBehaviour
    {
        public float actDuration = 1.0f;
        public ActResolutionLayer layer = ActResolutionLayer.wait;

        /// <summary>
        /// Property to access all componentes easily
        /// </summary>
        public IDisplay[] displayComponents;
        public IRule[] ruleComponents;

        /// <summary>
        /// Initialization method
        /// </summary>
        protected virtual void Initialize()
        {
            List<IDisplay> displayComponentsList = new List<IDisplay>();
            List<IRule> ruleComponentsList = new List<IRule>();
            foreach (Component component in GetComponents<Component>())
            {
                if (component is IDisplay)
                {
                    displayComponentsList.Add(component as IDisplay);
                    component.InvokeInitialize();
                }
                if (component is IRule)
                {
                    ruleComponentsList.Add(component as IRule);
                    component.InvokeInitialize();
                }
            }
            displayComponents = displayComponentsList.ToArray();
            ruleComponents = ruleComponentsList.ToArray();
        }

    }
}
