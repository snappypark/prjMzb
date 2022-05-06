using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.GenericEx;
using UnityEngine;

public partial class cel1l
{
    public static cel1l Empty = new cel1l();

    public const int Size = 1;
    public const float Half = 0.5f;
    public const float OvSzY = 0.3333333333333f; // 1/3
    
    public Pt pt;
    public Vector3 ct;
    public f2 ps00, ps11;
    
    #region Collision
    public nj.qObj collObj = null;
    public ColliType colliType = ColliType.Null;

    public enum ColliType : byte
    {
        Null,
        None,
        Cube,
        Capsule,
    }
    #endregion

    public zone zn = null;
    public Type type = Type.None;
    public nj.qObj obj = null;
    public nj.qObj zprp = null;
    public byte objType = rprs.eOutOf;
    public f4 opts;
    public byte state = 0;

    public bool IsNotWall = true;
    public bool IsPath = false;
    public bool IsMetal = false;
    public bool IsState0 {get{return state == 0;}}
    
    public enum Type : byte
    {
        None=0,

        Tile, // tile

        Wall, WallBg, WallZ1, WallZ2, WallZ3, WallZ4,

        Box,
        Tube,
        LambOn,
        LambOff,
        BotFire,
        BotGun, BotZ1, BotZ2, BotZ3, BotZ4, BotZ5, BotZ6,
        LadderUp,
        LadderDown,

        Plat_X, PlatX,
        Plat_Z, PlatZ,
        PlatSpd, PlatHeal, PlatOil, Plat2_,

        PushUp,
        PushSide_X,
        PushSideX,
        PushSide_Z,
        PushSideZ,
        PushZA1, PushZA2, PushZA3, PushZA4, PushZA5,
        PushZB1, PushZB2, PushZB3, PushZB4, PushZB5,
        PushZC1, PushZC2, PushZC3, PushZC4, PushZC5,
        Tree1A_Blue,
        Tree1B_Pink,
        Tree2A_Blue,
        Tree2B_Pink,
        Tree3A_Red,
        Tree3B_Green,
        Tree4A_Red,
        Tree4B_Green,

        AreaWin, // nxn
        AreaWayPoint, // nxn - 
        AreaUpLadder,
        AreaDownLadder,
        AreaZ1, AreaZ2, AreaZ3, AreaZ4,
        TrigCircleBomb,
        Trig0, Trig1, Trig2, Trig3, Trig4, Trig5,
        TrigOff,
        Door1, Door2, Door3, Door4, Door5, Door6,
        DoorOpen,
        DoorEnemyClear,
        DoorZ1, DoorZ2, DoorZ3, DoorZ4, DoorZ5,
        DoorZ6,
        DoorZ7,
        DoorZ8,
        DoorZ9,
        Key1, Key2, Key3, Key4, Key5, Key6,
        Bomb,
        Ammo,
        Potion,
        ///*
        Grass,
        Flower1,
        Flower2,
        Flower3,
        Flower4,
        Word_Stage,
        Word_ThisWayLeft,
        Word_ThisWayRight,
        Word_Welcome,
        Word_Z2,
        Word_Z3,
        Word_Z4,
        Word_Z5,
        Word_Z6,
        Word_Z7,
        Word_Z8,
        Word_Z9,
        //
        Spin,  Spin_Z1, Spin_Z2, Spin_Z3, Spin_Z4, Spin_Z5,
        //*/

        Bush,
        Bush1,
        Bush2,
        Bush3,

        Trap_Fire,
        Trap_Slow,
        Trap_FireWall,
        Trap_WaitForFire,
        Trap_J,

        Tnt,
    }

