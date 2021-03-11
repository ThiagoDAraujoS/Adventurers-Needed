using UnityEngine;
using System.Collections;
using UnityEditor;
using Hell;
using Hell.Display;
using Hell.Rune;
using Hell.Rune.Target;

public class CreateRunes : MonoBehaviour{

    private static GameObject runeShell;
    private static string actName = "Act ";

    /// <summary>
    /// Menu Dropdown for a 1 act rune
    /// </summary>
    [MenuItem("Create Runes/Add New/1 Act")]
    private static void SingleAct()
    {
        CreateActs(1);
        CreatePrefab();
    }

    /// <summary>
    /// Menu dropdown for a 2 act rune
    /// </summary>
    [MenuItem("Create Runes/Add New/2 Acts")]
    private static void DoubleAct()
    {
        CreateActs(2);
        CreatePrefab();
    }

    /// <summary>
    /// Menu dropdown for a 3 act rune
    /// </summary>
    [MenuItem("Create Runes/Add New/3 Acts")]
    private static void TripleAct()
    {
        CreateActs(3);
        CreatePrefab();
    }

    /// <summary>
    /// Menu dropdown for a 4 act rune
    /// </summary>
    [MenuItem("Create Runes/Add New/4 Acts")]
    private static void QuadActs()
    {
        CreateActs(4);
        CreatePrefab();
    }
    
    /// <summary>
    /// Method that creates acts and the rune shell
    /// Attaches components to them as they are created as well
    /// </summary>
    /// <param name="max"></param>
    private static void CreateActs(int max)
    {
        runeShell = new GameObject();
        runeShell.AddComponent<PlayerAction>();
        GameObject actComponent;
        //iterating through each rune and adding components, changing the name, etc.
        for (int i = 0; i < max; i++)
        {
            actComponent = new GameObject();
            actComponent.name = actName + (i + 1);
            actComponent.AddComponent<MasterAct>();
            actComponent.AddComponent<Animate>();
            actComponent.transform.parent = runeShell.transform;
            //if the index is in the last position, attach additional scripts to end the spell
            if (i == max -1)
            {
                actComponent.AddComponent<MasterAttack>();
                actComponent.AddComponent<Audio>();
                actComponent.AddComponent<Projectile>();
                actComponent.AddComponent<Damage>();
            }
        }
    }
    
    private static void CreatePrefab()
    {
        string path = "Assets/Hell/_Script/ComponentLibrary/Runes/RunePrefabs/NewRune.prefab";
        PrefabUtility.CreatePrefab(path, runeShell);
        DestroyImmediate(runeShell);
    }
}
