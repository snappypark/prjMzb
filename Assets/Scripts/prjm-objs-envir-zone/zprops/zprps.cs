using System.Runtime.CompilerServices;
using UnityEngine;

public class zprps : nj.ObjsQuePool<zprps, zprp>
{
    public const byte 
        eArea=0, eDoor=1, eKey=2, eTrigger = 3, eBomb = 4, eAmmo=5, 
        ePotion=6, 
        eSpin = 7, 
        eArrow = 8, eWall = 9,
        eNumType = 10, eOutOf = 222;
    public enum Type : byte {
        Area, Door, Key, Trigger, Bomb, Ammo,
        Potion, Spin, Arrow, Wall, OutOf = 222 }

    short[] _numClones = new short[] { 2, 3, 3, 4, 8, 8,
                                        2, 2, 2, 10 };
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PutOn(zone.prp_ info)
    {
        zprp zprp = Reactive(info.type, info.Ps);
        zprp.Assign(info);

        info.cdx = zprp.cdx;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PutOff(zone.prp_ info)
    {
        if (info.cdx != -1)
        {
            for (int x = info.bd.X0; x <= info.bd.X1; ++x)
                for (int z = info.bd.Z0; z <= info.bd.Z1; ++z)
                    core.zells[x, z].zprp = null;

            Unactive(info.type, info.cdx);

            info.cdx = -1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(zone.prp_ info)
    {
        if (info.cdx != -1)
        {
            for (int x = info.bd.xMin; x <= info.bd.xMax; ++x)
                for (int z = info.bd.zMin; z <= info.bd.zMax; ++z)
                {
                    cel1l c = core.zells[x, z];
                    c.zprp = null;
                    core.collis.cubeRoller.SetUnuseObj(c.pt);
                    c.SetTile();
                }

            Unactive(info.type, info.cdx);

            info.cdx = -1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Effect(zone.prp_ info)
    {
        for (int x = info.bd.xMin; x <= info.bd.xMax; ++x)
            for (int z = info.bd.zMin; z <= info.bd.zMax; ++z)
            {
                cel1l c = core.zells[x, z];
                gjs.effs.Play(effs.getKey, c.ct + Vector3.up);
            }
    }

}
