using UnityEngine;

public class tiles : nj.ObjsQuePool<tiles, tile>
{
    [SerializeField] public matTiles mats;

    short[] _numClones = new short[] { 3 };
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }
    protected override bool getInitActiveOfType(byte type) { return true; }
    
    public void PutOn(zone.tile_ info)
    {
        tile obj = Reuse(0, info.Ps);
        obj.Assign(ref info);
    }

    public void PutOff(zone.tile_ info)
    {
        if (info.cdx != -1)
        {
            Unuse(0, info.cdx);
            info.cdx = -1;
        }
    }
}
