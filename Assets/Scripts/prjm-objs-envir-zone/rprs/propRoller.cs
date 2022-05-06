using UnityEngine;

public class propRoller : nj.ObjsRoller
{
    public propRoller(int bdX, int bdZ,
        int cellSzX, int cellSzY, int cellSzZ)
        : base(bdX, bdZ, cellSzX, cellSzY, cellSzZ) { }

    public void Clear()
    {
        _curCt = new Pt(-1000, 0, -1000);
    }
    
    protected override void SetUnuseObj(int x, int y, int z)
    {
        if (cel1ls.IsOutIdx(x, z))
            return;

        cel1l c = core.zells[x, z];
        
        switch (c.objType) { case rprs.eOutOf: return;
            default:
                zjs.rprs.PutOff(c);
                return; }
    }
    
    protected override void SetUseObj(int x, int y, int z)
    {
        if (cel1ls.IsOutIdx(x, z))
            return;

        cel1l c = core.zells[x, z];
        if (c.obj != null)
            return;
        switch (c.objType) { case rprs.eOutOf: return;
            default:
                zjs.rprs.PutOn(c);
                return; }
    }
}
