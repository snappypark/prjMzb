using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

public partial class loads
{
    public IEnumerator FromRandMaze_Battle_(int zoneIdx, int mazeIdx)
    {
        zone zn = core.zones[zoneIdx];
        zone.mz mz = zn.mzs[mazeIdx];
        i5 bd = zn.bd;
        InitMazeRandState();

        _lstRC.Clear();
        for (int x = bd.X0; x < bd.X1; x += mz.szR)
            for (int z = bd.Z0; z < bd.Z1; z += mz.szC)
                _lstRC.Insert(RandEx.GetN(_lstRC.Count), new i2(x, z));

        yield return null;
        if(zoneIdx >= 2 && zoneIdx <= 4)
            loadLambs_OnZoneCorner2(bd, mz);
   //     loadExtraWalls(zn, mz.szR, mz.szC);
        loadExtraBoxes(zn, mz);
        loadExtraPlates(zn, mz, 30, 1);
        loadExtraBushs(zn, mz);
    }
}
