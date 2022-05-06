using UnityEngine;

[System.Serializable]
public class matTiles
{
    public enum Category { One, One45Rot, NxN }
    public enum Type : byte { Gray = 0, Blue, Green, Red, Yello}
    public const byte Gray = 0, Blue=1, Green=2, Red=3, Yello=4;

    [SerializeField] Material[] _mats;

    public Material this[Category category, byte idx]
    {
        get
        {
            return _mats[idx];
        }
    }

}
