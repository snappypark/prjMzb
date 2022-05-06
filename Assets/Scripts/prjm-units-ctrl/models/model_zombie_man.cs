
public enum ZM_Body : byte
{
    TankTop,
    TShirt,
    Jacket,
    Torso,
    RedTie,
    BlackSuit,
    GreySuit,
    Robust,
    No_Arm,
    FarmerA, FarmerB,
    PoliceA, PoliceB,
    RiotPolice, 
    PunkA, PunkB,
    Doctor, Nurse,
    Butcher,
    Priest,
    Soldier,
    Max =  21
}

public enum ZM_Head : byte
{
    Style1, Style2, Style3, Style4, Style5, Style6, Style7, Style8, Style9, Style10,
    Style11, Style12, Style13, Style14, Style15, Style16, Style17, Style18, Style19, Style20,
    Style21, 
    Max = 21
}

public class ZMModel
{
    public static ZM_Body GetRandBody(ZM_Body idx0, ZM_Body idx2)
    {
        return (ZM_Body)UnityEngine.Random.Range((int)idx0, (int)idx2);
    }
    public static ZM_Body GetRandBody()
    {
        return (ZM_Body)UnityEngine.Random.Range(0, (int)ZM_Body.Max);
    }

    public static ZM_Head GetRandHead(ZM_Head idx0, ZM_Head idx2)
    {
        return (ZM_Head)UnityEngine.Random.Range((int)idx0, (int)idx2);
    }
    public static ZM_Head GetRandHead()
    {
        return (ZM_Head)UnityEngine.Random.Range(0, (int)ZM_Head.Max);
    }

}