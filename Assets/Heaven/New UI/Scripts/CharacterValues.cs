using UnityEngine;
using System.Collections.Generic;

public enum CharacterClasses
{
    Templar,
    Goblin,
    Special
};

[RequireComponent(typeof(CardListener))]
public class CharacterValues : MonoBehaviour {

    public string title;
    public CharacterClasses charClass;
    public Sprite thumbnail;
    public int health;
    public string description;
    public Color color;
    public int playerId;

    //Values for persisting modes and cards through scenes
    public List<CardValues> exclusiveCards;
    public GameObject playerFigurine;
}
