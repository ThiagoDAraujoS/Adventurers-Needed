using System;
using UnityEngine;

[Serializable]
public class ActionNEW
{
    /// <summary>
    /// The action id (based on the actions in the owner list
    /// </summary>
    public int ActionId;

    /// <summary>
    /// The direction this action is headed
    /// </summary>

    public string Direction;

    public int Cost;

    /// <summary>
    /// Creates an action protocol
    /// </summary>
    /// <param name="owner">the owner of the protocol</param>
    /// <param name="actionID">the id of the action based in the owner inventory</param>
    /// <param name="direction">the direction the action is headed</param>
    public ActionNEW(int actionID, Direction direction, int cost)
    {
        ActionId = actionID;
        Cost = cost;
        Direction = direction.ToString();
    }

}
