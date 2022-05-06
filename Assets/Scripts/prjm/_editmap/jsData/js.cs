using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections.Generic;
using UnityEngine;

public partial class js : MonoBehaviour
{
    public static js Inst;
    private void Awake() { Inst = this; }

    [SerializeField] public info_ info;

    [SerializeField] public List<zone_> zones;
    [SerializeField] public List<spawn_> spawns = new List<spawn_>();

    public void Clear()
    {
        core.huds.editmap.Clear();
        ctrls.Inst.ClearSpawns();
        for (int i = 0; i < zones.Count; ++i)
        {
            zones[i].options.Clear();
            zones[i].tile1s.Clear();
            zones[i].tile5s.Clear();
            zones[i].tileRs.Clear();
            zones[i].tileTs.Clear();
            zones[i].npcs.Clear();
            zones[i].prps.Clear();
            zones[i].rprs.Clear();
            zones[i].walls.Clear();
            zones[i].wallsBg.Clear();
            zones[i].wallsDel.Clear();
            zones[i].wallsLines.Clear();
            zones[i].wallsMazes.Clear();
            zones[i].wallsRects.Clear();
            zones[i].zbgs.Clear();
        }
        spawns.Clear();
        zones.Clear();
    }

    void OnEnable()
    {
        jsData.Load();
    }

    void OnDisable()
    {
        Clear();
        jsData.SaveAll();
    }
    
    [System.Serializable]
    public class info_
    {
        [SerializeField] public multis.Mode mode;
        [SerializeField] public loads.TypeBg soloType = loads.TypeBg.Default;
        [SerializeField] public int remainSec = 140;
        // [SerializeField] public float terrainHeight = 10.0f;
    }

    public enum SeriType {
        Default = 0,
        WithCell,
        WithLoad,
    }

    public enum WriteType {
        Default = 0,
        NextVersion,
    }
}

public static class jsData
{
    public static int stageFileName = 0;
    public static string editFileName = "s0";

    public static void Load()
    {
        stageFileName = ObscuredPrefs.GetInt("jsdatastagename", 0);
        editFileName = ObscuredPrefs.GetString("jsdatafilename", "s0");
    }

    public static void SaveAll()
    {
        ObscuredPrefs.SetInt("jsdatastagename", stageFileName);
        ObscuredPrefs.SetString("jsdatafilename", editFileName);


        ObscuredPrefs.Save();
    }
}

public static class jsK
{
    public const string Map = "mp";
    public const string Info = "inf";
    public const string Zones = "zn";
    public const string Prps = "pr";
    public const string Npcs = "np";
    public const string Links = "ri";
    public const string Spawns = "snw";
    public const string Hero = "he";

    public static JSONObject ObjMap = new JSONObject();
    public static JSONObject ObjInfo = new JSONObject();

    public static JSONObject ObjZones = new JSONObject();
    public static JSONObject ObjPrps = new JSONObject();
    public static JSONObject ObjNpcs = new JSONObject();

    public static JSONObject ObjSpwan = new JSONObject();

    public static JSONObject ObjLinks = new JSONObject();

    public static JSONObject ObjHero = new JSONObject();

}

