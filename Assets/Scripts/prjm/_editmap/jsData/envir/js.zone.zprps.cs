using UnityEngine;

public partial class js
{
    public void AddZprop(zone_ zn, int x0, int z0, byte objType_, cel1l.Type cellType_, f4 opt_, SeriType seriType)
    {
        AddZprop(zn, x0, z0, x0, z0, objType_, cellType_, opt_, seriType);
    }

    public void AddZprop(zone_ zn, i4 bd, byte objType_, cel1l.Type cellType_, f4 opt_, SeriType seriType)
    {
        AddZprop(zn, bd.X0, bd.Z0, bd.X1, bd.Z1, objType_, cellType_, opt_, seriType);
    }

    public void AddZprop(zone_ zn, int x0, int z0, int x1, int z1, byte objType_, cel1l.Type cellType_, f4 opt_, SeriType seriType)
    {
        zn.prps.Add(new zprp_(x0, z0, x1, z1, objType_, cellType_, opt_));
        if(seriType == SeriType.WithLoad)
            zn.zn.AddPrp(x0, z0, x1, z1, objType_, cellType_, opt_);
    }

    [System.Serializable]
    public class zprp_
    {
        [HideInInspector] public i4 bd;
        [SerializeField] public zprps.Type oType;
        [HideInInspector] public cel1l.Type cellType;
        [SerializeField] public f4 opts;
        
        public zprp_(int x0, int z0, int x1, int z1, byte objType_, cel1l.Type cellType_, f4 opts_)
        {
            bd = new i4(x0, z0, x1, z1);
            oType = (zprps.Type)objType_;
            cellType = cellType_;
            opts = opts_;
        }
    }



    public void AddZprp_ArrowByExit(js.zone_ zn)
    {
        i4 bd = zn.wallsDel[1].bd;
        cel1ls.NearZnType type = cel1ls.NearZoneType(core.zells[bd.X0, bd.Z0]);
        f4 opt = new f4((float)type + 0.1f, 0, 0, 0);
        AddZprop(zn, bd, zprps.eArrow, cel1l.Type.Tile, opt, js.SeriType.Default);
    }
}
