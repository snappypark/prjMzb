using UnityEngine;
using UnityEditor;

public class abs_uj_wallsRectEditor : Editor
{
    protected wallType _wallType = wallType.Wall;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _wallType = (wallType)EditorGUILayout.EnumPopup("wallType", _wallType);
    }
    
    protected enum wallType : byte
    {
        Wall = cel1l.Type.Wall,
    }
}

[CustomEditor(typeof(uj_wallsSingle))]
public class uj_wallsEditor : abs_uj_wallsRectEditor
{
    public override void OnInspectorGUI()
    {
        uj_wallsSingle ui_panel = (uj_wallsSingle)target;
        base.OnInspectorGUI();
        ui_panel.OnGUI_SetWallInfo((cel1l.Type)_wallType);
    }
}


[CustomEditor(typeof(uj_wallsRect))]
public class js_wallsRectEditor : abs_uj_wallsRectEditor
{
    public override void OnInspectorGUI()
    {
        uj_wallsRect ui_panel = (uj_wallsRect)target;
        base.OnInspectorGUI();
        ui_panel.OnGUI_SetWallInfo((cel1l.Type)_wallType);
    }
}

[CustomEditor(typeof(uj_wallsMaze))]
public class js_wallsMazeEditor : abs_uj_wallsRectEditor
{
    public override void OnInspectorGUI()
    {
        uj_wallsMaze ui_panel = (uj_wallsMaze)target;
        base.OnInspectorGUI();
        ui_panel.OnGUI_SetWallInfo((cel1l.Type)_wallType);
    }
}
