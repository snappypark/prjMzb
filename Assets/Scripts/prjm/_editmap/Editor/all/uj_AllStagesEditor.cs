using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(uj_AllStgs))]
public class uj_AllStagesEditor : Editor
{
    uj_AllStgs.ReMakeStagesType _remaketype = uj_AllStgs.ReMakeStagesType.Default;
    bool _showSingleOption = false;
    bool _showMultiOption = false;
    public override void OnInspectorGUI()
    {
        uj_AllStgs ui = (uj_AllStgs)target;

        EditorGUILayout.Space();

        _showSingleOption = EditorGUILayout.Foldout(_showSingleOption, "Single Stage Option");
        if (_showSingleOption)
        {
            uj_AllStgs.INFO.idxIn100 = EditorGUILayout.IntField("Stage Idx", uj_AllStgs.INFO.idxIn100);

            GUILayout.BeginHorizontal();
            for (int i = 0; i < 4; ++i)
                uj_AllStgs.INFO.checkZones[i] = EditorGUILayout.Toggle(uj_AllStgs.INFO.checkZones[i]);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Make Stage") && uis.IsEnableBtnTime(1.7f))
                ui.MakeStages();
            if (GUILayout.Button("Remake Stage") && uis.IsEnableBtnTime(1.7f))
                ui.ReMakeZone();
            _showMultiOption = false;
        }

        _showMultiOption = EditorGUILayout.Foldout(_showMultiOption, "Multiple Stage Option");
        if (_showMultiOption)
        {
            GUILayout.BeginHorizontal();
            uj_AllStgs.INFO.idxIn100 = EditorGUILayout.IntField("stage Idx[0,99]:", uj_AllStgs.INFO.idxIn100);
            uj_AllStgs.INFO.edxIn100 = EditorGUILayout.IntField(uj_AllStgs.INFO.edxIn100);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            uj_AllStgs.INFO.idxX100 = EditorGUILayout.IntField("stage +100*[0,9]:", uj_AllStgs.INFO.idxX100);
            uj_AllStgs.INFO.edxX100 = EditorGUILayout.IntField(uj_AllStgs.INFO.edxX100);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Make Multiple Stages") && uis.IsEnableBtnTime(1.7f))
                ui.MakeMultilpleStages();


            GUILayout.BeginHorizontal();
            _remaketype = (uj_AllStgs.ReMakeStagesType)EditorGUILayout.EnumPopup(_remaketype);
            if (GUILayout.Button("ReMake Multiple Stages") && uis.IsEnableBtnTime(1.7f))
            {
                switch (_remaketype)
                {
                    case uj_AllStgs.ReMakeStagesType.Default:
                        ui.ChangeInfoes();
                        break;
                    case uj_AllStgs.ReMakeStagesType.ChangeAllFormat_FrStages:
                        ui.ChangeAllJsFormat_FrStages();
                        break;
                    case uj_AllStgs.ReMakeStagesType.ChangeAllFormat_FrLocal:
                        ui.ChangeAllJsFormat_FrLocal();
                        break;
                    case uj_AllStgs.ReMakeStagesType.Check_FrLocal:
                        ui.CheckAllJsFormat_FrLocal();
                        break;
                }
            }// 

            GUILayout.EndHorizontal();
            _showSingleOption = false;
        }
        


    }
}
