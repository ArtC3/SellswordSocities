using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileGraphicsMap))]
public class TileGraphicsMapEditorPlugin : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Regenerate"))
        {
            TileGraphicsMap tm = (TileGraphicsMap)target;
            tm.BuildMesh();
        }
    }


}
