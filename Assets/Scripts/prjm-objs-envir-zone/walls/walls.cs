using System.Runtime.CompilerServices;
using UnityEngine;

public class walls : nj.ObjsQuePool<walls, wall>
{
    [SerializeField] matWalls _mats;
    public Material Mats(byte idx) { return _mats[idx]; }

    short[] _numClones = new short[] { 2048 }; // 1256
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }
    protected override bool getInitActiveOfType(byte type) { return true; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool PutOn(zone.wall_ info)
    {
        if (_pool[0].IsFull)
        {
            Debug.LogWarning("full");
            return false;
        }

        switch (info.cell.type)
        {
            case cel1l.Type.Wall:
            case cel1l.Type.WallBg:
                info.cell.obj = Reuse(0, info.Ps);
                info.cell.obj.AssignWall(info, _mats[info.mats.Mini], _mats[info.mats.Top], _mats[info.mats.SideUp], _mats[info.mats.SideDown]);
                info.cell.IsMetal = info.mats.Top == matWalls.Metal;
                return true;
            default:
                return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PutOff(cel1l cell)
    {
        if (cell.obj != null)
        {
            Unuse(0, cell.obj.cdx);
            cell.obj = null;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(cel1l cell)
    {
        PutOff(cell);
        core.collis.cubeRoller.SetUnuseObj(cell.pt);
        cell.Set(cel1l.Type.Tile, cel1l.ColliType.None);
        cell.zn.changed = true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove_SkipMetal(cel1l cell)
    {
        if (cell.IsMetal)
            return;

        PutOff(cell);
        core.collis.cubeRoller.SetUnuseObj(cell.pt);
        cell.Set(cel1l.Type.Tile, cel1l.ColliType.None);
        cell.zn.changed = true;
    }
}
