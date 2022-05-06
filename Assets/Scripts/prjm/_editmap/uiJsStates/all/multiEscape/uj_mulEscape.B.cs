using UnityEngineEx;

public partial class uj_mulEscape
{
    void genB_1()
    {
        zmSZ[] zmsz = new zmSZ[] { new zmSZ(new zmSz(5, 3, 14), new zmSz(5, 3, 14)), getZmSZ(), getZmSZ(), getZmSZ(), getZmSZ(), getZmSZ() };


        #region zones
        int x = 50, z = 10;
        js.zone_ z0 = js.Inst.AddZone(x, z, zmsz[0], 1, js.SeriType.WithCell);
        js.zone_ t0 = tunnelForward(z0, zmsz[0], 1);

        js.zone_ z1 = znForward(t0, zmsz[1], RandEx.GetN(10, 20));
        js.zone_ t1 = tunnelForward(z1, zmsz[1], RandEx.GetN(1, 4));
        
        js.zone_ z2 =  znForward(t1, zmsz[2], RandEx.GetN(10, 20));
        js.zone_ t2 = tunnelRight(z2, zmsz[2], RandEx.GetN(3, 6));

        // center

        ////
        x = t2.bd.X1 + 1; z = z2.bd.Z0 - zmsz[3].szGapC;
        js.zone_ z3 = js.Inst.AddZone(x, z, zmsz[3], 1, js.SeriType.WithCell);
        js.zone_ t3 = tunnelRight(z3, zmsz[3], RandEx.GetN(3, 6));

        x = t3.bd.X1 + 1; z = z3.bd.Z0 - zmsz[4].szGapC;
        js.zone_ z4 = js.Inst.AddZone(x, z, zmsz[4], 1, js.SeriType.WithCell);
        js.zone_ t4 = tunnelForward(z4, zmsz[4], RandEx.GetN(1, 4));
        
        js.zone_ z5 = znForward(t4, zmsz[5], RandEx.GetN(10, 20));
        js.zone_ t5 = tunnelForward(z5, zmsz[5], RandEx.GetN(1, 3));

        js.zone_ z6l = znForward(t5, zmsz[0], zmsz[0].szGapR);
        #endregion

        setDelsToF(t0);
        setDelsToF(t1);

        setDelsToR(t2);
        setDelsToR(t3);
        setDelsToF(t4);
        setDelsToF(t5);

        genA_Common(ref zmsz, z0, z1, z2, z3, z4, z5, z6l, t0, t1, t2, t3, t4, t5);
    }

    void genB_2()
    {
        zmSZ[] zmsz = new zmSZ[] { new zmSZ(new zmSz(5, 3, 14), new zmSz(5, 3, 14)), getZmSZ(), getZmSZ(), getZmSZ(), getZmSZ(), getZmSZ() };


        #region zones
        int x = 220, z = 10;
        js.zone_ z0 = js.Inst.AddZone(x, z, zmsz[0], 1, js.SeriType.WithCell);
        js.zone_ t0 = tunnelForward(z0, zmsz[0], 1);

        js.zone_ z1 = znForward(t0, zmsz[1], RandEx.GetN(10, 20));
        js.zone_ t1 = tunnelForward(z1, zmsz[1], RandEx.GetN(1, 4));

        js.zone_ z2 = znForward(t1, zmsz[2], RandEx.GetN(10, 20));
        js.zone_ t2 = tunnelLeft(z2, zmsz[2], RandEx.GetN(3, 6));

        // center
        x = t2.bd.X0 - (zmsz[3].totalGapR + 1); z = z2.bd.Z0 - zmsz[3].szGapC;
        js.zone_ z3 = js.Inst.AddZone(x, z, zmsz[3], 1, js.SeriType.WithCell);
        js.zone_ t3 = tunnelLeft(z3, zmsz[3], RandEx.GetN(3, 6));

        x = t3.bd.X0 - (zmsz[4].totalGapR + 1); z = z3.bd.Z0 - zmsz[4].szGapC;
        js.zone_ z4 = js.Inst.AddZone(x, z, zmsz[4], 1, js.SeriType.WithCell);
        js.zone_ t4 = tunnelForward(z4, zmsz[4], RandEx.GetN(1, 4));

        js.zone_ z5 = znForward(t4, zmsz[5], RandEx.GetN(10, 20));
        js.zone_ t5 = tunnelForward(z5, zmsz[5], RandEx.GetN(1, 3));

        js.zone_ z6l = znForward(t5, zmsz[0], zmsz[0].szGapR);
        #endregion

        setDelsToF(t0);
        setDelsToF(t1);

        setDelsToL(t2);
        setDelsToL(t3);
        setDelsToF(t4);
        setDelsToF(t5);

        genA_Common(ref zmsz, z0, z1, z2, z3, z4, z5, z6l, t0, t1, t2, t3, t4, t5);
    }
}
