using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(TextToMeshConverter))]
public class TextToMeshConverterGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var converter = target as TextToMeshConverter;
        if (GUILayout.Button("Text To Mesh"))
        {
            converter.ConvertTextToMesh();

        }
    }
}
#endif
