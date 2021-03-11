using System;

[Serializable]
public class CharacterSelectProtocol
{
    public int playerId;
    public int teamId;
    public string characterName;
    public override string ToString()
    {
        return ("PlayerID = " + playerId + " TeamID = " + teamId + " Character Name = " + characterName);
    }
}