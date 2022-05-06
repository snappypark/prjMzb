using System.Collections;
using System.Collections.Generic;
using System.Collections.GenericEx;
using UnityEngine;
using UnityEngineEx;

public partial class uj_AllStgs
{
    #region zmSZ
    static zmSz[] _zmSzs = new zmSz[] {
        zmSz.s45, zmSz.s55, zmSz.s55, zmSz.s46,
        zmSz.s46, zmSz.s56, zmSz.s56, zmSz.s47,
        zmSz.s47, zmSz.s48, zmSz.s48, zmSz.s57,
        zmSz.s57, zmSz.s49, zmSz.s49, zmSz.s58,
        zmSz.s58, zmSz.s4T, zmSz.s4T, zmSz.s49,

        zmSz.s49, zmSz.s48,    zmSz.s49, zmSz.s58,
        zmSz.s4T, zmSz.s48,    zmSz.s4T, zmSz.s58,
        zmSz.s4T, zmSz.s4T};

    zmSZ getZmSZ(int znIdx, int stageIdx)
    {
        int by5 = stageIdx % 5;
        zmSz zmSz0 = _zmSzs[INFO.idxX100 + (stageIdx / 5)];
        zmSz zmSz1 = _zmSzs[INFO.idxX100 + (stageIdx / 5) + 1];

        if (znIdx == 0 || by5 == 0) // n1, n1, n1, n1
            return getZmN1(zmSz0, zmSz1);
        switch (by5) {
            case 1: // n1, 11, n1, n1
                switch (znIdx) {
                    case 1: return new zmSZ(zmSz1, zmSz1);
                    case 2: return getZmN1(zmSz0, zmSz1);
                    case 3: return getZmN1(zmSz0, zmSz1); }
                break;
            case 2: // n1, n1, 11, 11
                switch (znIdx) {
                    case 1: return getZmN1(zmSz0, zmSz1);
                    case 2: return new zmSZ(zmSz1, zmSz1);
                    case 3: return new zmSZ(zmSz1, zmSz1); }
                break;
            case 3: // n1, 11, 11, n1
                switch (znIdx) {
                    case 1: return new zmSZ(zmSz1, zmSz1);
                    case 2: return new zmSZ(zmSz1, zmSz1);
                    case 3: return getZmN1(zmSz0, zmSz1); }
                break;
            case 4: // n1, 11, 11, 11
                return new zmSZ(zmSz1, zmSz1);
        }
        return getZmN1(zmSz0, zmSz1);
    }

    zmSZ getZmN1(zmSz zmSz0, zmSz zmSz1)
    {
        return RandEx.TruePerCt(50) ? new zmSZ(zmSz0, zmSz1) : new zmSZ(zmSz1, zmSz0);
    }
    #endregion

    #region znRt
    const int _cb = 60;
    const int _cc = 130;
    const int _ce = 200;
    znRt[] _znRt0s = new znRt[] {
        new znRt(_cc,_cb, 12), new znRt(_cc,_ce, 5), new znRt(_ce,_cc, 10),
        new znRt(_cb,_cc, 3),  new znRt(_ce,_cc, 8), new znRt(_cc,_cb, 1),
        new znRt(_cc,_ce, 6), new znRt(_cc,_cb, 11), new znRt(_cb,_cc, 4),
        new znRt(_ce,_cc, 9), new znRt(_cb,_cc, 2), new znRt(_cc,_ce, 7)};
    struct znRt {
        public int x0, z0, edx0, edx1;
        public znRt(int x0_, int z0_, int edx0_, int edx1_ = 0) {
            x0 = x0_; z0 = z0_; edx0 = edx0_; edx1 = edx1_; }
        public znRt ArrangeForStart()
        {
            int r012 = RandEx.Get012();
            switch (edx0) {
                case 11: case 12: case 1:
                    switch (r012) {
                        case 0: x0 = _ce; edx1 = 10; break;
                        case 1: x0 = _cc; edx1 = RandEx.Get(11, 12, 1); break;
                        case 2: x0 = _cb; edx1 = 2; break; }
                    break;
                case 2: case 3: case 4:
                    switch (r012) {
                        case 0: z0 = _cb; edx1 = 1; break;
                        case 1: z0 = _cc; edx1 = RandEx.Get(2, 3, 4); break;
                        case 2: z0 = _ce; edx1 = 5; break; }
                    break;
                case 5: case 6: case 7:
                    switch (r012) {
                        case 0: x0 = _cb; edx1 = 4; break;
                        case 1: x0 = _cc; edx1 = RandEx.Get(5, 6, 7); break;
                        case 2: x0 = _ce; edx1 = 8; break; }
                    break;
                case 8: case 9: case 10:
                    switch (r012) {
                        case 0: z0 = _ce; edx1 = 7; break;
                        case 1: z0 = _cc; edx1 = RandEx.Get(8, 9, 10); break;
                        case 2: z0 = _cb; edx1 = 11; break; }
                    break;
            }
            return this;
        }
    }

