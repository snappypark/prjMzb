using System.Collections;
using UnityEngine;

public partial class zone
{
    public void AddWall(int x, int z, wal4Mat mats, cel1l.Type type)
    {
        cel1l cell = core.zells[x, z];
        if (cell.type == cel1l.Type.Wall || cell.type == cel1l.Type.WallBg)
            return;
        core.zells.Set(x, z, type, cel1l.ColliType.Cube);
        if(cell.zn.idx == -1)
            _wals.offs.Enqueue(new wall_(cell, cell.ct.x, 0, cell.ct.z, mats));
        else if (cel1ls.NearZoneType(cell) == cel1ls.NearZnType.None)
            _wals.offs.Enqueue(new wall_(cell, cell.ct.x, 0, cell.ct.z, mats));
        else
            _wals.offs.Enqueue(new wall_(cell, cell.ct.x, 0, cell.ct.z, wal4Mat.N(mats.Mini, matWalls.Metal)));
    }

    public void AddWallLines(i4 bd, wal4Mat mats, cel1l.Type type)
    {
        addWallLine(bd.X0, bd.Z0, bd.X1, bd.Z1, mats, type);
    }

    public void AddRectWalls(i4 bd, wal4Mat mats, cel1l.Type type)
    {
        addWallLine(bd.X0, bd.Z1, bd.X1, bd.Z1, mats, type);
        addWallLine(bd.X0, bd.Z0, bd.X1, bd.Z0, mats, type);
        addWallLine(bd.X0, bd.Z0, bd.X0, bd.Z1, mats, type);
        addWallLine(bd.X1, bd.Z0, bd.X1, bd.Z1, mats, type);
    }

    void addWallX(int x0, int z0, int gapR, wal4Mat mats, cel1l.Type type) {
        addWallLine(x0, z0, x0 + gapR, z0, mats, type);   }
    void addWallZ(int x0, int z0, int gapC, wal4Mat mats, cel1l.Type type) {
        addWallLine(x0, z0, x0, z0 + gapC, mats, type);   }
    void addWallLine(int ux0, int uz0, int ux1, int uz1, wal4Mat mats, cel1l.Type type) {
        for (int x = ux0; x <= ux1; ++x)
            for (int z = uz0; z <= uz1; ++z)
                AddWall(x, z, mats, type);
    }

    public void SetNoneWalls(int x0_, int z0_, int x1_, int z1_)
    {
        for (int x = x0_; x <= x1_; ++x)
            for (int z = z0_; z <= z1_; ++z)
                if (core.zells[x, z].type == cel1l.Type.Wall)
                    core.zells.Set(x, z, cel1l.Type.Tile, cel1l.ColliType.None);
    }

    public class wall_
    {
        public wal4Mat mats;
        public short cdx;
        public wall_(cel1l cell_, float x, float y, float z, wal4Mat mats_, float gapRan = 0.617f)
        {
            cell = cell_;
            Ps = new Vector3(x, y, z);
            mats = mats_;
            switch (mats.Top)
            {
                case matWalls.Metal:
                    szUp = new Vector3(metalSize, metalSize, metalSize);
                    szDown = new Vector3(metalSize, metalSize, metalSize);
                    break;
                default:
                    szUp = Vector3.one;
                    szDown = Vector3.one;
                    break;
            }
            cdx = -1;
        }

        public cel1l cell;
        public Vector3 Ps;
        public Vector3 szUp, szDown;

        const float metalSize = 1.121f;
    }

    [HideInInspector] qq<wall_> _wals = new qq<wall_>();

    bool hasTask_OnOn_Walls()
    {
        byte count = 0;
        while (_wals.offs.Count > 0 && ++count < CntTask)
            if (zjs.walls.PutOn(_wals.DeqFromOff()))
                _wals.EnqToOn();
        return count != 0;
    }

    bool hasTask_OnOff_Walls()
    {
        byte count = 0;
        while (_wals.ons.Count > 0 && ++count < CntTask)
            zjs.walls.PutOff(_wals.On_To_Off().cell);
        return count != 0;
    }

    public void clear_Walls()
    {
        while (_wals.ons.Count > 0)
            zjs.walls.PutOff(_wals.ons.Dequeue().cell);
        _wals.offs.Clear();
    }

    void refreshWalls_ByJs()
    {
        while (_wals.ons.Count > 0)
            zjs.walls.PutOff(_wals.On_To_Off().cell);
        
        while (_wals.offs.Count > 0)
            zjs.walls.PutOn(_wals.Off_To_On());
    }

    const float gap0 = 0.87f;
    const float gap1 = 1.13f;
    public IEnumerator SetWallsRandomGap_()
    {
        int num = _wals.offs.Count;
        for (int i = 0; i < num; ++i)
        {
            if ((i % 128) == 127)
                yield return null;
            wall_ w = _wals.offs.Dequeue();
            if (w.cell.East().IsNotWall && w.cell.West().IsNotWall)
            {
                float r1 = UnityEngine.Random.Range(gap0, gap1);
                float r2 = UnityEngine.Random.Range(gap0, gap1);

                w.szUp = new Vector3(r1, 1, 1);
                w.szDown = new Vector3(r2, 1, 1);
            }
            else if (w.cell.North().IsNotWall && w.cell.South().IsNotWall)
            {
                float r1 = UnityEngine.Random.Range(gap0, gap1);
                float r2 = UnityEngine.Random.Range(gap0, gap1);
                w.szUp = new Vector3(1, 1, r1);
                w.szDown = new Vector3(1, 1, r2);
            }

            _wals.offs.Enqueue(w);
        }
        yield return null;
    }
}

public struct wal4Mat
{
    public byte Mini, Top, SideUp, SideDown;

    public wal4Mat(byte Mini_, byte Top_, byte SideUp_, byte SideDown_)
    {
        Mini = Mini_;
        Top = Top_;
        SideUp = SideUp_;
        SideDown = SideDown_;
    }

    public static wal4Mat N(byte Mini_, byte Top_, byte SideUp_, byte SideDown_) {
                                                    return new wal4Mat(Mini_, Top_, SideUp_, SideDown_);  }
    public static wal4Mat N(byte mat_) {            return new wal4Mat(mat_, mat_, mat_, mat_); }
    public static wal4Mat N(matWalls.Type mat_) {   return N((byte)mat_);   }
    public static wal4Mat N(byte mat_, byte mat2_){ return new wal4Mat(mat_, mat2_, mat2_, mat2_);}
    public static wal4Mat N(matWalls.Type mat_, byte mat2_)
    {
        return new wal4Mat((byte)mat_, mat2_, mat2_, mat2_);
    }
}