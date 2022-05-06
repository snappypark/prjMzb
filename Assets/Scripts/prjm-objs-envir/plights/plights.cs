using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plights : nj.ObjsQuePool<plights, plight>
{
    short[] _numClones = new short[] { 16 };
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }

    public plight PutOn(Vector3 pos)
    {
        return Reactive(0, pos);
    }

    public void PutOff(short cdx)
    {
        Unactive(0, cdx);
    }

}
