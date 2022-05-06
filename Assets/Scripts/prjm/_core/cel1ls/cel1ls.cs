
public class cel1ls : nj.MonoSingleton<cel1ls>
{
    public const int MaxX = 300, MaxY = 1, MaxZ = 300;

    nj.arrx3d<cel1l> _arrPool = null;
    public virtual cel1l this[int x, int z] { get { return _arrPool[x, 0, z]; } /*set { _arrPool[x, y, z] = value; }*/ }
    public virtual cel1l this[Pt pt] { get { return _arrPool[pt.x, pt.y, pt.z]; } /*set { _arrPool[x, y, z] = value; }*/ }
    public virtual cel1l this[i2 pt] { get { return _arrPool[pt.x, 0, pt.z]; } /*set { _arrPool[x, y, z] = value; }*/ }

    void Awake()
    {
        _arrPool = new nj.arrx3d<cel1l>(MaxX, MaxY, MaxZ, 1, 3, true);
        for (int x = 0; x < MaxX; ++x)
                for (int z = 0; z < MaxZ; ++z)
                {
                    cel1l c = _arrPool[x, 0, z];
                    c.ct = new UnityEngine.Vector3(x+0.5f, 0, z + 0.5f);
                    c.ps00 = new f2(x, z); c.ps11 = new f2(x+1, z+1);
                    c.pt = new Pt(x, 0, z);
                    c.zn = zone.Empty;
                    c.pNode.cell = c;
                }
    }

    public void Set(int x0, int z0, int x1, int z1, int y, 
        cel1l.Type type_, cel1l.ColliType colliType_)
    {
        for (int x = x0; x <= x1; ++x)
            for (int z = z0; z <= z1; ++z)
                _arrPool[x, 0, z].Set(type_, colliType_);
    }

    public void Set(int x, int z, cel1l.Type type_, cel1l.ColliType colliType_ )
    {
        _arrPool[x, 0, z].Set(type_, colliType_);
    }

    public void Clear()
    {
        for (int x = 0; x < MaxX; ++x)
            for (int y = 0; y < MaxY; ++y)
                for (int z = 0; z < MaxZ; ++z)
                    this[x, z].Clear();
    }

    public static bool IsOutIdx(int x, int z)
    {
        return x < 0 || z < 0 || x >= MaxX || z >= MaxZ;
    }
    public static bool IsOutIdx(int x, int y, int z)
    {
        return x < 0 || y < 0 || z < 0 || x >= MaxX || y >= MaxY || z >= MaxZ;
    }
    public static bool IsOutIdx(Pt pt)
    {
        return (pt.x < 0 || pt.y < 0 || pt.z < 0 || pt.x >= MaxX || pt.y >= MaxY || pt.z >= MaxZ);
    }

    
    public static NearZnType NearZoneType(cel1l c)
    {
        if (c.East().zn.idx != -1 && c.East().zn.idx != c.zn.idx)
            return NearZnType.East;
        else if (c.West().zn.idx != -1 && c.West().zn.idx != c.zn.idx)
            return NearZnType.West;
        else if (c.North().zn.idx != -1 && c.North().zn.idx != c.zn.idx)
            return NearZnType.North;
        else if (c.South().zn.idx != -1 && c.South().zn.idx != c.zn.idx)
            return NearZnType.South;
        return NearZnType.None;
    }

    public enum NearZnType
    {
        None,
        East,
        West,
        North,
        South,
    }
}

