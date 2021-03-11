using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System;
using Hell.Display;

namespace Hell
{
    public class RuneEditor : EditorWindow
    {
        string name = "";
        string description = "";
        Sprite icon;
        int size;
        bool destroy = false;

        Color red = new Color(1.0f, 0.5f, 0.3f);
        Color redBG = new Color(1.0f, 0.7f, 0.6f);
        Color blue = new Color(0.3f, 0.5f, 1.0f);
        Color blueBG = new Color(0.6f, 0.75f, 1.0f);
        Color green = new Color(0.3f, 1.0f, 0.5f);
        Color greenBG = new Color(0.8f, 1.0f, 0.85f);

        GameObject runePrototype = null;

        [MenuItem("Rune Forge/Rune Forge")]
        private static void ShowEditor()
        {
            EditorWindow.GetWindow<RuneEditor>(false, "Rune prefab generator");
        }

        public bool FormNotReady
        {
            get { return (icon == null || description == null || description == "" || name == null || name == "" || IsSizeNotLegal); }
        }

        void PrefabField()
        {
            GUI.Label(new Rect(5, 2, Screen.width - 230, 18), "Prototype:");
            GUI.color = blueBG;
            runePrototype = EditorGUI.ObjectField(new Rect(72, 2, Screen.width - 90, 18), runePrototype, typeof(GameObject), false) as GameObject;
        }

        void RuneForm()
        {
            FormField<string>(new Rect(5, 60, Screen.width - 230, 18), "Name:", o => (o == null || o == ""),
                       ref name, EditorGUI.TextField, new Rect(47, 60, Screen.width - 270, 18));

            FormField<int>(new Rect(Screen.width - 220, 60, 128, 128), "Size:", o => (o != runePrototype.transform.childCount),
                           ref size, EditorGUI.IntField, new Rect(Screen.width - 186, 60, 39, 18));

            FormField<string>(new Rect(5, 78, Screen.width - 122, 18), "Description:", o => (o == null || o == ""),
                              ref description, EditorGUI.TextArea, new Rect(5, 96, Screen.width - 152, 92));

            GUI.color = (icon == null) ? redBG : greenBG;
            icon = EditorGUI.ObjectField(new Rect(Screen.width - 142, 60, 128, 128), icon, typeof(Sprite), false) as Sprite;
            GUI.color = Color.white;
        }

        void OnSelectionChange()
        {
            //   gameObject = Selection.activeGameObject;
            //   Editor Editor = Editor.CreateEditor(gameObject);
        }

        private delegate T DrawFieldDelegate<T>(Rect rect, T info);

        void FormField<T>(Rect position, string label, System.Predicate<T> validation, ref T variable, DrawFieldDelegate<T> drawingDelegate, Rect drawRect)
        {
            GUI.Label(position, label);
            GUI.color = (validation(variable)) ? redBG : greenBG;
            variable = drawingDelegate(drawRect, variable);
            GUI.color = Color.white;
        }

        void Button(string label, Color color, Rect area, Action action)
        {
            GUI.color = color;
            if (GUI.Button(area, label))
                action();
            GUI.color = Color.white;
        }

        void ActBar()
        {
            for (int i = 0; i < size; i++)
            {
                Button("Act " + (i + 1), blue, new Rect(5 + (((Screen.width - 9) / size) * i), 4, (((Screen.width - 9) / size)) - 5, 50), () =>
                {
                    Selection.activeGameObject = runePrototype.transform.GetChild(i).gameObject;
                });
            }
        }

        public bool IsSizeNotLegal
        {
            get
            {
                return (size < 1 || size > 4);
            }
        }

        void OnGUI()
        {
            runePrototype = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Editor/RuneForge/RunePrototype.prefab");

            //If there's no folder
            if (!AssetDatabase.IsValidFolder("Assets/Editor/RuneForge"))
            {
                //Folder creation button
                Button("Generate rune forge folder", blue, new Rect(6, 5, Screen.width - 14, 60), () =>
                {
                    AssetDatabase.CreateFolder("Assets/Editor", "RuneForge");
                });
            }

            //If there's folder but there's not prototype
            else if (runePrototype == null || runePrototype.name != "RunePrototype")
            {
                //Load an prefab as prototype
                PrefabField();

                //Load rune prefab btton
                Button("Load Rune prefab", blue, new Rect(6, 22, Screen.width - 18, 32), () => { });

                //rune size creation
                FormField<int>(new Rect(5, 60, 128, 128), "Size:", o => IsSizeNotLegal,
                    ref size, EditorGUI.IntField, new Rect(39, 60, 39, 32));

                //rune prototype creation button
                Button("Create New Rune", (size < 1 || size > 4) ? red : green, new Rect(85, 60, Screen.width - 97, 32), () =>
                {
                    if (!IsSizeNotLegal)
                        CreatePrototype();
                });
            }

            //if there's a RunePrototype object inside forge
            else
            {
                //Act bar
                ActBar();

                //Basic form
                RuneForm();

                //Export prefab button
                Button("Export prefab", (FormNotReady) ? red : green, new Rect(6, 192, Screen.width - 18, 32), () =>
                {
                    if (!FormNotReady)
                    {

                    }
                });

                //Destroy button
                Button((destroy) ? "ARE YOU SURE?" : "Destroy prototype", red, new Rect(Screen.width - 350, 228, 338, 22), () => destroy = !destroy);

                //Destroy verification
                if (destroy)
                    Button("YES I WANT TO DESTROY THIS PROTOTYPE FOREVER", Color.red, new Rect(Screen.width - 350, 252, 338, 40), () =>
                    {
                        destroy = false;
                        DestroyPrototype();
                    });
            }
        }

        void CreatePrototype()
        {
            string path = "Assets/Editor/RuneForge/RunePrototype.prefab";
            GameObject result = new GameObject();
            for (int i = 0; i < size; i++)
            {
                GameObject temp = new GameObject();
                temp.transform.parent = result.transform;
                temp.AddComponent<MasterBehaviour<ActionToken>>();
            }
            PrefabUtility.CreatePrefab(path, result);
            runePrototype = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            DestroyImmediate(result);
        }

        void CreatePrefab()
        {
        }

        void DestroyPrototype()
        {

            AssetDatabase.DeleteAsset("Assets/Editor/RuneForge/RunePrototype.prefab");
        }

        void LoadPrefab()
        {

        }
    }
}