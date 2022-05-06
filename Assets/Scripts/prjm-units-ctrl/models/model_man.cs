
public enum M_Body : byte
{
    // 2
    TShirtBlack, ShirtRed, Shirt,
    TankTopA, TankTopB,

    //3
    Sports, SportsChain,
    Jacket, JacketBlack,
    SuitGrey, SuitBrown, SuitWhite, RedTie, SuitBlack, Tuxedo,

    // 1
    TShirtWhite, TShirtYellow,
    PoloPink, PoloBlue,
    Stripes,

    UnderwearWhite, UnderwearBlack,
    Swimsuit,

    //4
    LeatherJacket, GreenJacket,
    BanditA, BanditB,
    WorkerA, WorkerB,
    
    //8
    PunkA, PunkB, PunkC, PunkD,

    //5
    RockerA, RockerB,
    Butcher,

    //6
    FarmerA, FarmerB, FarmerC,

    //
    PoliceA, PoliceB, PoliceC, PoliceD, PoliceE, 

    //7
    Sailor,
    Priest,
    Doctor,
    Nurse,
    Pilot,
    Max = 49
}

public enum M_Head : byte
{
    Style1, Style2, Style3, Style4, Style5, Style6, Style7, Style8, Style9, Style10,
    Style11, Style12, Style13, Style14, Style15, Style16, Style17, Style18, Style19, Style20,
    Style21, Style22, Style23, Style24, Style25, Style26, Style27, Style28, Style29, Style30,
    Style31, Style32, Style33, Style34, Style35, Style36, Style37, Style38, Style39, Style40,
    Style41, Style42, Style43, Style44, Style45, Style46, Style47, Style48, Style49, Style50,
    Style51, Style52, Style53, Style54, Style55, Style56, Style57, Style58, Style59, Style60,
    Style61, Style62, Style63, Style64, Style65, Style66,
    Max = 66
}


public class MModel
{
    public static M_Body GetRandBody(M_Body idx0, M_Body idx2)
    {
        return (M_Body)UnityEngine.Random.Range((int)idx0, (int)idx2);
    }
    public static M_Body GetRandBody()
    {
        return (M_Body)UnityEngine.Random.Range(0, (int)M_Body.Max);
    }

    public static M_Head GetRandHead(M_Head idx0, M_Head idx2)
    {
        return (M_Head)UnityEngine.Random.Range((int)idx0, (int)idx2);
    }
    public static M_Head GetRandHead()
    {
        return (M_Head)UnityEngine.Random.Range(0, (int)M_Head.Max);
    }

}