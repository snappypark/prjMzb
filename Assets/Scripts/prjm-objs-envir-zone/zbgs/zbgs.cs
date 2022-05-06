using UnityEngine;

public class zbgs : nj.ObjsQuePool<zbgs, zbg>
{
    public const byte eCloud=0, eGrass=1, eFlower=2, eWord=3, eNumType = 4, eOutOf = 222;

    public enum Type : byte { Cloud, Grass, Flower, Word, OutOf = 222 }
    short[] _numClones = new short[] { 2, 4, 1, 1 };
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }

    public void PutOn(zone.zbg_ info)
    {
        zbg zprp = Reactive(info.oType, info.ps);
        zprp.Assign(ref info);
        info.cdx = zprp.cdx;
    }

    public void PutOff(zone.zbg_ info)
    {
        if (info.cdx != -1)
        {
            Unactive(info.oType, info.cdx);
            info.cdx = -1;
        }
    }

}
