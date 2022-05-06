using UnityEngine;

public class ctrlHuds : nj.ObjsQuePool<ctrlHuds, ctrlHud>
{
    public const byte eWord = 0;

    short[] _numClones = new short[] { 4, };
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }
    
    public void ShowWord(string str)
    {
        if (_pool[eWord].IsFull)
            return;
        ctrlHud zprp = Reactive(eWord, new Vector3(0, 0, 1.2f));
        zprp.textmesh.text = str;
        zprp.endTime = Time.time + 1.2f;
    }
}
