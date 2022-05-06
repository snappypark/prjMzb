using UnityEngine;

public class zprp : nj.qObj
{
    public i4 bd;

    public virtual void Assign(zone.prp_ info) { }
    public virtual void UnAssign(zone.prp_ info) { }

    public virtual bool OnUsed() { return true; }

    public virtual bool OnUpdate(zone.prp_ info) { return true; }

    protected void AssignZprp(zone.prp_ info)
    {
        bd = info.bd.i4;
        for (int x = info.bd.X0; x <= info.bd.X1; ++x)
            for (int z = info.bd.Z0; z <= info.bd.Z1; ++z)
                core.zells[x, z].zprp = this;
    }
    
    protected void UnassignZprp(zone.prp_ info)
    {
        for (int x = info.bd.X0; x <= info.bd.X1; ++x)
            for (int z = info.bd.Z0; z <= info.bd.Z1; ++z)
                core.zells[x, z].zprp = null;

    }
}
