using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

public partial class uj_AllStgs
{
    void generateBlockWalls(js.zone_ zn, int szR, int szC)
    {
        if (!_bwall.Active || zn.zn.idx != _bwall.ZnIdx || cnd.pathMiddleNodes.Count == 0)
            return;

        i4 bcd = cnd.pathMiddleNodes[0].GetSideToPrevious(zn);
        js.Inst.AddWallsLine(zn, bcd, wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.WithCell);
        
        js.zone_ tl = js.Inst.zones[zn.zn.idx - 1];
        js.Inst.AddZprop(zn, tl.bd.xMidd, tl.bd.zMidd, zprps.eBomb, cel1l.Type.Bomb, new f4(1, 1.1f, 1), js.SeriType.WithCell);
    }

    void generateBushs(js.zone_ zn, int szR, int szC)
    {
        if (!_bush.Active || zn.zn.idx != _bush.ZnIdx || cnd.pathMiddleNodes.Count == 0)
            return;

        i4 bcd = cnd.pathMiddleNodes[0].GetSideToPrevious(zn);
        for (int x = bcd.X0; x <= bcd.X1; ++x)
            for (int z = bcd.Z0; z <= bcd.Z1; ++z)
            {
                if(core.zells[x, z].type == cel1l.Type.Tile)
                    js.Inst.AddRpr(zn, x,z, rprs.eBush, cel1l.Type.Bush, f4.O, js.SeriType.WithCell);
            }
    }


    void generateTraps(js.zone_ zn, ref cnd[,] cellnodes)
    {
        if (!_trap.Active)
            return;

        // path
        int maxTrap = _trap.Max;
        int cntNode = 0, cntTrap = 0;
        while (cntNode < cnd.pathNodes.Count && cntTrap < maxTrap)
        {
            cnd cnd = cnd.pathNodes[cntNode++];

            if (js.Inst.AddFire(zn, cnd.GetCenter(zn), cel1l.Type.Trap_Fire))
                cntTrap++;

            if (RandEx.IsTrue_Probability(50) &&
                js.Inst.AddFire(zn, cnd.GetCenter(zn), cel1l.Type.Trap_FireWall))
                cntTrap++;

            if (RandEx.IsTrue_Probability(25) &&
                js.Inst.AddFire(zn, cnd.GetCenter(zn), cel1l.Type.Trap_FireWall))
                cntTrap++;
        }

        // random
        maxTrap = Random.Range(2, 5) + _trap.Max * 2; // 1 ~ 17, 
        cntNode = 0; cntTrap = 0;
        while (cntNode < cnd.Nodes.Count && cntTrap < maxTrap)
        {
            cnd cnd = cnd.Nodes[cntNode++];

            if (RandEx.IsTrue_Probability(90) &&
                js.Inst.AddFire(zn, cnd.GetCenter(zn), cel1l.Type.Trap_Fire))
                cntTrap++;

            if (RandEx.IsTrue_Probability(60) &&
                js.Inst.AddFire(zn, cnd.GetCenter(zn), cel1l.Type.Trap_FireWall))
                cntTrap++;
        }
    }

    void generateMeteor(js.zone_ zn)
    {
        if (_meteor.Active && zn.zn.idx == _meteor.ZnIdx)
            zn.options.Add(zone.Option.Meteor);
        else if (_meteor2.Active && (zn.zn.idx == 3 || zn.zn.idx == 5))
            zn.options.Add(zone.Option.Meteor);
    }
}
