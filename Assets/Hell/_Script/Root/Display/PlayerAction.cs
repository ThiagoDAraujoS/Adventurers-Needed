using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hell.Display
{ 
    /// <summary>
    /// Describes an entire list of acts needed to fully perform an action in 
    /// </summary>
    public class PlayerAction : MonoBehaviour
    {
        MasterAct[] acts;

        public int Size { get { return acts.Length; } }


        void Initialize()
        {
            acts = gameObject.InitializeComponentsInChildren<MasterAct>()
                .OrderBy(o => o.name)
                .ToArray();
        }

        /// <summary>
        /// Get a master act inside this action
        /// </summary>
        /// <param name="index">act index</param>
        /// <returns>the act</returns>
        public MasterAct this[int index]{
            get {
                return acts[index];
            }
        }

        /// <summary>
        /// Get the number of master acts inside this action
        /// </summary>
        public int Count{ get { return acts.Length; } }
    }
}
