using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class uj_mulBattle
{
    void genA()
    {
        zmSZ[] zmsz = new zmSZ[] {
            new zmSZ(zmSz.s25, zmSz.s45),
            getZmSZ(), getZmSZCenter()};

        int x = 25, z = 90;
        i4 b1d = new i4(x, z, x + zmsz[0].totalGapR, z + zmsz[0].totalGapC);
        js.zone_ z1 = js.Inst.AddZone(b1d.X0, b1d.Z0, b1d.X1, b1d.Z1, 0, 1, js.SeriType.WithCell);
        js.zone_ z2 = addZn(z1, zmsz[1], 0);
        js.zone_ zc = addZn(z2, zmsz[2], -zmsz[2].szGapC);
        js.zone_ z4 = addZn(zc, zmsz[1], zmsz[1].szGapC);
        js.zone_ z5 = addZn(z4, zmsz[0], 0);


        for (int i = 0; i < js.Inst.zones.Count; ++i)
        {
            js.zone_ zn = js.Inst.zones[i];
            js.Inst.AddTileT(zn, zn.bd.X0, zn.bd.Z0, zn.bd.X1, zn.bd.Z1, matTiles.Gray, js.SeriType.WithCell);
        }

        wal4Mat mat = wal4Mat.N(matWalls.SkyDark);
        //js.Inst.AddWallsRect(z0, z0.bd.i4, mat, cel1l.Type.Wall, js.SeriType.WithCell);
        js.Inst.AddWallsRect(z1, z1.bd.i4, mat, cel1l.Type.Wall, js.SeriType.WithCell);
        js.Inst.AddWallsMaze(z2, z2.bd.i4, zmsz[1].szs(), zmsz[1].nums(), mat, cel1l.Type.Wall, 3187, 0, js.SeriType.WithCell);
        js.Inst.AddWallsMaze(zc, zc.bd.i4, zmsz[2].szs(), zmsz[2].nums(), wal4Mat.N(matWalls.PurpDark), cel1l.Type.Wall, 3187, 0, js.SeriType.WithCell);
        js.Inst.AddWallsMaze(z4, z4.bd.i4, zmsz[1].szs(), zmsz[1].nums(), mat, cel1l.Type.Wall, 3187, 0, js.SeriType.WithCell);
        js.Inst.AddWallsRect(z5, z5.bd.i4, mat, cel1l.Type.Wall, js.SeriType.WithCell);

        #region dels
        i4 d1p = getDelsRR_Up(z1, zmsz[0].szGapC);
        i4 d1b = getDelsRR_Down(z1, zmsz[0].szGapC);
        i4 d2p = getDelsRR_Up(z2, zmsz[0].szGapC);
        i4 d2b = getDelsRR_Down(z2, zmsz[0].szGapC);

        i4 d4p = getDelsLL_Up(z4, zmsz[0].szGapC);
        i4 d4b = getDelsLL_Down(z4, zmsz[0].szGapC);
        i4 d5p = getDelsLL_Up(z5, zmsz[0].szGapC);
        i4 d5b = getDelsLL_Down(z5, zmsz[0].szGapC);

        js.Inst.AddWallsDel(z1, d1p, js.SeriType.WithCell);
        js.Inst.AddWallsDel(z1, d1b, js.SeriType.WithCell);
        js.Inst.AddWallsDel(z2, d1p.WithX(1), js.SeriType.WithCell);
        js.Inst.AddWallsDel(z2, d1b.WithX(1), js.SeriType.WithCell);

        js.Inst.AddWallsDel(z2, d2p, js.SeriType.WithCell);
        js.Inst.AddWallsDel(z2, d2b, js.SeriType.WithCell);
        js.Inst.AddWallsDel(zc, d2p.WithX(1), js.SeriType.WithCell);
        js.Inst.AddWallsDel(zc, d2b.WithX(1), js.SeriType.WithCell);

        js.Inst.AddWallsDel(z5, d5p, js.SeriType.WithCell);
        js.Inst.AddWallsDel(z5, d5b, js.SeriType.WithCell);
        js.Inst.AddWallsDel(z4, d5p.WithX(-1), js.SeriType.WithCell);
        js.Inst.AddWallsDel(z4, d5b.WithX(-1), js.SeriType.WithCell);

        js.Inst.AddWallsDel(z4, d4p, js.SeriType.WithCell);
        js.Inst.AddWallsDel(z4, d4b, js.SeriType.WithCell);
        js.Inst.AddWallsDel(zc, d4p.WithX(-1), js.SeriType.WithCell);
        js.Inst.AddWallsDel(zc, d4b.WithX(-1), js.SeriType.WithCell);
        #endregion

        #region door
        setDoor(z1);
        setDoor(z5);
        #endregion

        #region spwan
        js.Inst.spawns.Clear();

        js.Inst.AddSpawn(0, z1.bd.X0+2, 0, z1.bd.Z0+2, 0, 0, 1, 0, 0, 0, js.SeriType.Default);
        js.Inst.AddSpawn(1, z5.bd.X1-2, 0, z1.bd.Z0+2, 0, 0, 1, 0, 0, 0, js.SeriType.Default);
        #endregion

    }

    js.zone_ addZn(js.zone_ zn, zmSZ zmz, int gap)
    {
        int x = zn.bd.X1 + 1;
        int z = zn.bd.Z0 + gap;
        i4 bd = new i4(x, z, x + zmz.totalGapR, z + zmz.totalGapC);
        return js.Inst.AddZone(bd.X0, bd.Z0, bd.X1, bd.Z1, 0, 1, js.SeriType.WithCell);
    }

    i4 getDelsRR_Up(js.zone_ zn, int gap) { return new i4(zn.bd.X1, zn.bd.Z1 - gap, zn.bd.X1, zn.bd.Z1 - 1); }
    i4 getDelsRR_Down(js.zone_ zn, int gap) { return new i4(zn.bd.X1, zn.bd.Z0 + 1, zn.bd.X1, zn.bd.Z0 + gap); }
    i4 getDelsLL_Up(js.zone_ zn, int gap) { return new i4(zn.bd.X0, zn.bd.Z1 - gap, zn.bd.X0, zn.bd.Z1 - 1); }
    i4 getDelsLL_Down(js.zone_ zn, int gap) { return new i4(zn.bd.X0, zn.bd.Z0 + 1, zn.bd.X0, zn.bd.Z0 + gap); }

    void setDoor(js.zone_ zn)
    {
        for (int i = 0; i < zn.wallsDel.Count; ++i)
        {
            i4 bdDoor = zn.wallsDel[i].bd;
            js.Inst.AddZprop(zn, bdDoor,
                zprps.eDoor, cel1l.Type.Door1, new f4(3.1f, 7, 0, 0), js.SeriType.WithCell);
        }

    }
}
