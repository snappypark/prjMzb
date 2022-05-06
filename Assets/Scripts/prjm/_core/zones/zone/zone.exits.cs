using System.Collections.Generic;
using UnityEngine;

public partial class zone
{
    [HideInInspector] public List<i4> waycells = new List<i4>();
    [HideInInspector] public List<way_> ways = new List<way_>();

    public void AddWay(int x0, int z0, int x1, int z1)
    {
        waycells.Add(new i4(x0, z0, x1, z1));
        SetNoneWalls(x0, z0, x1, z1);

        cel1l next;
        wayDir waydir = getWayDor(core.zells[x0, z0], out next);
        if (waydir == wayDir.none) {
            Debug.LogWarning("addway");
            return;
        }

        way_ way = getWays(next.zn);
        if (way == null) {
            ways.Add(new way_(next.zn));
            way = ways[ways.Count - 1];
        }

        for (int x = x0; x <= x1; ++x)
            for (int z = z0; z <= z1; ++z)
            {
                cel1l c1 = core.zells[x, z];
                cel1l c2;
                switch (waydir)
                {
                    case wayDir.east:
                        c2 = c1.East();
                        way.Add(x, z, c2.pt.x, c2.pt.z);
                        break;
                    case wayDir.west:
                        c2 = c1.West();
                        way.Add(x, z, c2.pt.x, c2.pt.z);
                        break;
                    case wayDir.north:
                        c2 = c1.North();
                        way.Add(x, z, c2.pt.x, c2.pt.z);
                        break;
                    case wayDir.south:
                        c2 = c1.South();
                        way.Add(x, z, c2.pt.x, c2.pt.z);
                        break;
                }
            }
    }
    
    public bool GetClosestWay(Pt pt, zone nextZn, out i4 result)
    {
        result = i4.O;
        way_ way = getWays(nextZn);
        if(way == null || way.paths.Count == 0)
            return false;

        i4 path = way.paths[0];
        i2 gap = new i2(path.X0 - pt.x, path.Z0 - pt.z);
        float dist = gap.x * gap.x + gap.z * gap.z;
        result = path;

        for (int i = 1; i < way.paths.Count; ++i)
        {
            path = way.paths[i];
            gap = new i2(path.X0 - pt.x, path.Z0 - pt.z);
            float newDist = gap.x * gap.x + gap.z * gap.z;
            if (dist > newDist)
            {
                dist = newDist;
                result = path;
            }
        }
        return true;
    }
    
    enum wayDir { east, west, north, south, none }
    wayDir getWayDor(cel1l c1, out cel1l c2)
    {
        c2 = c1.East();
        if (c2.zn.idx != -1 && c2.zn.idx != c1.zn.idx)
            return wayDir.east;
        c2 = c1.West();
        if (c2.zn.idx != -1 && c2.zn.idx != c1.zn.idx)
            return wayDir.west;
        c2 = c1.North();
        if (c2.zn.idx != -1 && c2.zn.idx != c1.zn.idx)
            return wayDir.north;
        c2 = c1.South();
        if (c2.zn.idx != -1 && c2.zn.idx != c1.zn.idx)
            return wayDir.south;
        c2 = null;
        return wayDir.none;
    }

    public class way_
    {
        public zone next;
        public List<i4> paths = new List<i4>();
        public way_(zone next_) { next = next_; }
        public void Add(int x0, int z0, int x1, int z1)
        {
            paths.Add(new i4(x0, z0, x1, z1));
        }
    }

    way_ getWays(zone nextZn)
    {
        for (int i = 0; i < ways.Count; ++i)
            if (ways[i].next.idx == nextZn.idx)
                return ways[i];
        return null;
    }
}