    znRt getZrt(i4 t, zmSZ sz, znRt pre, bool nextEdx)
    {
        int rnr = UnityEngine.Random.Range(1, (sz.numGapR - 1));
        int rnc = UnityEngine.Random.Range(1, (sz.numGapC - 1));
        switch (pre.edx0)
        {
            case 11: case 12: case 1:
                return new znRt(t.X0 - sz.szGapR * rnr, t.Z1 + 1, pre.edx1, nextEdx ? getEdxOfZn2_11121(pre.edx1) : pre.edx1);
            case 2: case 3: case 4:
                return new znRt(t.X1 + 1, t.Z0 - sz.szGapC * rnc, pre.edx1, nextEdx ? getEdxOfZn2_234(pre.edx1) : pre.edx1);

            case 5: case 6: case 7:
                return new znRt(t.X0 - sz.szGapR * rnr, (t.Z0 - 1) - sz.totalGapC, pre.edx1, nextEdx ? getEdxOfZn2_567(pre.edx1) : pre.edx1);
            case 8: case 9: case 10:
                return new znRt((t.X0 - 1) - sz.totalGapR, t.Z0 - sz.szGapC * rnc, pre.edx1, nextEdx ? getEdxOfZn2_8910(pre.edx1) : pre.edx1);
        }
        Debug.LogWarning("e:" + pre.edx0);
        return new znRt();
    }

    int getEdxOfZn2_234(int preEdx)
    {
        switch (preEdx) {
            case 1: return RandEx.TruePerCt(50) ? 2 : RandEx.Get(11, 12, 1);
            case 5: return RandEx.TruePerCt(50) ? 4 : RandEx.Get(5, 6, 7);
            default:
                switch (RandEx.Get012()) {
                    case 0: return 1;
                    case 1: return 5; }
                return RandEx.Get(2, 3, 4);
        }
    }

    int getEdxOfZn2_567(int preEdx)
    {
        switch (preEdx)
        {
            case 4: return RandEx.TruePerCt(50) ? 5 : RandEx.Get(2, 3, 4);
            case 8: return RandEx.TruePerCt(50) ? 7 : RandEx.Get(8, 9, 10);
            default:
                switch (RandEx.Get012()) {
                    case 0: return 4;
                    case 1: return 8; }
                return RandEx.Get(5, 6, 7);
        }
    }

    int getEdxOfZn2_8910(int preEdx)
    {
        switch (preEdx) {
            case 7: return RandEx.TruePerCt(50) ? 8 : RandEx.Get(5, 6, 7);
            case 11: return RandEx.TruePerCt(50) ? 10 : RandEx.Get(11, 12, 1);
            default:
                switch (RandEx.Get012()) {
                    case 0: return 7;
                    case 1: return 11; }
                return RandEx.Get(8, 9, 10);
        }
    }

    int getEdxOfZn2_11121(int preEdx)
    {
        switch (preEdx) {
            case 2: return RandEx.TruePerCt(50) ? 1 : RandEx.Get(2, 3, 4);
            case 10: return RandEx.TruePerCt(50) ? 11 : RandEx.Get(8, 9, 10);
            default:
                switch (RandEx.Get012()) {
                    case 0: return 2;
                    case 1: return 10; }
                return RandEx.Get(1, 12, 11);
        }
    }
    #endregion

