using UnityEngine;
using UnityEngineEx;

public class boms : nj.ObjsQuePool<boms, bom>
{
    short[] _numClones = new short[] { 32 };
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }

    public void Throw(Vector3 pos, float delay = 1.9f)
    {
        if (_pool[0].IsFull)
            return;
        Reactive(0, pos).Assign(19999, delay);
    }

}