    public void Set(Type type_, ColliType colliType_)
    {
        type = type_;
        colliType = colliType_;
        IsNotWall = type_ == Type.None || type_ == Type.Tile;
        IsPath = colliType_ == ColliType.None;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetTile()
    {
        type = Type.Tile;
        colliType = ColliType.None;
        IsPath = true;
        objType = rprs.eOutOf;
        state = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetFire()
    {
        type = Type.Trap_FireWall;
        colliType = ColliType.None;
        IsPath = true;
        objType = rprs.eTrap;
    }

    public void SetPrePath()
    {
        IsPath = true;
        zn.changed = true;
    }

    public void Clear()
    {
        state = 0;
        zn = zone.Empty;
        objType = rprs.eOutOf;
        obj = null;
        zprp = null;
        collObj = null;
        type = Type.None;
        colliType = ColliType.Null;

        IsNotWall = true;
        IsPath = false;
        IsMetal = false;

        units.Clear();
    }

    public cel1l East() { return core.zells[pt.x+1, pt.z]; }
    public cel1l West() { return core.zells[pt.x-1, pt.z]; }
    public cel1l North() { return core.zells[pt.x, pt.z+1]; }
    public cel1l South() { return core.zells[pt.x, pt.z-1]; }
    public cel1l NorthEast() { return core.zells[pt.x + 1, pt.z + 1]; }
    public cel1l NorthWest() { return core.zells[pt.x - 1, pt.z + 1]; }
    public cel1l SouthEast() { return core.zells[pt.x + 1, pt.z - 1]; }
    public cel1l SouthWest() { return core.zells[pt.x - 1, pt.z - 1]; }

    public cel1l C2X2(int idx) {
        return core.zells[pt.x + nj.idx.INxN(idx), pt.z + nj.idx.JNxN(idx)]; }
    public cel1l C3X3(int idx) {
        return core.zells[pt.x + nj.idx.INxN(idx), pt.z + nj.idx.JNxN(idx)]; }

    public static Pt Pt(Vector3 pos)
    {
        return new Pt((int)(pos.x), (int)(pos.y * OvSzY), (int)(pos.z));
    }
    public static Pt Pt11(Vector3 pos)
    {
        return new Pt((int)(pos.x+0.5f), (int)(pos.y * OvSzY), (int)(pos.z + 0.5f));
    }
    public static Vector3 Center(Pt pt)
    {
        return new Vector3((int)(pt.x) + Half, pt.y*3 + 0.5f, (int)(pt.z) + Half);
    }
    public static Vector3 Center(Vector3 pos)
    {
        return new Vector3((int)(pos.x) + Half, pos.y, (int)(pos.z) + Half);
    }
    public static Vector3 Pos00(Vector3 pos)
    {
        return new Vector3((int)(pos.x) * Size, pos.y, (int)(pos.z) * Size);
    }
    public static Vector3 Pos11(Vector3 pos)
    {
        return new Vector3((int)(pos.x) * Size + Size, pos.y, (int)(pos.z) * Size + Size);
    }

    public Queue<short> units = new Queue<short>();
    public unit RollUnit
    {
        get
        {
            unit unit = core.unitClones[units.Dequeue()];
            units.Enqueue(unit.cdx);
            return unit;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Clamp(Vector3 pos, float sqrDist, float dist)
    {
        f2 p = new f2(pos.x, pos.z);
        cel1l c11 = core.zells[cel1l.Pt11(pos)];
        for (int i = 0; i < nj.idx.MaxNum2x2; ++i)
        {
            cel1l c = c11.C2X2(i);
            if (c.IsPath)
                continue;
            f2 npos;
            if (c.GetNearestDectectedPos(p, sqrDist, out npos))
            {
                f2 v2 = new f2(c.ct.x - npos.x, c.ct.z - npos.z);
                f2 v21 = new f2(c.ct.x - p.x, c.ct.z - p.z);
                if (v2.Dot(v21) > 0)
                {
                    Vector3 v = new Vector3(npos.x - c.ct.x, 0, npos.z - c.ct.z).normalized * dist;
                    p = new f2(npos.x + v.x, npos.z + v.z);
                }
                else
                {
                    Vector3 v = new Vector3(p.x - npos.x, 0, p.z - npos.z).normalized * dist;
                    p = new f2(npos.x + v.x, npos.z + v.z);
                }
            }
        }
        return new Vector3(p.x, pos.y, p.z);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetNearestDectectedPos(f2 pos, float sqrDist, out f2 result)
    {
        f2 v = new f2(pos.x - ps00.x, pos.z - ps00.z);
        f2 v2 = new f2(pos.x - ps11.x, pos.z - ps11.z);

        if (pos.z < ct.z)
            result = new f2(ps00.x + Mathf.Clamp01(v.Dot(f2.x1)), ps00.z);
        else
            result = new f2(ps11.x - Mathf.Clamp01(v2.Dot(f2.x_1)), ps11.z);

        if (f2.SqrMagnitude(pos, result) < sqrDist)
            return true;

        if (pos.x < ct.x)
            result = new f2(ps00.x, ps00.z + Mathf.Clamp01(v.Dot(f2.z1)));
        else
            result = new f2(ps11.x, ps11.z - Mathf.Clamp01(v2.Dot(f2.z_1)));

        if (f2.SqrMagnitude(pos, result) < sqrDist)
            return true;
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 Clamp2(Vector3 ori, Vector3 pos, float sqrDist, float dist)
    {
        f2 p = new f2(pos.x, pos.z);
        cel1l c11 = core.zells[cel1l.Pt11(pos)];
        for (int i = 0; i < nj.idx.MaxNum2x2; ++i)
        {
            cel1l c = c11.C2X2(i);
            if (c.IsPath)
                continue;
            f2 npos;
            if (c.GetNearestDectectedPos(p, sqrDist, out npos))
            {
                f2 v2 = new f2(c.ct.x - npos.x, c.ct.z - npos.z);
                f2 v21 = new f2(c.ct.x - p.x, c.ct.z - p.z);
                if (v2.Dot(v21) > 0)
                {
                    Vector3 v = new Vector3(ori.x - npos.x, 0, ori.z - npos.z).normalized * dist;
                    p = new f2(npos.x + v.x, npos.z + v.z);
                }
                else
                {
                    Vector3 v = new Vector3(p.x - npos.x, 0, p.z - npos.z).normalized * dist;
                    p = new f2(npos.x + v.x, npos.z + v.z);
                }
            }
        }
        return new Vector3(p.x, pos.y, p.z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsInsideCubeColli(Vector3 p)
    {
        if (obj == null)
            return false;
        float x0 = obj.transform.localPosition.x - 0.5f;
        float x1 = obj.transform.localPosition.x + 0.5f;
        float z0 = obj.transform.localPosition.z - 0.5f;
        float z1 = obj.transform.localPosition.z + 0.5f;
        return p.x > x0 && p.x < x1 && p.z > z0 && p.z < z1;
    }
}