    #region znm
    struct znm
    {
        public i4 bd, ll; public i2 szGap, numGap;
        public znm(znRt rt, zmSZ sz)
        {
            bd = new i4(rt.x0, rt.z0, rt.x0 + sz.totalGapR, rt.z0 + sz.totalGapC);
            ll = new i4(bd.X0 - 1, bd.Z0 - 1, bd.X1 + 1, bd.Z1 + 1);
            szGap = new i2(sz.szGapR, sz.szGapC);
            numGap = new i2(sz.numGapR, sz.numGapC);
        }
        public znm(i4 bd_, zmSZ sz)
        {
            bd = bd_;
            ll = new i4(bd.X0 - 1, bd.Z0 - 1, bd.X1 + 1, bd.Z1 + 1);
            szGap = new i2(sz.szGapR, sz.szGapC);
            numGap = new i2(sz.numGapR, sz.numGapC);
        }
    }
    #endregion

    List<znRt> _znRts = new List<znRt>();
    List<znm> _znms = new List<znm>();
    void Reset_ZnRt_Znm(int stageIdx, zmSZ zms)
    {
        _znRts.Clear(); _znms.Clear();
        _znRts.Add(_znRt0s[stageIdx % 12].ArrangeForStart());
        _znms.Add(new znm(_znRts[0], zms));
    }

    #region addEntry
    void addEntryZone(zmSZ zms)
    {
        i5 bd = getEntry(_znms[0], _znRts[0].edx0);
        js.zone_ z0 = js.Inst.AddZone(bd.X0, bd.Z0, bd.X1, bd.Z1, 0, 1, js.SeriType.WithCell);
        js.Inst.AddTileT(z0, bd.X0, bd.Z0, bd.X1, bd.Z1, _mats.tile, js.SeriType.WithCell);
        js.Inst.AddWallsRect(z0, bd.i4, wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.WithCell);
        js.Inst.Add_Zbg_Zprp_ByEntryZone(z0);

        js.Inst.ResetSpawn(0.5f + bd.X0 + (bd.GapX>>1), bd.Z0 + (bd.GapZ >> 1) + 1);

        i4 bzd = _znms[0].bd;
        js.zone_ z1 = js.Inst.AddZone(bzd.X0, bzd.Z0, bzd.X1, bzd.Z1, 0, 1, js.SeriType.WithCell);
        js.Inst.AddTileT(z1, bzd.X0, bzd.Z0, bzd.X1, bzd.Z1, _mats.tile, js.SeriType.WithCell);
        
        switch (bd.Y)
        {
            case 0: case 1:
                js.Inst.AddWallsRect(z0, new i4(bd.X0 - 1, bd.Z0 - 1, bd.X1, bd.Z1 + 1), wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.WithCell);
                break;
            case 2: case 3:
                js.Inst.AddWallsRect(z0, new i4(bd.X0, bd.Z0 - 1, bd.X1 + 1, bd.Z1 + 1), wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.WithCell);
                break;
        }

        switch (bd.Y) {
            case 0:
                js.Inst.AddWallsDel(z0, bd.X1, bd.Z0 + 3, bd.X1, bd.Z1 - 1, js.SeriType.WithCell);
                js.Inst.AddWallsDel(z1, bd.X1+1, bd.Z0 + 3, bd.X1+1, bd.Z1 - 1, js.SeriType.WithCell);
                break;
            case 1:
                js.Inst.AddWallsDel(z0, bd.X1, bd.Z0 + 1, bd.X1, bd.Z1 - 3, js.SeriType.WithCell);
                js.Inst.AddWallsDel(z1, bd.X1+1, bd.Z0 + 1, bd.X1+1, bd.Z1 - 3, js.SeriType.WithCell);
                break;
            case 2:
                js.Inst.AddWallsDel(z0, bd.X0, bd.Z0 + 1, bd.X0, bd.Z1 - 3, js.SeriType.WithCell);
                js.Inst.AddWallsDel(z1, bd.X0-1, bd.Z0 + 1, bd.X0-1, bd.Z1 - 3, js.SeriType.WithCell);
                break;
            case 3:
                js.Inst.AddWallsDel(z0, bd.X0, bd.Z0 + 3, bd.X0, bd.Z1 - 1, js.SeriType.WithCell);
                js.Inst.AddWallsDel(z1, bd.X0-1, bd.Z0 + 3, bd.X0-1, bd.Z1 - 1, js.SeriType.WithCell);
                break;
        }

      //  addMazeAndNpc(z1, bzd, zms.szGapR, zms.szGapC, zms.numGapR, zms.numGapC,
      //                           wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.WithCell);

    }
    const int etl = 10;
    i5 getEntry(znm zn, int edx)
    {
        i4 ll = zn.ll, bd = zn.bd;
        int sc = zn.szGap.z;

        switch (edx)
        {
            case 1: case 2: return i5.N(ll.X0 - etl, ll.Z0 - 1, ll.X0, bd.Z0 + sc, 0);
            case 4: case 5: return i5.N(ll.X0 - etl, bd.Z1 - sc, ll.X0, ll.Z1 + 1, 1);
            case 7: case 8: return i5.N(ll.X1, bd.Z1 - sc, ll.X1 + etl, ll.Z1 + 1, 2);
            case 10: case 11: return i5.N(ll.X1, ll.Z0 - 1, ll.X1 + etl, bd.Z0 + sc, 3);

            case 12: return getEntry(zn, RandEx.Get(1, 11));
            case 6: return getEntry(zn, RandEx.Get(5, 7));
            case 3: return getEntry(zn, RandEx.Get(2, 4));
            case 9: return getEntry(zn, RandEx.Get(8, 10));
        }
        Debug.LogError("e" + edx);
        return new i5();
    }

