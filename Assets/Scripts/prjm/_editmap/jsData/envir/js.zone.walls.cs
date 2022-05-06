using UnityEngine;
using System.Collections.Generic;

public partial class js
{
    public void AddWallsMaze(
        zone_ zn, i4 bd_, i2 szRC, i2 numRC, wal4Mat mat_,
        cel1l.Type type, int rand0_, int option0_, SeriType seriType)
    {
        zn.wallsMazes.Add(new wallsMaze_(bd_, szRC, numRC, mat_, type, rand0_, option0_));

        switch (seriType)
        {
            case SeriType.WithLoad:
                zn.zn.AddMazeWalls(bd_, szRC.x, szRC.z, numRC.x, numRC.z, mat_, type, rand0_, option0_);
                break;
            case SeriType.WithCell:
                //
                break;
        }
    }

    public void AddWallsRect(zone_ zn, i4 bd, wal4Mat mats, cel1l.Type type, SeriType seriType)
    {
        zn.wallsRects.Add(new wallsRect_(bd, mats, type));
        switch (seriType)
        {
            case SeriType.WithLoad:
                zn.zn.AddRectWalls(bd, mats, type);
                break;
            case SeriType.WithCell:
                core.zells.Set(bd.X0, bd.Z1, bd.X1, bd.Z1, 0, cel1l.Type.Wall, cel1l.ColliType.Cube);
                core.zells.Set(bd.X0, bd.Z0, bd.X1, bd.Z0, 0, cel1l.Type.Wall, cel1l.ColliType.Cube);
                core.zells.Set(bd.X0, bd.Z0, bd.X0, bd.Z1, 0, cel1l.Type.Wall, cel1l.ColliType.Cube);
                core.zells.Set(bd.X1, bd.Z0, bd.X1, bd.Z1, 0, cel1l.Type.Wall, cel1l.ColliType.Cube);
                break;
        }
    }

    public void AddWallsLine(zone_ zn, i4 bd, wal4Mat mats, cel1l.Type type, SeriType seriType)
    {
        zn.wallsLines.Add(new wallsRect_(bd, mats, type));
        switch (seriType)
        {
            case SeriType.WithLoad:
                zn.zn.AddWallLines(bd, mats, type);
                break;
            case SeriType.WithCell:
                core.zells.Set(bd.X0, bd.Z0, bd.X1, bd.Z1, 0, cel1l.Type.Wall, cel1l.ColliType.Cube);
                break;
        }
    }

    public void AddWallsSingle(
        zone_ zn, int x, int z, wal4Mat mats, cel1l.Type type, SeriType seriType)
    {
        zn.walls.Add(new wallSingle_(x, z, mats, type));
        switch (seriType)
        {
            case SeriType.WithLoad:
                zn.zn.AddWall(x, z, mats, type);
                break;
            case SeriType.WithCell:
                core.zells.Set(x, z, x, z, 0, cel1l.Type.Wall, cel1l.ColliType.Cube);
                break;
        }
    }

    [System.Serializable]
    public class wallsRect_
    {
        [HideInInspector] public i4 bd;
        [HideInInspector] public cel1l.Type type;
        public matWalls.Type matMini, matTop, matSideUp, matSideDown;
        public wallsRect_() { }
        public wallsRect_(i4 bd_, wal4Mat mat_, cel1l.Type type_)
        {
            bd = bd_;
            matMini = (matWalls.Type)mat_.Mini;
            matTop = (matWalls.Type)mat_.Top;
            matSideUp = (matWalls.Type)mat_.SideUp;
            matSideDown = (matWalls.Type)mat_.SideDown;
            type = type_;
        }

        public void SetMat(matWalls.Type mini, matWalls.Type top, matWalls.Type sideUp, matWalls.Type sideDown)
        {
            matMini = mini; matTop = top; matSideUp = sideUp; matSideDown = sideDown;
        }
    }

