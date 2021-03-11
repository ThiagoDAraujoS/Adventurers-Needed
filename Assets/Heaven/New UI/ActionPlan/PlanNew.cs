using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class PlanNEW
{
    public List<ActionNEW> actions;
    public int owner;
    public long timestamp;


    public PlanNEW(int owner)
    {
        this.owner = owner;
        actions = new List<ActionNEW>();
    }

    public void AddAction(ActionNEW actionToAdd)
    {
        actions.Add(actionToAdd);
    }

    public void AddAction(int id, Direction direction, int cost)
    {
        actions.Add(new ActionNEW(id, direction, cost));
    }

    public override string ToString()
    {
        string toReturn = "";
        for (int i = 0; i < actions.Count; i++)
            toReturn += (owner+ " " + actions[i].ActionId + " " + actions[i].Direction);

        return toReturn;
    }

    public void DebugMessage()
    {
        string output = "";

        foreach (ActionNEW aN in actions)
        {
            output += aN.ActionId.ToString() + " : " + aN.Direction.ToString() + ", ";
        }

        Debug.LogWarning(output);
    }
}