    #endregion

    #region addEndry
    void addEndry(znRt zrt_, znm zn, zmSZ zms)
    {
        js.zone_ zn_1 = js.Inst.zones[js.Inst.zones.Count - 1];

        i5 bd = getEndry(zn, zrt_.edx0);
        js.zone_ zn_e = js.Inst.AddZone(bd.X0, bd.Z0, bd.X1, bd.Z1, 0, 1, js.SeriType.WithCell);
        js.Inst.AddTileT(zn_e, bd.X0, bd.Z0, bd.X1, bd.Z1, _mats.tile, js.SeriType.WithCell);
        js.Inst.AddWallsRect(zn_e, bd.i4, wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.WithCell);
        
        switch (bd.Y)
        {
            case 22: case 44:
                js.Inst.AddWallsRect(zn_e, new i4(bd.X0, bd.Z0 - 1, bd.X1 + 1, bd.Z1 + 1), wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.WithCell);
                break;
            case 88: case 101:
                js.Inst.AddWallsRect(zn_e, new i4(bd.X0 - 1, bd.Z0 - 1, bd.X1, bd.Z1 + 1), wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.WithCell);
                break;
        }

        switch (bd.Y) {
            case 22:
                js.Inst.AddWallsDel(zn_e, bd.X0, bd.Z0 + 1, bd.X0, bd.Z1 - 3, js.SeriType.WithCell);
                js.Inst.AddWallsDel(zn_1, bd.X0 - 1, bd.Z0 + 1, bd.X0 - 1, bd.Z1 - 3, js.SeriType.WithCell);
                js.Inst.AddZprop(zn_e, bd.X1 - 3, bd.Z1 - 3, bd.X1 - 1, bd.Z1 - 1, zprps.eArea, cel1l.Type.AreaWin, f4.O, js.SeriType.Default);
                break;
            case 44:
                js.Inst.AddWallsDel(zn_e, bd.X0, bd.Z0 + 3, bd.X0, bd.Z1 - 1, js.SeriType.WithCell);
                js.Inst.AddWallsDel(zn_1, bd.X0 - 1, bd.Z0 + 3, bd.X0 - 1, bd.Z1 - 1, js.SeriType.WithCell);
                js.Inst.AddZprop(zn_e, bd.X1 - 3, bd.Z1 - 3, bd.X1 - 1, bd.Z1 - 1, zprps.eArea, cel1l.Type.AreaWin, f4.O, js.SeriType.Default);
                break;
            case 88:
                js.Inst.AddWallsDel(zn_e, bd.X1, bd.Z0 + 3, bd.X1, bd.Z1 - 1, js.SeriType.WithCell);
                js.Inst.AddWallsDel(zn_1, bd.X1 + 1, bd.Z0 + 3, bd.X1 + 1, bd.Z1 - 1, js.SeriType.WithCell);
                js.Inst.AddZprop(zn_e, bd.X0 + 1, bd.Z1 - 3, bd.X0 + 3, bd.Z1 - 1, zprps.eArea, cel1l.Type.AreaWin, f4.O, js.SeriType.Default);
                break;
            case 101:
                js.Inst.AddWallsDel(zn_e, bd.X1, bd.Z0 + 1, bd.X1, bd.Z1 - 3, js.SeriType.WithCell);
                js.Inst.AddWallsDel(zn_1, bd.X1 + 1, bd.Z0 + 1, bd.X1 + 1, bd.Z1 - 3, js.SeriType.WithCell);
                js.Inst.AddZprop(zn_e, bd.X0 + 1, bd.Z1 - 3, bd.X0 + 3, bd.Z1 - 1, zprps.eArea, cel1l.Type.AreaWin, f4.O, js.SeriType.Default);
                break;
        }

        js.Inst.AddRpr(zn_e, bd.X0 + 2, bd.Z0 + 2, rprs.eLamb, cel1l.Type.LambOff, f4.O, js.SeriType.WithCell);
        js.Inst.AddRpr(zn_e, bd.X1 - 2, bd.Z0 + 2, rprs.eLamb, cel1l.Type.LambOff, f4.O, js.SeriType.WithCell);

    }
    const int et2 = 10;
    i5 getEndry(znm zn, int edx)
    {
        i4 ll = zn.ll, bd = zn.bd;
        int sc = zn.szGap.z;

        switch (edx)
        {
            case 2: case 3: case 4: return getEndry(zn, RandEx.Get(22, 44)); 
            case 5: case 6: case 7: return getEndry(zn, RandEx.Get(88, 44)); 
            case 8: case 9: case 10: return getEndry(zn, RandEx.Get(88, 101)); 
            case 11: case 12: case 1: return getEndry(zn, RandEx.Get(101, 22));

            case 88: return i5.N(ll.X0 - et2, ll.Z0 - 1, ll.X0, bd.Z0 + sc, 88);
            case 101: return i5.N(ll.X0 - et2, bd.Z1 - sc, ll.X0, ll.Z1 + 1, 101);
            case 22: return i5.N(ll.X1, bd.Z1 - sc, ll.X1 + et2, ll.Z1 + 1, 22);
            case 44: return i5.N(ll.X1, ll.Z0 - 1, ll.X1 + et2, bd.Z0 + sc, 44);
        }
        Debug.LogError("e" + edx);
        return new i5();
    }
    #endregion

