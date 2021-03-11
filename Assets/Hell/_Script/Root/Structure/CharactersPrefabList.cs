using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "CharacterList", menuName = "Create Character List Object",order = 0)]
public class CharactersPrefabList : ScriptableObject {
    public GameObject[] list;

    public static implicit operator GameObject[] (CharactersPrefabList c) {
        return c.list;
    }
}
