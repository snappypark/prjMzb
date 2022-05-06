using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(uj_mulEscape))]
public class uj_MulEscapesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        uj_mulEscape ui = (uj_mulEscape)target;

        if (GUILayout.Button("Generate") && uis.IsEnableBtnTime(1.7f))
            ui.Generate();

        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();
    }
}
