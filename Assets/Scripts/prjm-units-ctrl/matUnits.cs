using UnityEngine;
using UnityEngineEx;

[System.Serializable]
public class matUnits
{
    public enum Type : byte
    {
        CitizenA = 0,
        CitizenB,
        Zomb0,
        Zomb1,
        Zomb2,
        Zomb3,
        Zomb4,
        Zomb5,
        Zomb6,
        Avata,
        OnDmg,

        Max
    }

    public const byte
        CitizenA = 0, CitizenB = 1,
        Zomb0 = 2, Zomb1 = 3, Zomb2 = 4,
        Zomb3 = 5, Zomb4 = 6, Zomb5 = 7, Zomb6 = 8,
        Avata = 9, OnDmg = 10;

    [SerializeField] Material[] _mats;
    public Material this[byte idx] { get { return _mats[idx]; } }

    public static byte ByType(byte type)
    {
        return type< 2 ? matUnits.CitizenA : matUnits.CitizenB;
    }


    public static byte GetByZombieLv(unit.SubType subType)
    {
        switch (subType)
        {
            case unit.SubType.ZombieLv1:
                return (byte)UnityEngine.Random.Range((int)matUnits.Type.Zomb0, (int)matUnits.Type.Zomb3);
            case unit.SubType.ZombieLv2:
                return Zomb3;
            case unit.SubType.ZombieLv3:
                return Zomb4;
            case unit.SubType.ZombieLv4:
                return Zomb5;
            case unit.SubType.ZombieLv5:
                return Zomb6;
        }
        return Zomb0;
    }
}
