
public enum ZF_Body : byte
{
    TankTop,
    TShirt,
    WhiteTShirt,
    BlackDress,
    RedDress,
    WhiteDress,
    Skirt,
    Top,
    Underwear,
    FarmerA,
    FarmerB,
    PunkA,
    PunkB,
    Doctor,
    Nurse,
    Nun,
    Max = 16
}

public enum ZF_Head : byte
{
    Style1, Style2, Style3, Style4, Style5, Style6, Style7, Style8, Style9, Style10,
    Style11, Style12, Style13, Style14, 
    Max = 14
}

public class ZFModel
{
    public static ZF_Body GetRandBody(ZF_Body idx0, ZF_Body idx2)
    {
        return (ZF_Body)UnityEngine.Random.Range((int)idx0, (int)idx2);
    }
    public static ZF_Body GetRandBody()
    {
        return (ZF_Body)UnityEngine.Random.Range(0, (int)ZF_Body.Max);
    }

    public static ZF_Head GetRandHead(ZF_Head idx0, ZF_Head idx2)
    {
        return (ZF_Head)UnityEngine.Random.Range((int)idx0, (int)idx2);
    }
    public static ZF_Head GetRandHead()
    {
        return (ZF_Head)UnityEngine.Random.Range(0, (int)ZF_Head.Max);
    }

}