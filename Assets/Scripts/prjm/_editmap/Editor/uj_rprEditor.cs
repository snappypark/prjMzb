using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(uj_rpr))]
public class uj_rprEditor : Editor
{
    lambType _lambType = lambType.LambOff;
    botType _botType = botType.BotFire;
    platType _platType = platType.Plat_X;
    pushType _pushType = pushType.PushUp;
    treeType _treeType = treeType.Tree1A_Blue;
    bushType _bushType = bushType.Bush;
    fireType _fireType = fireType.Trap_Fire;

    rprs.Type _cur = rprs.Type.OutOf;
    float _opt1, _opt2, _opt3, _opt4;
    matWalls.Type _mat = matWalls.Type.GrayLight;
    public override void OnInspectorGUI()
    {
        uj_rpr ui = (uj_rpr)target;

        if (GUILayout.Button("Apply pre data"))
        {
            ui.ApplyPreData();
        }

        base.OnInspectorGUI();

        if (_cur != ui.OnGUI_GetPropType() || ui.checkCreatedFlagOnce())
        {
            _cur = ui.OnGUI_GetPropType();
            switch (_cur) {
                case rprs.Type.Bot: // delay, bullet speed,
                    _opt1 = 1.5f; _opt2 = 15;
                    break;
                case rprs.Type.Plat: // null, speed(1), time gap(0~0.5), {scale},
                    _opt1 = 0; _opt2 = Random.Range(0.33f, 0.39f); _opt3 = Random.Range(0.0f, 6.3f);
                    break;
                case rprs.Type.Push: // mat, speed(1), time gap(0~0.5),
                    _opt1 = 0; _opt2 = Random.Range(0.9f, 1.1f); _opt3 = Random.Range(0.0f, 2.1f);
                    break;
                case rprs.Type.Tree: // rotation, height, scale_radius, scale_height, 
                    _opt1 = Random.Range(0, 360); _opt2 = -Random.Range(0.0f, 0.7f);
                    _opt3 = Random.Range(0.9f, 1.1f); _opt4 = Random.Range(0.8f, 1f);
                    break;
                case rprs.Type.Trap: // null, speed(1), time gap(0~0.5), {scale},
                    _opt1 = 0; _opt2 = Random.Range(0.27f, 0.31f); _opt3 = Random.Range(0.0f, 6.3f);
                    break;
                default:
                    _opt1 = 0; _opt2 = 0; _opt3 = 0; _opt4 = 0;
                    break;
            }
        }

        switch (ui.OnGUI_GetPropType())
        {
            case rprs.Type.Box:
                ui.OnGUI_SetCellType(cel1l.Type.Box);
                break;
            case rprs.Type.Lamb:
                _lambType = (lambType)EditorGUILayout.EnumPopup("lambType", _lambType);
                ui.OnGUI_SetCellType((cel1l.Type)_lambType);
                break;
            case rprs.Type.Bot:
                _botType = (botType)EditorGUILayout.EnumPopup("botType", _botType);
                _opt1 = EditorGUILayout.FloatField("delay", _opt1);
                _opt2 = EditorGUILayout.FloatField("speed", _opt2);
                ui.OnGUI_SetCellType((cel1l.Type)_botType);
                ui.OnGUI_SetOptions(new f4(_opt1, _opt2, _opt3, _opt4));
                break;
            case rprs.Type.Plat:
                _platType = (platType)EditorGUILayout.EnumPopup("platType", _platType);
                _opt2 = EditorGUILayout.FloatField("speed(1)", _opt2);
                _opt3 = EditorGUILayout.FloatField("gap[0,1]:", _opt3);
                ui.OnGUI_SetCellType((cel1l.Type)_platType);
                ui.OnGUI_SetOptions(new f4(0, _opt2, _opt3));
                break;
            case rprs.Type.Push:
                _pushType = (pushType)EditorGUILayout.EnumPopup("pushType", _pushType);
                //_opt1 = EditorGUILayout.FloatField("mat", _opt1);
                _mat = (matWalls.Type)EditorGUILayout.EnumPopup("Mat", _mat);
                _opt2 = EditorGUILayout.FloatField("speed(1)", _opt2);
                _opt3 = EditorGUILayout.FloatField("gap[0,1]:", _opt3);
                ui.OnGUI_SetCellType((cel1l.Type)_pushType);
                ui.OnGUI_SetOptions(new f4((float)_mat, _opt2, _opt3 ));
                break;
            case rprs.Type.Tree:
                _treeType = (treeType)EditorGUILayout.EnumPopup("treeType", _treeType);
                _opt1 = EditorGUILayout.FloatField("Rotation", _opt1);
                _opt2 = EditorGUILayout.FloatField("Height(-0)", _opt2);
                _opt3 = EditorGUILayout.FloatField("Scale_XZ(1):", _opt3);
                _opt4 = EditorGUILayout.FloatField("Scale_Y(1):", _opt4);
                ui.OnGUI_SetCellType((cel1l.Type)_treeType);
                ui.OnGUI_SetOptions(new f4(_opt1, _opt2, _opt3, _opt4));
                break;
            case rprs.Type.Bush:
                _bushType = (bushType)EditorGUILayout.EnumPopup("bushType", _bushType);
                ui.OnGUI_SetCellType((cel1l.Type)_bushType);
                ui.OnGUI_SetOptions(new f4(_opt1, _opt2, _opt3, _opt4));
                break;

            case rprs.Type.Trap:
                _fireType = (fireType)EditorGUILayout.EnumPopup("fireType", _fireType);
                _opt2 = EditorGUILayout.FloatField("speed(1)", _opt2);
                _opt3 = EditorGUILayout.FloatField("gap[0,1]:", _opt3);
                ui.OnGUI_SetCellType((cel1l.Type)_fireType);
                ui.OnGUI_SetOptions(new f4(0, _opt2, _opt3));
                break;

            case rprs.Type.Tnt:
                ui.OnGUI_SetCellType(cel1l.Type.Tnt);
                break;
        }

    }

    enum lambType
    {
        LambOn = cel1l.Type.LambOn,
        LambOff,
    }

    enum botType
    {
        BotFire = cel1l.Type.BotFire,
    }
    
    enum platType
    {
        Plat_X = cel1l.Type.Plat_X,
        PlatX,
        Plat_Z,
        PlatZ,
        PlatSpd, PlatHeal, PlatOil,
    }

    enum pushType
    {
        PushUp = cel1l.Type.PushUp,
        PushSide_X,
        PushSideX,
        PushSide_Z,
        PushSideZ,
    }

    enum treeType
    {
        Tree1A_Blue = cel1l.Type.Tree1A_Blue,
        Tree1B_Pink,
        Tree2A_Blue,
        Tree2B_Pink,
        Tree3A_Red,
        Tree3B_Green,
        Tree4A_Red,
        Tree4B_Green,
    }
    
    enum bushType
    {
        Bush = cel1l.Type.Bush,
        Bush1,
        Bush2,
        Bush3,
    }


    enum fireType
    {
        Trap_Fire = cel1l.Type.Trap_Fire,
        Trap_Slow,
        Trap_FireWall,
    }
}
