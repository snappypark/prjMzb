using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(uj_mulBattle))]
public class uj_MulBattlesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        uj_mulBattle ui = (uj_mulBattle)target;

        if (GUILayout.Button("Generate") && uis.IsEnableBtnTime(1.7f))
            ui.Generate();

        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();
    }
}
