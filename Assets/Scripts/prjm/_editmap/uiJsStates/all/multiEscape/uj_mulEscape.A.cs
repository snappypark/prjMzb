using UnityEngineEx;

public partial class uj_mulEscape
{
    byte _matTile = matTiles.Green;
    wal4Mat _matWal = wal4Mat.N(matWalls.GreenLight);

    void genA_1()
    {
        zmSZ[] zmsz = new zmSZ[] { new zmSZ(new zmSz(5,3,14), new zmSz(5,3,14)), getZmSZ(), getZmSZ(), getZmSZ(), getZmSZ(), getZmSZ() };

        #region zones
        int x = 50, z = 10;
        js.zone_ z0 = js.Inst.AddZone(x, z, zmsz[0], 1, js.SeriType.WithCell);
        js.zone_ t0 = tunnelForward(z0, zmsz[0], 1);
        
        js.zone_ z1 = znForward(t0, zmsz[1], RandEx.GetN(10, 20));
        js.zone_ t1 = tunnelRight(z1, zmsz[1], RandEx.GetN(2, 4));

        x = t1.bd.X1 + 1; z = z1.bd.Z0;
        js.zone_ z2 = js.Inst.AddZone(x, z, zmsz[2], 1, js.SeriType.WithCell);
        js.zone_ t2 = tunnelRight(z2, zmsz[2], RandEx.GetN(2, 4));

        // center
        x = t2.bd.X1 + 1; z = t2.bd.Z0 - zmsz[3].szGapC;
        js.zone_ z3 = js.Inst.AddZone(x, z, zmsz[3], 1, js.SeriType.WithCell);
        js.zone_ t3 = tunnelForward(z3, zmsz[3], RandEx.GetN(1, 4));
        
        js.zone_ z4 = znForward(t3, zmsz[4], RandEx.GetN(3, 6) * zmsz[4].szGapR);
        js.zone_ t4 = tunnelLeft(z4, zmsz[4], RandEx.GetN(1, 4));

        x = t4.bd.X0 - (zmsz[5].totalGapR+1); z = z4.bd.Z0;
        js.zone_ z5 = js.Inst.AddZone(x, z, zmsz[5], 1, js.SeriType.WithCell);
        js.zone_ t5 = tunnelForward(z5, zmsz[5], RandEx.GetN(1, 3));
        
        js.zone_ z6l = znForward(t5, zmsz[0], zmsz[0].szGapR);
        #endregion

        setDelsToF(t0);
        setDelsToR(t1);
        setDelsToR(t2);
        setDelsToF(t3);
        setDelsToL(t4);
        setDelsToF(t5);

        genA_Common(ref zmsz, z0, z1, z2, z3, z4, z5, z6l, t0, t1, t2, t3, t4, t5);
    }

    void genA_2()
    {
        zmSZ[] zmsz = new zmSZ[] { new zmSZ(new zmSz(5, 3, 14), new zmSz(5, 3, 14)), getZmSZ(), getZmSZ(), getZmSZ(), getZmSZ(), getZmSZ() };

        #region zones
        int x = 220, z = 10;
        js.zone_ z0 = js.Inst.AddZone(x, z, zmsz[0], 1, js.SeriType.WithCell);
        js.zone_ t0 = tunnelForward(z0, zmsz[0], 1);

        js.zone_ z1 = znForward(t0, zmsz[1], RandEx.GetN(15, 25));
        js.zone_ t1 = tunnelLeft(z1, zmsz[1], RandEx.GetN(1, 4));

        x = t1.bd.X0 - (zmsz[2].totalGapR + 1); z = z1.bd.Z0;
        js.zone_ z2 = js.Inst.AddZone(x, z, zmsz[2], 1, js.SeriType.WithCell);
        js.zone_ t2 = tunnelLeft(z2, zmsz[2], RandEx.GetN(1, 4));

        // center
        x = t2.bd.X0 - (zmsz[3].totalGapR + 1); z = t2.bd.Z0 - zmsz[3].szGapC;
        js.zone_ z3 = js.Inst.AddZone(x, z, zmsz[3], 1, js.SeriType.WithCell);
        js.zone_ t3 = tunnelForward(z3, zmsz[3], RandEx.GetN(4, 7));

        js.zone_ z4 = znForward(t3, zmsz[4], RandEx.GetN(3, 6) * zmsz[4].szGapR);
        js.zone_ t4 = tunnelRight(z4, zmsz[4], RandEx.GetN(2, 4));

        x = t4.bd.X1 + 1; z = z4.bd.Z0;
        js.zone_ z5 = js.Inst.AddZone(x, z, zmsz[5], 1, js.SeriType.WithCell);
        js.zone_ t5 = tunnelForward(z5, zmsz[5], RandEx.GetN(5, 7));

        js.zone_ z6l = znForward(t5, zmsz[0], zmsz[0].szGapR);
        #endregion

        setDelsToF(t0);
        setDelsToL(t1);
        setDelsToL(t2);
        setDelsToF(t3);
        setDelsToR(t4);
        setDelsToF(t5);

        genA_Common(ref zmsz, z0, z1, z2, z3, z4, z5, z6l, t0, t1, t2, t3, t4, t5);
    }

