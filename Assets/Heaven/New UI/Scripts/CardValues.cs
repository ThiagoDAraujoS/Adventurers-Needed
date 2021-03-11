using UnityEngine;
using System.Collections;

public enum CardTypes
{
    Melee,
    Projectile,
    Movement,
    Spell,
    Test
};

[RequireComponent(typeof(CardListener))]
public class CardValues : MonoBehaviour {

    public Sprite thumbnail;
    public string title;
    public CardTypes type;
    public Sprite preview;
    public int damage;
    public int cost;
    public string description;
    public Color color;
    public int id;

}