    [System.Serializable]
    public class wallsMaze_ : wallsRect_
    {
        public i2 szRC, numRC;
        public int rand0, option0;
        public wallsMaze_() { }
        public wallsMaze_(i4 bd_, i2 szRC_, i2 numRC_, wal4Mat mats_, 
            cel1l.Type type_, int rand0_, int option0_)
        {
            bd = bd_;
            szRC = szRC_;
            numRC = numRC_;
            matMini = (matWalls.Type)mats_.Mini;
            matTop = (matWalls.Type)mats_.Top;
            matSideUp = (matWalls.Type)mats_.SideUp;
            matSideDown = (matWalls.Type)mats_.SideDown;
            type = type_;
            rand0 = rand0_;
            option0 = option0_;
        }
    }

    [System.Serializable]
    public class wallSingle_
    {
        [HideInInspector] public int x, z; // 1
        [HideInInspector] public cel1l.Type cType;
        public matWalls.Type matMini, matTop, matSideUp, matSideDown;
        public wallSingle_() { }
        public wallSingle_(int x_, int z_, wal4Mat mats_, cel1l.Type type_)
        {
            x = x_;
            z = z_;
            matMini = (matWalls.Type)mats_.Mini;
            matTop = (matWalls.Type)mats_.Top;
            matSideUp = (matWalls.Type)mats_.SideUp;
            matSideDown = (matWalls.Type)mats_.SideDown;
            cType = type_;
        }

        public void SetMat(matWalls.Type mini, matWalls.Type top, matWalls.Type sideUp, matWalls.Type sideDown)
        {
            matMini = mini; matTop = top; matSideUp = sideUp; matSideDown = sideDown;
        }
    }


    public void SetCellOfWallDel(zone_ zn)
    {
        for (int i = 0; i < zn.wallsDel.Count; ++i)
        {
            wall_Del_ dwall = zn.wallsDel[i];
            core.zells.Set(dwall.bd.X0, dwall.bd.Z0, dwall.bd.X1, dwall.bd.Z1, 0, cel1l.Type.Tile, cel1l.ColliType.None);
        }
    }

    public void AddWallsDel(zone_ zn, i4 bd, SeriType seriType)
    {
        AddWallsDel(zn, bd.X0, bd.Z0, bd.X1, bd.Z1, seriType);
    }

    public void AddWallsDel(zone_ zn, int x0_, int z0_, int x1_, int z1_, SeriType seriType)
    {
        zn.wallsDel.Add(new js.wall_Del_(x0_, z0_, x1_, z1_));
        
        switch (seriType)
        {
            case SeriType.WithLoad:
                zn.zn.SetNoneWalls(x0_, z0_, x1_, z1_);
                break;
            case SeriType.WithCell:
                core.zells.Set(x0_, z0_, x1_, z1_, 0, cel1l.Type.Tile, cel1l.ColliType.None);
                break;
        }
    }

    [System.Serializable]
    public class wall_Del_
    {
        [HideInInspector] public i4 bd;
        public wall_Del_() { }
        public wall_Del_(int x0_, int z0_, int x1_, int z1_)
        {
            bd = new i4(x0_, z0_, x1_, z1_);
        }

        public cel1l getCell00() { return core.zells[bd.X0, bd.Z0]; }
    }


    #region Wbg

    [System.Serializable]
    public class wallBg
    {
        [HideInInspector] public Vector3 ps;
        [HideInInspector] public Vector3 sz;
        [HideInInspector] public matWalls.Type mat;
        public wallBg() { }
        public wallBg(Vector3 ps_, Vector3 sz_, matWalls.Type mat_)
        {
            ps = ps_; sz = sz_; mat = mat_;
        }
    }

    public void AddWallBg_Randomly(zone_ zn, matWalls.Type mat_)
    {
        Vector3 ps = new Vector3();
        Vector3 sz = new Vector3(8 + Random.Range(0.0f, 14.0f),
                                3, 6 + Random.Range(0.0f, 14.0f));
        zn.wallsBg.Add(new wallBg(ps, sz, mat_));
    }
    #endregion
}
