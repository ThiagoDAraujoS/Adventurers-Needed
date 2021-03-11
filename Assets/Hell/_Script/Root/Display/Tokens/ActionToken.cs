using UnityEngine;
namespace Hell.Display
{

    /// <summary>
    /// An action token encapsulates an action, and everything needed to play it properlly
    /// </summary>
    public class ActionToken : Token
    {
        private int iterator = 0;

        public override float Duration { get { return Master.actDuration; } }

        /// <summary>
        /// The action id (based on the actions in the owner list
        /// </summary>
        public int ActionId { get; private set; }

        /// <summary>
        /// Get the top act's layer
        /// </summary>
        public ActResolutionLayer Layer { get { return Master.layer; } }

        /// <summary>
        /// The action itself
        /// </summary>
        public PlayerAction Action { get { return ((Character)Owner)[ActionId]; } }

        /// <summary>
        /// Get the masteract pointed by the iterator
        /// </summary>
        public MasterAct Master { get { return Action[iterator]; } }

        /// <summary>
        /// Creates an action token
        /// </summary>
        /// <param name="owner">the owner of the token</param>
        /// <param name="actionID">the id of the action based in the owner inventory</param>
        /// <param name="direction">the direction the action is headed</param>
        public ActionToken(Character owner, int actionID, Direction direction)
        {
            ActionId = actionID;
            Owner = owner;
            Direction = direction;
        }
        
        /// <summary>
        /// Number of acts inside this token
        /// </summary>
        public int Count
        {
            get { return Action.Count; }
        }

        /// <summary>
        /// Move iterator
        /// </summary>
        /// <returns>true if iterator is still legal, false if its time to reset or destroy token</returns>
        public bool MoveIterator()
        {
            return (++iterator < Action.Count);
        }

    }
    
}
