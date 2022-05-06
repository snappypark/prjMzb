using UnityEngine;

public class colliCubeRoller : nj.ObjsRoller
{
    public colliCubeRoller(int bdX, int bdZ,
        int cellSzX, int cellSzY, int cellSzZ)
        : base(bdX, bdZ, cellSzX, cellSzY, cellSzZ) { }

    public void SetUnuseObj(Pt pt)
    {
        if (cel1ls.IsOutIdx(pt.x, pt.z))
            return;

        cel1l c = core.zells[pt.x, pt.z];
        if (c.collObj == null)
            return;
        switch (c.colliType)
        {
            case cel1l.ColliType.Null:
            case cel1l.ColliType.Cube:
                core.collis.Unuse(0, c.collObj.cdx);
                c.collObj = null;
                break;
            case cel1l.ColliType.Capsule:
                core.collis.Unuse(1, c.collObj.cdx);
                c.collObj = null;
                break;
        }
    }

    protected override void SetUnuseObj(int x, int y, int z)
    {
        if (cel1ls.IsOutIdx(x, z))
            return;

        cel1l c = core.zells[x, z];
        if (c.collObj == null)
            return;
        switch (c.colliType)
        {
            case cel1l.ColliType.Null:
            case cel1l.ColliType.Cube:
                core.collis.Unuse(0, c.collObj.cdx);
                c.collObj = null;
                break;
            case cel1l.ColliType.Capsule:
                core.collis.Unuse(1, c.collObj.cdx);
                c.collObj = null;
                break;
        }
    }

    protected override void SetUseObj(int x, int y, int z)
    {
        if (cel1ls.IsOutIdx(x, z))
            return;

        cel1l c = core.zells[x, z];
        if (c.collObj != null)
            return;
        switch (c.colliType)
        {
            case cel1l.ColliType.Null:
            case cel1l.ColliType.Cube:
                c.collObj = core.collis.Reuse(0,
                    new Vector3( _cellHalfSz.x + x,
                                 0.5f,
                                 _cellHalfSz.z + z));
                break;
            case cel1l.ColliType.Capsule:
                c.collObj = core.collis.Reuse(1,
                    new Vector3(_cellHalfSz.x + x,
                                0,
                                 _cellHalfSz.z + z));
                break;
        }
    }
}
