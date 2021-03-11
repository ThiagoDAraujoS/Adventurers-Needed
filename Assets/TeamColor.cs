using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "TeamColorList", menuName = "Create Team color list", order = 0)]
public class TeamColor : ScriptableObject
{
    public Color[] list;
}
