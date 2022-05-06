using UnityEngine;

public class smudges : nj.ObjsQuePool<smudges, nj.qObj>
{
    public const byte eExplosionBomb = 0, eFootPrint = 1;
    short[] _numClones = new short[] { 5,  22};
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }
    protected override bool getInitActiveOfType(byte type) { return true;  }

    public void Replace(byte type, Vector3 pos)
    {
        if (_pool[type].IsFull)
            return;
        Recycle(type, pos);
    }


    static Vector3 _prePos = Vector3.zero;
    public static void SetFootPrint()
    {
        Vector3 v = ctrls.Unit.tran.localPosition - _prePos;
        if (Vector3.SqrMagnitude(v) > 9)
        {
            _prePos = ctrls.Unit.tran.localPosition;
            gjs.smudges.Replace(eFootPrint,
                _prePos + new Vector3(0, 0.1f, 0));
        }
    }
}
