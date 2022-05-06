using UnityEngine;

public class cel5l
{
    public const int Size = 5;
    public const float Half = 2.5f;
    public const float OvSz = 0.2f; // 1/5
    public const float OvSzY = 0.3333333333f; // 1/3
    
    public static Pt Pt(Vector3 pos)
    {
        return new Pt((int)(pos.x * OvSz), (int)(pos.y * 0.333333333333f), (int)(pos.z * OvSz));
    }
    
    public static Vector3 Center(Vector3 pos)
    {
        return new Vector3((int)(pos.x * OvSz) * Size + Half, pos.y, (int)(pos.z * OvSz) * Size + Half);
    }

    public static Vector3 Pos00(Vector3 pos)
    {
        return new Vector3((int)(pos.x * OvSz) * Size, pos.y, (int)(pos.z * OvSz) * Size);
    }

    public static Vector3 Pos11(Vector3 pos)
    {
        return new Vector3((int)(pos.x * OvSz) * Size + Size, pos.y, (int)(pos.z * OvSz) * Size + Size);
    }
}
