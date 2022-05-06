using System.Collections.Generic;
using UnityEngine;

public partial class js
{
    public zone_ AddZone(int x, int z, zmSZ zmz, int eSight_, SeriType seriType)
    { 
        return AddZone(x, z, x + zmz.totalGapR, z + zmz.totalGapC, 0, eSight_, seriType);
    }

    public zone_ AddZone(i4 bd, int eSight_, SeriType seriType)
    {
        return AddZone(bd.X0, bd.Z0, bd.X1, bd.Z1, 0, eSight_, seriType);
    }

    public zone_ AddZone(int x0, int z0, int x1, int z1, int y_, int eSight_, SeriType seriType)
    {
        zone_ zn_ = new zone_(x0, z0, x1, z1, y_, eSight_);
        zones.Add(zn_);
        zn_.zn = core.zones.AddZone(x0, z0, x1, z1, y_, (sights.eState)eSight_);
        if (seriType == SeriType.WithLoad)
            zn_.hud = core.huds.editmap.CreateCel1lArea(zn_.bd, Color.cyan, 0.2f, "zone(Clone)");
        return zn_;
    }

    public bool IsCollided(i5 area)
    {
        for (int i = 0; i < zones.Count; ++i)
            if (zones[i].bd.IsCollided(area))
                return true;
        return false;
    }

    public bool FindZoneByIdx(int idx, out zone_ z)
    {
        for (int i = 0; i < zones.Count; ++i)
            if (i == idx)
            {
                z = zones[i];
                return true;
            }
        Debug.Log("not found zone");
        z = null;
        return false;
    }

    public bool FindZoneByArea(i5 area, out zone_ z)
    {
        for (int i = 0; i < zones.Count; ++i)
            if (zones[i].bd.IsInside(area))
            {
                z = zones[i];
                return true;
            }
        Debug.Log("not found zone");
        z = null;
        return false;
    }

    [System.Serializable]
    public class zone_
    {
        [HideInInspector] public i5 bd;
        [SerializeField] public sights.eState eSight = sights.eState.ByCtrlUnit;
        
        public zone_(int x0, int z0, int x1, int z1, int y_, int eSight_)
        {
            bd = new i5(x0, z0, x1, z1, y_);
            eSight = (sights.eState)eSight_;
            zn = zone.Empty;
            hud = null;// core.huds.editmap.CreateCel5lArea(b5d, Color.cyan, 0.2f, "zone(Clone)");
        }

        public zone_()
        {
            bd = zone.Empty.bd;
            eSight = zone.Empty.sight;
            zn = zone.Empty;
            hud = null;// core.huds.editmap.CreateCel5lArea(b5d, Color.cyan, 0.2f, "zone(Clone)");
        }

        [SerializeField] public List<zone.Option> options = new List<zone.Option>();

        [SerializeField] public List<tileR_> tileRs = new List<tileR_>();
        [SerializeField] public List<tile_> tile1s = new List<tile_>();
        [SerializeField] public List<tile_> tile5s = new List<tile_>();
        [SerializeField] public List<tile_> tileTs = new List<tile_>();

        [SerializeField] public List<zprp_> prps = new List<zprp_>();
        [SerializeField] public List<wallsMaze_> wallsMazes = new List<wallsMaze_>();
        [SerializeField] public List<wallsRect_> wallsRects = new List<wallsRect_>();
        [SerializeField] public List<wallsRect_> wallsLines = new List<wallsRect_>();
        [SerializeField] public List<wallSingle_> walls = new List<wallSingle_>();
        [SerializeField] public List<wall_Del_> wallsDel = new List<wall_Del_>();
        [SerializeField] public List<wallBg> wallsBg = new List<wallBg>();

        [SerializeField] public List<npc_> npcs = new List<npc_>();

        [SerializeField] public List<zbg_> zbgs = new List<zbg_>();

        [SerializeField] public List<rpr_> rprs = new List<rpr_>();

        [HideInInspector] public hudLine hud = null;
        [HideInInspector] public zone zn = null;
    }
}
