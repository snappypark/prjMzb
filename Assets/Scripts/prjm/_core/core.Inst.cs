using UnityEngine;

public partial class core /*.Inst*/
{
    public static core Inst;

    public static loads loads;
    public static stages stages;
    public static multis multis;

    public static sights sights;
    public static huds huds;

    public static collis collis;

    public static cel1ls zells;
    public static zones zones;

    public static units units;
    public static unitClones unitClones;

    void Awake()
    {
        cel1l.Empty.zn = zone.Empty;

        Inst = this;

        loads = loads.Inst; stages = stages.Inst; multis = multis.Inst;

        zones = zones.Inst;
        zells = cel1ls.Inst;
        collis = collis.Inst;
        
        units = units.Inst;
        unitClones = unitClones.Inst;
    }

    public static void ClearZz()
    {
        zells.Clear();
        zones.Clear();
    }
}