    void genA_Common(ref zmSZ[] zmsz, js.zone_ z0, js.zone_ z1, js.zone_ z2, js.zone_ z3, js.zone_ z4, js.zone_ z5, js.zone_ z6l,
                                        js.zone_ t0, js.zone_ t1, js.zone_ t2, js.zone_ t3, js.zone_ t4, js.zone_ t5)
    {
        setDoor(z0);

        js.Inst.AddAllTile(matTiles.Green);

        for (int i = 0; i < 6; ++i) { // tunnel
            js.zone_ tl = js.Inst.zones[1 + i * 2];
            js.Inst.AddWallsRect(tl, tl.bd.i4, _matWal, cel1l.Type.Wall, js.SeriType.WithCell);
         }

        for (int i = 0; i < 5; ++i)
        { // tunnel
            js.zone_ tl = js.Inst.zones[1 + i * 2];
            js.Inst.AddZprop(tl, tl.bd.xMidd, tl.bd.zMidd, zprps.eAmmo, cel1l.Type.Ammo, new f4(1, 1.1f, 20, 30), js.SeriType.WithCell);
        }
        js.Inst.AddWallsRect(z0, z0.bd.i4, _matWal, cel1l.Type.Wall, js.SeriType.WithCell);
        js.Inst.AddWallsRect(z6l, z6l.bd.i4, _matWal, cel1l.Type.Wall, js.SeriType.WithCell);
        for (int i = 1; i < 6; ++i) {
            js.zone_ zn = js.Inst.zones[i * 2];
            js.Inst.AddWallsMaze(zn, zn.bd.i4, zmsz[i].szs(), zmsz[i].nums(), _matWal, cel1l.Type.Wall, 3187, 0, js.SeriType.WithCell);
        }

        i4 bfd = z6l.bd.CenterArea(1, 1);
        js.Inst.AddWallsLine(z6l, new i4(bfd.X0, bfd.Z1 + 1, bfd.X1, bfd.Z1 + 1), _matWal, cel1l.Type.Wall, js.SeriType.WithCell);
        js.Inst.AddZprop(z6l, z6l.bd.CenterArea(1, 1),
            zprps.eArea, cel1l.Type.AreaWin, f4.O, js.SeriType.WithCell);

    }

    js.zone_ znForward(js.zone_ t, zmSZ zms, int gap)
    {
        int x = t.bd.X0 - gap;
        int z = t.bd.Z1 + 1;
        i4 b1d = new i4(x, z, x + zms.totalGapR, z + zms.totalGapC);
        return js.Inst.AddZone(b1d.X0, b1d.Z0, b1d.X1, b1d.Z1, 0, 1, js.SeriType.WithCell);
    }

