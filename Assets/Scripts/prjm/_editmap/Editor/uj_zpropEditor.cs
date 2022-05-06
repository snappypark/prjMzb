using UnityEngine;
using UnityEngineEx;
using UnityEditor;

[CustomEditor(typeof(uj_zprp))]
public class uj_zprpEditor : Editor
{
    areaType _areType = areaType.AreaWin;
    triggerType _triggerType = triggerType.TrigCircleBomb;
    doorType _doorType = doorType.Door1;
    keyType _keyType = keyType.key1;
    zDoor.State _doorState = zDoor.State.Locked;
    zBomb.Type _bombState = zBomb.Type.Default;

    zprps.Type _cur = zprps.Type.OutOf;

    float _opt1, _opt2, _opt3, _opt4;
    Color _color;
    matWalls.Type _matWall = matWalls.Type.BlueDark;
    public override void OnInspectorGUI()
    {
        uj_zprp ui = (uj_zprp)target;

        if (GUILayout.Button("Apply pre data"))
        {
            ui.ApplyPreData();
        }

        base.OnInspectorGUI();

        if (_cur != ui.OnGUI_GetZpropType() || ui.checkCreatedFlagOnce())
        {
            _cur = ui.OnGUI_GetZpropType();
            switch (_cur)
            {
                case zprps.Type.Door:
                    _doorState = zDoor.State.Locked;
                    _opt2 = 30;
                    break;
                case zprps.Type.Trigger:
                    _matWall = uj_abs.matWall;
                    break;
                case zprps.Type.Bomb:
                    _opt1 = 1; _opt2 = 30; _opt3 = 1;
                    break;
                case zprps.Type.Ammo:
                    _opt1 = 1; _opt2 = 30; _opt3 = 10; _opt4 = 20;
                    break;
            }
        }

        switch (ui.OnGUI_GetZpropType())
        {
            case zprps.Type.Area:
                _areType = (areaType)EditorGUILayout.EnumPopup("areaType", _areType);
                ui.OnGUI_SynCellType((cel1l.Type)_areType);
                break;
            case zprps.Type.Trigger:
                _triggerType = (triggerType)EditorGUILayout.EnumPopup("triggerType", _triggerType);
                _matWall = (matWalls.Type)EditorGUILayout.EnumPopup("matWall", _matWall);
                ui.OnGUI_SynCellType((cel1l.Type)_triggerType);
                ui.OnGUI_SynOptions(new f4((float)_matWall, _opt2, _opt3, _opt4));
                break;
            case zprps.Type.Door:
                _doorType = (doorType)EditorGUILayout.EnumPopup("doorType", _doorType);
                _doorState = (zDoor.State)EditorGUILayout.EnumPopup("doorState", _doorState);
                _opt2 = EditorGUILayout.FloatField("delay(sec):", _opt2);
                ui.OnGUI_SynCellType((cel1l.Type)_doorType);
                ui.OnGUI_SynOptions(new f4((float)_doorState, _opt2, _opt3, _opt4));
                break;
            case zprps.Type.Key:
                _keyType = (keyType)EditorGUILayout.EnumPopup("keyType", _keyType);
                ui.OnGUI_SynCellType((cel1l.Type)_keyType);
                break;
            case zprps.Type.Bomb:
                _opt1 = EditorGUILayout.FloatField("count(-1:forever):", _opt1);
                _opt2 = EditorGUILayout.FloatField("delay(sec):", _opt2);
                _opt3 = EditorGUILayout.FloatField("num:", _opt3);
                _bombState = (zBomb.Type)EditorGUILayout.EnumPopup("state", _bombState);
                ui.OnGUI_SynCellType(cel1l.Type.Bomb);
                ui.OnGUI_SynOptions(new f4(_opt1, _opt2, _opt3, (float)_bombState + 0.4f));
                break;
            case zprps.Type.Ammo:
                _opt1 = EditorGUILayout.FloatField("count(-1:forever):", _opt1);
                _opt2 = EditorGUILayout.FloatField("delay(sec):", _opt2);
                _opt3 = EditorGUILayout.FloatField("ammo1:", _opt3);
                _opt4 = EditorGUILayout.FloatField("ammo2:", _opt4);
                ui.OnGUI_SynCellType(cel1l.Type.Ammo);
                ui.OnGUI_SynOptions(new f4(_opt1, _opt2, _opt3, _opt4));
                break;
            case zprps.Type.Potion:
                _opt1 = EditorGUILayout.FloatField("count(-1:forever):", _opt1);
                _opt2 = EditorGUILayout.FloatField("delay:", _opt2);
                ui.OnGUI_SynCellType(cel1l.Type.Potion);
                ui.OnGUI_SynOptions(new f4(_opt1, _opt2, _opt3, _opt4));
                break;

            case zprps.Type.Spin:
                _opt1 = EditorGUILayout.FloatField("speed:", _opt1);
                ui.OnGUI_SynCellType(cel1l.Type.Spin);
                ui.OnGUI_SynOptions(new f4(_opt1, 0, 0, 0));
                break;

        }
    }

    enum areaType
    {
        AreaWin = cel1l.Type.AreaWin,
        AreaWayPoint,
        AreaUpLadder,
        AreaDownLadder,
    }

    enum triggerType {
        TrigCircleBomb = cel1l.Type.TrigCircleBomb,
        Trig0, Trig1, Trig2, Trig3, Trig4, Trig5,
    }

    enum doorType
    {
        Door1 = cel1l.Type.Door1,
        Door2,
        Door3,
        Door4,
        Door5,
        Door6,
    }

    enum keyType
    {
        key1 = cel1l.Type.Key1,
        key2,
        key3,
        key4,
        key5,
        key6,
    }
    
}
