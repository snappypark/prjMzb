using UnityEngine;

[System.Serializable]
public class matProps
{
    public const byte Tree1A = 0, Tree1B = 1, Tree2A = 2, Tree2B = 3, Tnt = 4, Red = 5;

    [SerializeField] Material[] _mats;
    public Material this[byte idx] { get { return _mats[idx]; } }

    public enum Type : byte
    {
        Tree1A = 0, Tree1B, Tree2A, Tree2B, Tnt, Red,
        None = 222,
    }
}
