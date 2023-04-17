using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldSpawner))]
public class WorldSpawnerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This is a help box", MessageType.Info);

        WorldSpawner spawnEditor = (WorldSpawner)target;
        if(GUILayout.Button("Remove Loaded Level"))
        {
            spawnEditor.NoMap();
        }

        if (GUILayout.Button("Load Level"))
        {
            spawnEditor.SpawnAgain();
        }
    }
}