    int _tl = 14;
    js.zone_ tunnelLeft(js.zone_ zn, zmSZ zms, int gap)
    {
        int z = zn.bd.Z1 - gap * zms.szGapC;
        js.zone_ t = js.Inst.AddZone(zn.bd.X0 - _tl, z - zms.szGapC, zn.bd.X0 - 1, z+1, 0, 1, js.SeriType.WithCell);
        js.Inst.AddWallsDel(t, new i4(t.bd.X1, t.bd.Z0 + 1, t.bd.X1, t.bd.Z1 - 1), js.SeriType.WithCell);
        js.Inst.AddWallsDel(t, new i4(t.bd.X0, t.bd.Z0 + 1, t.bd.X0, t.bd.Z1 - 1), js.SeriType.WithCell);
        return t;
    }

    js.zone_ tunnelForward(js.zone_ zn, zmSZ zms, int gap)
    {
        int x = zn.bd.X0 + zms.szGapR * gap - 1;
        js.zone_ t = js.Inst.AddZone(x, zn.bd.Z1 + 1, x + zms.szGapR+1, zn.bd.Z1 + _tl, 0, 1, js.SeriType.WithCell);
        js.Inst.AddWallsDel(t, new i4(t.bd.X0 + 1, t.bd.Z0, t.bd.X1 - 1, t.bd.Z0), js.SeriType.WithCell);
        js.Inst.AddWallsDel(t, new i4(t.bd.X0 + 1, t.bd.Z1, t.bd.X1 - 1, t.bd.Z1), js.SeriType.WithCell);
        return t;
    }

    js.zone_ tunnelRight(js.zone_ zn, zmSZ zms, int gap)
    {
        int z = zn.bd.Z1 - gap * zms.szGapC;
        js.zone_ t = js.Inst.AddZone(zn.bd.X1 + 1, z - zms.szGapC, zn.bd.X1 + _tl, z+1, 0, 1, js.SeriType.WithCell);
        js.Inst.AddWallsDel(t, new i4(t.bd.X0, t.bd.Z0 + 1, t.bd.X0, t.bd.Z1 - 1), js.SeriType.WithCell);
        js.Inst.AddWallsDel(t, new i4(t.bd.X1, t.bd.Z0 + 1, t.bd.X1, t.bd.Z1 - 1), js.SeriType.WithCell);
        return t;
    }

    void setDelsToF(js.zone_ t)
    {
        js.Inst.AddWallsDel(js.Inst.zones[t.zn.idx - 1], t.wallsDel[0].bd.WithZ(-1), js.SeriType.WithCell);
        js.Inst.AddWallsDel(js.Inst.zones[t.zn.idx + 1], t.wallsDel[1].bd.WithZ(+1), js.SeriType.WithCell);
    }
    void setDelsToR(js.zone_ t)
    {
        js.Inst.AddWallsDel(js.Inst.zones[t.zn.idx - 1], t.wallsDel[0].bd.WithX(-1), js.SeriType.WithCell);
        js.Inst.AddWallsDel(js.Inst.zones[t.zn.idx + 1], t.wallsDel[1].bd.WithX(+1), js.SeriType.WithCell);
    }
    void setDelsToL(js.zone_ t)
    {
        js.Inst.AddWallsDel(js.Inst.zones[t.zn.idx - 1], t.wallsDel[0].bd.WithX(+1), js.SeriType.WithCell);
        js.Inst.AddWallsDel(js.Inst.zones[t.zn.idx + 1], t.wallsDel[1].bd.WithX(-1), js.SeriType.WithCell);
    }

    void setDoor(js.zone_ zn)
    {
        js.Inst.ResetSpawn(0.5f + zn.bd.X0 + (zn.bd.GapX >> 1), zn.bd.Z0 + (zn.bd.GapZ >> 1) - 1);

        i4 bdDoor = zn.bd.CenterArea(1, 0).WithZ(4);
        
        js.Inst.AddZprop(zn, bdDoor,
            zprps.eDoor, cel1l.Type.Door1, new f4(3.1f, 7,0,0), js.SeriType.WithCell);

        for(int i=0; i<3; ++i) {
            js.Inst.AddWallsSingle(zn, bdDoor.X0 - 1, bdDoor.Z0+i, _matWal, cel1l.Type.Wall, js.SeriType.WithCell);
            js.Inst.AddWallsSingle(zn, bdDoor.X1 + 1, bdDoor.Z0+i, _matWal, cel1l.Type.Wall, js.SeriType.WithCell);
        }
    }
}
