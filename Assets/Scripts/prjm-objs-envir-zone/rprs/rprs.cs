using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngineEx;

public class rprs : nj.ObjsQuePool<rprs, rpr>
{
    [SerializeField] public matProps mats;
    [SerializeField] public spriteProps sps;

    public const byte eBox = 0, eLamb=1, eBot=2, ePlat=3, 
                      ePush=4, eTree=5, eBush=6, eTrap=7, eTnt = 8, eOutOf = 222;
    public enum Type { Box = 0, Lamb, Bot, Plat, /**/Push, Tree, Bush, Trap, Tnt, OutOf = 222 }

    //48
    short[] _numClones = new short[] { 64, 8, 12, 12, /**/12, 64, 32, 128, 12 };
    bool[] _initActives = new bool[] { true, false, false, false, /**/false, true, true, false, false };
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }
    protected override bool getInitActiveOfType(byte type) { return _initActives[type]; }

    //public propRoller roller = new propRoller(31, 29, 1, 3, 1);
    //public propRoller roller = new propRoller(27, 25, 1, 3, 1);
    public propRoller roller = new propRoller(29, 21, 1, 3, 1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PutOn(cel1l c_)
    {
        rpr prp = Reuse(c_.objType, c_.ct);
        c_.obj = prp;
        prp.Assign(c_);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PutOff(cel1l c_)
    {
        if (c_.obj != null)
        {
            c_.obj.UnAssign();
            Unuse(c_.obj.type, c_.obj.cdx, VectorEx.Huge);
            c_.obj = null;
        }
    }

    public void Clear()
    {
        roller.Clear();
        UnAssignAndUnuseAllGamObj();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(cel1l c_)
    {
        PutOff(c_);
        c_.SetTile();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveWithCheck(cel1l c_, bool skipMetal = false /*for push*/)
    {
        if (skipMetal && c_.IsMetal)
            return;
        PutOff(c_);
        core.collis.cubeRoller.SetUnuseObj(c_.pt);
        c_.SetTile();
        c_.zn.changed = true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ReplaceWithFire(cel1l c_)
    {
        PutOff(c_);
        c_.SetFire();
        PutOn(c_);
    }

    public void SetWithFire(cel1l c_)
    {
        c_.SetFire();
        PutOn(c_);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(int x, int z, byte objType_, cel1l.Type cellType_, f4 options)
    {
        cel1l cb = core.zells[x, z];
        if (cb.type != cel1l.Type.Tile)
            return;
        cb.objType = objType_;
        cb.opts = options;
        switch (objType_)
        {
            case eBox:
            case eTnt:
                core.zells.Set(x, z, cellType_, cel1l.ColliType.Cube);
                break;
            case ePush:
                cb.IsMetal = matWalls.Metal == (byte)options.F4;
                core.zells.Set(x, z, cellType_, cel1l.ColliType.Cube);
                break;

            case eBot:
            case eLamb:
            case eTree:
                core.zells.Set(x, z, cellType_, cel1l.ColliType.Capsule);
                break;
            case ePlat:
            case eBush:
            case eTrap:
                core.zells.Set(x, z, cellType_, cel1l.ColliType.None);
                break;
        }
    }
}
