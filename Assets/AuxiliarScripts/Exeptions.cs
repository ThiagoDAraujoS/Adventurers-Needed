using UnityEngine;
using System;
using Hell;

[Serializable]
public class ComponentNotFoundDuringFetchException : Exception
{
    public ComponentNotFoundDuringFetchException(string componentName) :
        base("During the fetch a(n) <color = red>" + componentName + "</color> component was not found!")
    { }
}

[Serializable]
public class NoAvaliableElementException : Exception
{
    public NoAvaliableElementException() : 
        base("The random didnt had any avaliable element to return.") { }
}

[Serializable]
public class InvalidTileException : System.Exception
{
    public InvalidTileException(Pawn pawn, Tile tile) :
        base
        ("the tile " + tile + " " +
            ((tile != null) ?
                "inst null but" :
                "<color=red>is null and</color>") + " " +
            ((Board.s[tile.coord].IsFree) ?
                "is free" :
                "<color=red>inst free</color>") + 
        " therefore couldnt be placed properly as " + pawn + "'s tile")
    { }
}


