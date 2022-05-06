using CodeStage.AntiCheat.ObscuredTypes;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(uj_editmap))]
public class uj_editmapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        uj_editmap ui_panel = (uj_editmap)target;

        ui_panel.OnGUI_StateComponent();
        ui_panel.OnGUI_Mode();

        base.OnInspectorGUI();
    }
}

[CustomEditor(typeof(js))]
public class jsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        js ui_panel = (js)target;

        //  ui_panel.OnGUI_StateComponent();
        //  ui_panel.OnGUI_Mode();

        GUILayout.BeginHorizontal();
        
        if (ui_panel.gameObject.activeSelf && GUILayout.Button("Menu") && uis.IsEnableBtnTime(1.7f))
        {
            core.Inst.flowMgr.Change<Flow_Menu>();
        }
        else if (GUILayout.Button("Edit") && uis.IsEnableBtnTime(1.7f))
        {
            uis.outgam.Inactive();
            core.Inst.flowMgr.Change<Flow_EditMap>();
        }

        if (GUILayout.Button("New"))
            core.Inst.flowMgr.Change<Flow_EditMap>();

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Stage File: ");
        int stageIdx = jsData.stageFileName;
        if (GUILayout.Button("Load"))
            ui_panel.ReadJSON(core.stages.GetJson(stageIdx), js.SeriType.WithLoad);
        if (GUILayout.Button("Save")) {
            FileIO.Local.Write(core.stages.GetName(stageIdx), js.Inst.CreateJSON(), "txt");
            Debug.Log("[Saved Stage File] " + stageIdx);
        }
        jsData.stageFileName = EditorGUILayout.IntField(stageIdx);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Label("Edit File: ");
        if (GUILayout.Button("Load"))
            ui_panel.ReadJSON(FileIO.Local.Read(jsData.editFileName, "txt"), js.SeriType.WithLoad);
        if (GUILayout.Button("Save")) {
            FileIO.Local.Write(jsData.editFileName, js.Inst.CreateJSON(), "txt");
            Debug.Log("[Saved Edit File] " + jsData.editFileName);
        }
        jsData.editFileName = EditorGUILayout.TextField(jsData.editFileName);
        GUILayout.EndHorizontal();



        base.OnInspectorGUI();
    }
}

