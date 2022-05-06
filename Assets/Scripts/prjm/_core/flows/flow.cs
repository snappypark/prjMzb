using nj;

public abstract class Flow : Flow_Abs
{
    public override byte iType { get { return (byte)Type; } }
    protected virtual eType Type { get { return eType.None; } }

    public enum eType : byte
    {
        None = 0,
        Menu,
        SoloPlay,
        MultiLobby,
        MultiPlay,
        EditMap,
    }
    
    public const byte iTypeMenu = 1;
    public const byte iTypePlay = 2;
    public const byte iTypeMultiLobby = 3;
    public const byte iTypeMultiSession = 4;
    public const byte iTypeEditMap = 5;

}
