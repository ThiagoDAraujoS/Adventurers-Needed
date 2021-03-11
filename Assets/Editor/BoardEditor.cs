using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using Hell;
[CustomEditor(typeof(Board))]
public class BoardEditor : Editor
{ 
    public override void OnInspectorGUI()
    {
        Board board = (Board)target;

        DrawDefaultInspector();

        if(board.WasInitialized)
        {
            if (!board.IsInSyncWithDistance)
                Button( "Update tiles position", new Color(0.3f, 0.5f, 1.0f), 25.0f, board.RelocateTiles);
            
            if (board.IsInSyncWithTileAmmount)
                Button( "Destroy Board",         new Color(1.0f, 0.4f, 0.3f), 40.0f, board.DestroyBoard);

            else
                Button( "Reload Board",          new Color(0.2f, 0.5f, 1.0f), 40.0f, board.CreateBoard);
        }
        else
            Button(     "Create Board",          new Color(0.2f, 1.0f, 0.5f), 40.0f, board.CreateBoard);
    }

    /// <summary>
    /// Button function, it creates a button with a color and a label that does something
    /// </summary>
    /// <param name="buttonColor">The button color</param>
    /// <param name="label">The button label</param>
    /// <param name="buttonRoutine">What happen when the button is pressed</param>
    /// <param name="options">Button Layout Options</param>
    private void Button(string label, Color buttonColor, float height, Action buttonRoutine)
    {
        GUI.color = buttonColor;
        if (GUILayout.Button(label, GUILayout.Height(height)))
            buttonRoutine();
    }
}
