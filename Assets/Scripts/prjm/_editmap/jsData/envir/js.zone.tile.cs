using UnityEngine;

public partial class js /*.zone.tile*/
{
    public void AddTileR(zone_ zn, float f0, float f1, float f2, float f3, byte matId, SeriType seriType)
    {
        zn.tileRs.Add(new js.tileR_(new f4(f0, f1, f2, f3), matId));
        if (seriType == SeriType.WithLoad)
            zn.zn.AddTile_l1lRot45(new f4(f0, f1, f2, f3), matId);
    }

    public void AddTile1(zone_ zn, int x0, int z0, int x1, int z1, byte matId, SeriType seriType)
    {
        zn.tile1s.Add(new js.tile_(x0, z0, x1, z1, matId));
        switch (seriType)
        {
            case SeriType.WithLoad:
                zn.zn.AddTile_l1l(x0, z0, x1, z1, matId);
                break;
            case SeriType.WithCell:
                core.zells.Set(x0, z0, x1, z1, 0, cel1l.Type.Tile, cel1l.ColliType.None);
                break;
        }
    }

    public void AddTile5(zone_ zn, int x0, int z0, int x1, int z1, byte matId, SeriType seriType)
    {
        zn.tile5s.Add(new js.tile_(x0, z0, x1, z1, matId));
        switch (seriType)
        {
            case SeriType.WithLoad:
                zn.zn.AddTile_l5l(x0, z0, x1, z1, matId);
                break;
            case SeriType.WithCell:
                core.zells.Set(x0, z0, x1, z1, 0, cel1l.Type.Tile, cel1l.ColliType.None);
                break;
        }
    }

    public void AddTileT(zone_ zn, int x0, int z0, int x1, int z1, byte matId, SeriType seriType)
    {
        zn.tileTs.Add(new js.tile_(x0, z0, x1, z1, matId));
        switch (seriType)
        {
            case SeriType.WithLoad:
                zn.zn.AddTile_lTl(x0, z0, x1, z1, matId);
                break;
            case SeriType.WithCell:
                core.zells.Set(x0, z0, x1, z1, 0, cel1l.Type.Tile, cel1l.ColliType.None);
                break;
        }
    }

    [System.Serializable]
    public class tile_
    {
        [HideInInspector] public i4 bd;
        public matTiles.Type matIdx;

        public tile_(int x0, int z0, int x1, int z1, byte matId_)
        {
            bd = new i4(x0, z0, x1, z1);
            matIdx = (matTiles.Type)matId_;
        }

    }

    [System.Serializable]
    public class tileR_
    {
        [SerializeField] public f4 opts;
        public matTiles.Type matIdx;

        public tileR_(f4 opts_, byte matId_)
        {
            opts = opts_;
            matIdx = (matTiles.Type)matId_;
        }
    }

    public void AddAllTile(byte mat)
    {
        for (int i = 0; i < zones.Count; ++i)
        {
            zone_ zn = zones[i];
            AddTileT(zn, zn.bd.X0, zn.bd.Z0, zn.bd.X1, zn.bd.Z1, mat, SeriType.WithCell);
        }
    }

}
