using System.Collections;
using System.Collections.Generic;

public partial class zones : nj.MonoSingleton<zones>
{
    List<zone> _zones = new List<zone>();
    zone _cur = zone.Empty;

    public int Num { get { return _zones.Count; } }
    public zone this[int idx] { get { return _zones[idx]; } }

    public bool IsCurIdx(int idx) { return _cur.idx == idx; }

    public zone AddZone(int x0, int z0, int x1, int z1, int y_, 
        sights.eState eSight, bool noneEnter_ = false)
    {
        zone zn = new zone(_zones.Count, new i5(x0, z0, x1, z1, y_), eSight, noneEnter_);
        _zones.Add(zn);
        for (int x = x0; x <= x1; ++x)
            for (int z = z0; z <= z1; ++z)
                core.zells[x, z].zn = zn;
        return zn;
    }
    
    public void Clear()
    {
        for (int i = 0; i < _zones.Count; ++i)
            _zones[i].Clear();
        _zones.Clear();

        _znsNew.Reset();
        _znsOld.Reset();
        _znsHaving.Clear();
        _cur = zone.Empty;
    }

    public IEnumerator InitJsps_()
    {
        for (int i = 0; i < _zones.Count; ++i)
            yield return _zones[i].InitJsp_();
        yield return null;
    }

    public void Reset_AllWays()
    {
        for (int i = 0; i < _zones.Count; ++i)
        {
            zone zn = _zones[i];
            zn.adjx.Clear();
            zn.ways.Clear();
        }

        for (int i = 0; i < _zones.Count; ++i)
        {
            for (int j = i + 1; j < _zones.Count; ++j)
            {
                if (_zones[i].bd.IsContacted(_zones[j].bd))
                {
                    _zones[i].adjx.Add((byte)j);
                    _zones[j].adjx.Add((byte)i);
                }
            }
        }

    }
}
