
public enum F_Body : byte
{
    // 2
    White, Black, Red, Casual,
    TankTop, TankTopBlack,

    //3
    SportsSkirt, SportsBlue, SportsPink, SportsYellow,
    Office, SuitBlack, SuitGrey, SuitPink,

    //4
    JacketBlack, JacketLeather,
    DressWhite, DressBlack, DressPink, DressRed,

    // 1
    UnderwearWhite,
    UnderwearBlack,
    UnderwearRed,
    LingerieWhite,
    LingerieBlack,

    //8
    PoliceA, PoliceB, PoliceC, PoliceD,

    //5
    Nun,
    FarmerA, FarmerB, FarmerC,
    //6
    Nurse, NurseWhite, Doctor,
    //7
    PunkA, PunkB, PunkC, PunkD,

    Max = 40
}

public enum F_Head : byte
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

public class FModel
{
    public static F_Body GetRandBody(F_Body idx0, F_Body idx2)
    {
        return (F_Body)UnityEngine.Random.Range((int)idx0, (int)idx2);
    }
    public static F_Body GetRandBody()
    {
        return (F_Body)UnityEngine.Random.Range(0, (int)F_Body.Max);
    }

    public static F_Head GetRandHead(F_Head idx0, F_Head idx2)
    {
        return (F_Head)UnityEngine.Random.Range((int)idx0, (int)idx2);
    }
    public static F_Head GetRandHead()
    {
        return (F_Head)UnityEngine.Random.Range(0, (int)F_Head.Max);
    }

}