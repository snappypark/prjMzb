using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugs : nj.ObjsQuePool<debugs, nj.qObj>
{
    public const byte eNode = 0;
    //3111
    short[] _numClones = new short[] { 1, };
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }


    public nj.qObj PutOn(Vector3 pos)
    {
        nj.qObj obj = Reactive(0, pos);
        return obj;
    }

    public void PutOff(nj.qObj obj)
    {
        Unactive(obj.type, obj.cdx);
    }


    public void Test(zone zn)
    {
        for (int x = zn.bd.X0; x <= zn.bd.X1; ++x)
            for (int z = zn.bd.Z0; z <= zn.bd.Z1; ++z)
            {
                cel1l c = core.zells[x, z];
                debugNode d = PutOn(c.ct) as debugNode;
                d.Init(c);
            }
    }
}
