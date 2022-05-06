using UnityEngine;
using UnityEngineEx;
using UnityEditor;

[CustomEditor(typeof(uj_zbg))]
public class uj_zbgEditor : Editor
{
    wordType _wordType = wordType.Word_Stage;
    wordPosType _wordPosType = wordPosType.Up;

    float _opt1, _opt2, _opt3, _opt4;
    zbgs.Type _cur = zbgs.Type.OutOf;
    public override void OnInspectorGUI()
    {
        uj_zbg ui = (uj_zbg)target;

        if (GUILayout.Button("Apply pre data"))
        {
        }

        base.OnInspectorGUI();

        if (_cur != ui.OnGUI_GetObjType() || ui.checkCreatedFlagOnce())
        {
            _cur = ui.OnGUI_GetObjType();
            switch (_cur)
            {
                case zbgs.Type.Word:
                    _opt3 = 1.17f;
                    _opt4 = 0.0f;
                    break;
            }
        }

        switch (ui.OnGUI_GetObjType())
        {
            case zbgs.Type.Word:
                _wordType = (wordType)EditorGUILayout.EnumPopup("wordType", _wordType);
                _wordPosType = (wordPosType)EditorGUILayout.EnumPopup("wordPosType", _wordPosType);
                _opt3 = EditorGUILayout.FloatField("size(1):", _opt3);
                _opt4 = EditorGUILayout.FloatField("angle(1):", _opt4);
                ui.OnGUI_SynOptions(new f4((int)_wordType + 0.5f,
                    (int)_wordPosType + 0.5f, _opt3, _opt4));
                break;
        }
    }

    enum wordType
    {
        Word_Stage = zbWord.Type.Word_Stage,
        This_Way_Left,
        This_Way_Right,
        Welcome,
    }

    enum wordPosType
    {
        Up = zbWord.PosType.Up,
        Down,
    }
}
