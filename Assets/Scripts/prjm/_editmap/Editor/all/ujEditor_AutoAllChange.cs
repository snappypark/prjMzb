using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(uj_AutoAllChange))]
public class ujEditor_AutoAllChange : Editor
{
    public override void OnInspectorGUI()
    {
        uj_AutoAllChange ui = (uj_AutoAllChange)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("ChangeAllWallMats") && uis.IsEnableBtnTime(1.7f))
        {
            ui.ChangeAllWallMats();
        }
        if (GUILayout.Button("ChangeAllTileMats") && uis.IsEnableBtnTime(1.7f))
        {
            ui.ChangeAllTileMats();
        }
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}
