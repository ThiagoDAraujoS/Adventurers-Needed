using UnityEngine;
using System.Collections;
using Hell.Display;
using Hell.Rune.Target;

[RequireComponent (typeof(PlayerAction))]
public class RuneHeuristic : MonoBehaviour
{

    public PlayerAction CurrentRune { get; private set; }

    public float runeWeight;

    public TargetSystem targetSystem;

    void Start()
    {
        CurrentRune = GetComponent<PlayerAction>();
    }

}
