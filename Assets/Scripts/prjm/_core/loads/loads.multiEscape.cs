using System.Collections;
using UnityEngine;
using UnityEngineEx;

public partial class loads
{
    public IEnumerator FromRandMaze_Escape_(int zoneIdx, int mazeIdx, int mapIdx)
    {
        zone zn = core.zones[zoneIdx];
        zone.mz mz = zn.mzs[mazeIdx];
        i5 bd = zn.bd;
        InitMazeRandState();

        switch (mapIdx) {
            case 0: addKeys_OnMap0(zn, mz); break;
            case 1: addKeys_OnMap1(zn, mz); break;
            case 2: addKeys_OnMap2(zn, mz); break;
            case 3: addKeys_OnMap3(zn, mz); break;  }

        addDoorBomb(zn, mz);

        _lstRC.Clear();
        for (int x = bd.X0; x < bd.X1; x += mz.szR)
            for (int z = bd.Z0; z < bd.Z1; z += mz.szC)
                _lstRC.Insert(RandEx.GetN(_lstRC.Count), new i2(x, z));

        yield return null;
        loadLambs_OnZoneCorner(bd, mz);
        loadExtraWalls(zn, mz.szR, mz.szC);
        loadExtraBoxes(zn, mz);
        loadExtraPlates(zn, mz, 75, 4);
        loadExtraBushs(zn, mz);
    }

    void addDoorBomb(zone zn, zone.mz mz)
    {
        switch (zn.idx)
        {
            case 2: case 8: case 10:
                zn.AddPrp(zn.waycells[1], zprps.eDoor, cel1l.Type.Door1, f4.O);
                break;
            case 4:
                zn.AddPrp(zn.waycells[1], zprps.eDoor, cel1l.Type.Door1, f4.O);
                zn.AddPrp(mz.bdR(RandEx.GetN(1, mz.numR - 1)), mz.bdC(RandEx.GetN(1, mz.numC - 1)), zprps.eBomb, cel1l.Type.Bomb, new f4(1, 1, 1));
                break;
            case 6:
                zn.AddPrp(zn.waycells[1], zprps.eDoor, cel1l.Type.Door1, f4.O);
                zn.AddPrp(mz.bdR(RandEx.GetN(1, mz.numR - 1)), mz.bdC(RandEx.GetN(1, mz.numC - 1)), zprps.eBomb, cel1l.Type.Bomb, new f4(1, 1, 1));
                break;
        }
    }

    void addKeys_OnMap0(zone zn, zone.mz mz)
    {
        switch (zn.idx) {
            case 2: addKey_Left(zn, mz); break;
            case 4: addKey_Botton(zn, mz); break;
            case 6: addKey_Right(zn, mz); break;
            case 8: addKey_Right(zn, mz); break;
            case 10: addKey_Botton(zn, mz); break;
        }
    }

    void addKeys_OnMap1(zone zn, zone.mz mz)
    {
        switch (zn.idx) {
            case 2: addKey_Right(zn, mz); break;
            case 4: addKey_Botton(zn, mz); break;
            case 6: addKey_Left(zn, mz); break;
            case 8: addKey_Left(zn, mz); break;
            case 10: addKey_Botton(zn, mz); break;
        }
    }

    void addKeys_OnMap2(zone zn, zone.mz mz)
    {
        switch (zn.idx) {
            case 2: addKey_Left(zn, mz); break;
            case 4: addKey_Left(zn, mz); break;
            case 6: addKey_Botton(zn, mz); break;
            case 8: addKey_Botton(zn, mz); break;
            case 10: addKey_Right(zn, mz); break;
        }
    }

    void addKeys_OnMap3(zone zn, zone.mz mz)
    {
        switch (zn.idx) {
            case 2: addKey_Right(zn, mz); break;
            case 4: addKey_Right(zn, mz); break;
            case 6: addKey_Botton(zn, mz); break;
            case 8: addKey_Botton(zn, mz); break;
            case 10: addKey_Left(zn, mz); break;
        }
    }

    void addKey_Forward(zone zn, zone.mz mz)
    {
        zn.AddPrp(mz.bdR(RandEx.GetN(1, mz.numR - 2)), mz.bdC(mz.numC - 1), zprps.eKey, cel1l.Type.Key1, f4.O);
    }

    void addKey_Botton(zone zn, zone.mz mz)
    {
        zn.AddPrp(mz.bdR(RandEx.GetN(1, mz.numR - 2)), mz.bdC(0), zprps.eKey, cel1l.Type.Key1, f4.O);
    }

    void addKey_Left(zone zn, zone.mz mz)
    {
        zn.AddPrp(mz.bdR(0), mz.bdC(RandEx.GetN(3, mz.numC - 2)), zprps.eKey, cel1l.Type.Key1, f4.O);
    }

    void addKey_Right(zone zn, zone.mz mz)
    {
        zn.AddPrp(mz.bdR(mz.numR - 1), mz.bdC(RandEx.GetN(3, mz.numC - 2)), zprps.eKey, cel1l.Type.Key1, f4.O);
    }

    public IEnumerator Mob_MultiEscape_(int zoneIdx, int mazeIdx, bool first)
    {
        zone zn = core.zones[zoneIdx];
        zone.mz mz = zn.mzs[mazeIdx];
        int xc = mz.bdR(mz.numR >> 1);
        int zc = mz.bdC(mz.numC >> 1);

        yield return null;
        int idx = 0;
        switch (zoneIdx)
        {
            case 2:
                idx = 0;
                break;
            case 4:
                idx = 2;
                break;
            case 6:
                idx = 0;
                break;
            case 8:
                idx = 2;
                break;
            case 10:
                idx = 0;
                break;
        }

        zn.Mobs.Enqueue(new iff(idx, xc + 0.5f, zc + 0.5f));

        if (zn.waycells[0].Z0 == mz.bd.Z0)
        {

        }
        else if (zn.waycells[0].X0 == mz.bd.X0)
        {
        }
        else if (zn.waycells[0].X0 == mz.bd.X1)
        {
        }
    }

    public void Bos_MultiBattle(zone zn)
    {
        zone.mz mz = zn.mzs[0];
        int xc = mz.bdR(mz.numR >> 1);
        int zc = mz.bdC(mz.numC >> 1);
        boltBos.SpawnPos = new Vector3(xc + 0.5f, 0, zc + 0.5f);
    }
}
