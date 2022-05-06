using System.Collections.Generic;
using System.Collections.GenericEx;
using UnityEngine;
using UnityEngineEx;

public partial class loads
{
    int _randMazeSeed = -1;
    public void SetRandSeed(int seed_)
    {
        _randMazeSeed = seed_;
    }

    public void InitMazeRandState()
    {
        Random.InitState(_randMazeSeed);
        _randMazeSeed += 9999;
    }

    static List<i2> _lstRC = new List<i2>();

    void loadExtraWalls(zone zn, int szR, int szC)
    {
        for (int i = 0; i < _lstRC.Count; ++i)
        {
            i2 pt = _lstRC[i];
            if (isNotWallInZone(zn, core.zells[pt.x - 1, pt.z - 1]))
                zn.AddWall(pt.x, pt.z, zn.mats, cel1l.Type.Wall);
            if (isNotWallInZone(zn, core.zells[pt.x + szR, pt.z + szC]))
                zn.AddWall(pt.x + szR - 1, pt.z + szC - 1, zn.mats, cel1l.Type.Wall);
            if (isNotWallInZone(zn, core.zells[pt.x - 1, pt.z + szC]))
                zn.AddWall(pt.x, pt.z + szC - 1, zn.mats, cel1l.Type.Wall);
            if (isNotWallInZone(zn, core.zells[pt.x + szR, pt.z - 1]))
                zn.AddWall(pt.x + szR - 1, pt.z, zn.mats, cel1l.Type.Wall);
        }
    }

    bool isNotWallInZone(zone zn, cel1l c)
    {
        return zn.idx == c.zn.idx && c.type != cel1l.Type.Wall;
    }

    void loadExtraBoxes(zone zn, zone.mz mz)
    {
        for (int i = 0; i < _lstRC.Count; ++i)
        {
            i2 pt = _lstRC[i];
            zjs.rprs.Add(pt.x + RandEx.GetN(mz.hszR), pt.z + RandEx.GetN(mz.hszC), rprs.eBox, cel1l.Type.Box, f4.O);
        }
    }

    void loadExtraPlates(zone zn, zone.mz mz, int percnet, int count)
    {
        for (int i = 0; i < _lstRC.Count; ++i)
        {
            i2 pt = _lstRC[i];
            for (int c = 0; c < count; ++c)
                if(RandEx.TruePerCt(percnet))
                    zjs.rprs.Add(pt.x + RandEx.GetN(mz.hszR), pt.z + RandEx.GetN(mz.hszC), rprs.eTrap, cel1l.Type.Trap_Slow,
                        new f4(0, Random.Range(0.32f, 0.37f), Random.Range(-6.0f, 6.3f)));
        }
    }

    void loadExtraBushs(zone zn, zone.mz mz)
    {
        for (int i = 0; i < _lstRC.Count; ++i)
        {
            i2 pt = _lstRC[i];
            int xc = pt.x + mz.hszR;
            int zc = pt.z + mz.hszC;

            int randBu = Random.Range(0, 20);
            switch (randBu)
            {
                case 0:
                    for(int gap = -2; gap<=2; ++gap)
                        zjs.rprs.Add(pt.x, zc + gap, rprs.eBush, cel1l.Type.Bush, f4.O);
                    break;
                case 1:
                    for (int gap = -2; gap <= 2; ++gap)
                        zjs.rprs.Add(pt.x + mz.szR-1, zc + gap, rprs.eBush, cel1l.Type.Bush, f4.O);
                    break;
                case 2:
                    for (int gap = -2; gap <= 2; ++gap)
                        zjs.rprs.Add(xc + gap, pt.z, rprs.eBush, cel1l.Type.Bush, f4.O);
                    break;
                case 3:
                    for (int gap = -2; gap <= 2; ++gap)
                        zjs.rprs.Add(xc + gap, pt.z + mz.szC - 1, rprs.eBush, cel1l.Type.Bush, f4.O);
                    break;
            }
        }
    }

    void loadLambs_OnZoneCorner(i5 bd, zone.mz mz)
    {
        zjs.rprs.Add(bd.X0 + (mz.hszR), bd.Z0 + (mz.hszC), rprs.eLamb, cel1l.Type.LambOff, f4.O);
        zjs.rprs.Add(bd.X0 + (mz.hszR), bd.Z1 - (mz.hszC), rprs.eLamb, cel1l.Type.LambOff, f4.O);
        zjs.rprs.Add(bd.X1 - (mz.hszR), bd.Z0 + (mz.hszC), rprs.eLamb, cel1l.Type.LambOff, f4.O);
        zjs.rprs.Add(bd.X1 - (mz.hszR), bd.Z1 - (mz.hszC), rprs.eLamb, cel1l.Type.LambOff, f4.O);
    }

    void loadLambs_OnZoneCorner2(i5 bd, zone.mz mz)
    {
        zjs.rprs.Add(bd.X0 + (mz.hszR*2), bd.Z0 + (mz.hszC*2), rprs.eLamb, cel1l.Type.LambOff, f4.O);
        zjs.rprs.Add(bd.X0 + (mz.hszR*2), bd.Z1 - (mz.hszC*2), rprs.eLamb, cel1l.Type.LambOff, f4.O);
        zjs.rprs.Add(bd.X1 - (mz.hszR*2), bd.Z0 + (mz.hszC*2), rprs.eLamb, cel1l.Type.LambOff, f4.O);
        zjs.rprs.Add(bd.X1 - (mz.hszR*2), bd.Z1 - (mz.hszC*2), rprs.eLamb, cel1l.Type.LambOff, f4.O);
    }
}