    #region addTunnel
    void addTunnelAndZone(znRt zrt_, znm znm_, zmSZ zms, bool nextEdx = false)
    {
        int edx = zrt_.edx0;

        i4 bd = getTunnel(znm_, edx, zms);
            js.zone_ tn = js.Inst.AddZone(bd.X0, bd.Z0, bd.X1, bd.Z1, 0, 1, js.SeriType.WithCell);
            js.Inst.AddTileT(tn, bd.X0, bd.Z0, bd.X1, bd.Z1, _mats.tile, js.SeriType.WithCell);
            js.Inst.AddWallsRect(tn, bd, wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.WithCell);
            js.Inst.AddZbg_CloudByTunnel(tn);

        znRt zrt = getZrt(bd, zms, zrt_, nextEdx);
        znm zm = new znm(zrt, zms);
        
            js.zone_ zn = js.Inst.AddZone(zm.bd.X0, zm.bd.Z0, zm.bd.X1, zm.bd.Z1, 0, 1, js.SeriType.WithCell);
            js.Inst.AddTileT(zn, zm.bd.X0, zm.bd.Z0, zm.bd.X1, zm.bd.Z1, _mats.tile, js.SeriType.WithCell);
           // addMazeAndNpc(zn, zm.bd, zms.szGapR, zms.szGapC, zms.numGapR, zms.numGapC,
          //                               wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.WithCell);

        _znRts.Add(zrt);
        _znms.Add(zm);

        // 
        setExits(edx, bd, js.Inst.zones[js.Inst.zones.Count - 3], tn, zn);
    }
    
    //tn
    const int tl = 13;
    i4 getTunnel(znm zn, int edx, zmSZ nextZmsz )
    {
        i4 ll = zn.ll, bd = zn.bd;
        int sr = Mathf.Min(zn.szGap.x, nextZmsz.szGapR);
        int sc = Mathf.Min(zn.szGap.z, nextZmsz.szGapC);
        int cr = RandEx.GetHalfIdx(zn.numGap.x);
        int cc = RandEx.GetHalfIdx(zn.numGap.z);

        switch (edx)
        {
            case 11: return i4.N(bd.X0 + sr, ll.Z1, ll.X0 + sr * 2, ll.Z1 + tl);
            case 1: return i4.N(ll.X1 - sr * 2, ll.Z1, bd.X1 - sr, ll.Z1 + tl);

            case 2: return i4.N(ll.X1, ll.Z1 - sc * 2, ll.X1 + tl, bd.Z1 - sc);
            case 4: return i4.N(ll.X1, bd.Z0 + sc, ll.X1 + tl, ll.Z0 + sc * 2);

            case 5: return i4.N(ll.X1 - sr * 2, ll.Z0 - tl, bd.X1 - sr, ll.Z0);
            case 7: return i4.N(bd.X0 + sr, ll.Z0 - tl, ll.X0 + sr * 2, ll.Z0);

            case 8: return i4.N(ll.X0 - tl, bd.Z0 + sc, ll.X0, ll.Z0 + sc * 2);
            case 10: return i4.N(ll.X0 - tl, ll.Z1 - sc * 2, ll.X0, bd.Z1 - sc);


            case 12: return i4.N(bd.X0 + sr * cr, ll.Z1, ll.X0 + sr * cr + sr, ll.Z1 + tl);
            case 6: return i4.N(bd.X0 + sr * cr, ll.Z0 - tl, ll.X0 + sr * cr + sr, ll.Z0);

            case 3: return i4.N(ll.X1, bd.Z0 + sc * cc, ll.X1 + tl, ll.Z0 + sc * cc + sc);
            case 9: return i4.N(ll.X0 - tl, bd.Z0 + sc * cc, ll.X0, ll.Z0 + sc * cc + sc);
        }
        Debug.LogError("e" + edx);
        return new i4();
    }
    #endregion

    void setExits(int edx, i4 bd, js.zone_ zn0, js.zone_ t, js.zone_ zn1)
    {
        switch (edx) {
            case 11: case 12: case 1: case 5: case 6: case 7:
                js.Inst.AddWallsDel(t, bd.X0+1, bd.Z0, bd.X1-1, bd.Z0, js.SeriType.WithCell);
                js.Inst.AddWallsDel(t, bd.X0+1, bd.Z1, bd.X1-1, bd.Z1, js.SeriType.WithCell);
                break;
            case 2: case 3: case 4: case 8: case 9: case 10:
                js.Inst.AddWallsDel(t, bd.X0, bd.Z0+1, bd.X0, bd.Z1-1, js.SeriType.WithCell);
                js.Inst.AddWallsDel(t, bd.X1, bd.Z0+1, bd.X1, bd.Z1-1, js.SeriType.WithCell);
                break;
        }
        
        switch (edx)  {
            case 11: case 12: case 1:
                js.Inst.AddWallsDel(zn0, bd.X0+1, bd.Z0-1, bd.X1-1, bd.Z0-1, js.SeriType.WithCell);
                js.Inst.AddWallsDel(zn1, bd.X0+1, bd.Z1+1, bd.X1-1, bd.Z1+1, js.SeriType.WithCell);
                break;

            case 2: case 3: case 4:
                js.Inst.AddWallsDel(zn0, bd.X0-1, bd.Z0+1, bd.X0-1, bd.Z1-1, js.SeriType.WithCell);
                js.Inst.AddWallsDel(zn1, bd.X1+1, bd.Z0+1, bd.X1+1, bd.Z1-1, js.SeriType.WithCell);
                break;

            case 5: case 6: case 7:
                js.Inst.AddWallsDel(zn0, bd.X0+1, bd.Z1+1, bd.X1-1, bd.Z1+1, js.SeriType.WithCell);
                js.Inst.AddWallsDel(zn1, bd.X0+1, bd.Z0-1, bd.X1-1, bd.Z0-1, js.SeriType.WithCell);
                break;

            case 8: case 9: case 10:
                js.Inst.AddWallsDel(zn0, bd.X1+1, bd.Z0+1, bd.X1+1, bd.Z1-1, js.SeriType.WithCell);
                js.Inst.AddWallsDel(zn1, bd.X0-1, bd.Z0+1, bd.X0-1, bd.Z1-1, js.SeriType.WithCell);
                break;
        }
    }
    
}
