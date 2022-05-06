using System.Collections;
using System.Collections.Generic;
using System.Collections.GenericEx;
using UnityEngine;
using UnityEngineEx;

public partial class uj_AllStgs
{
    void resetAddMazeAndNpc(js.zone_ zn, znm zm, zmSZ zms)
    {
        zn.wallsLines.Clear();
        zn.walls.Clear();
        core.zells.Set(zm.bd.X0, zm.bd.Z0, zm.bd.X1, zm.bd.Z1, 0, cel1l.Type.Tile, cel1l.ColliType.None);

        addMazeAndNpc(zn, zm.bd, zms.szGapR, zms.szGapC, zms.numGapR, zms.numGapC,
                          wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.WithCell);
        js.Inst.SetCellOfWallDel(zn);
    }

    List<js.wallsRect_> _tmpWLines = new List<js.wallsRect_>();
    List<js.wallSingle_> _tmpWs = new List<js.wallSingle_>();
    IEnumerator generateZone(js.zone_ zn, znm zm, zmSZ zms)
    {
        cel1l c0 = zn.wallsDel[0].getCell00();
        cel1l c1 = zn.wallsDel[1].getCell00();

        zn.rprs.Clear();
        resetAddMazeAndNpc(zn, zm, zms);

        _tmpWLines.Clear();
        _tmpWs.Clear();
        float minJsp1 = 0.219f * zms.szGapL() * (zms.NumGapL() + 1);
        float minJspAll = 1.0f * (zms.szGapS() * (zms.NumGapL() - 0.5f));// - (zms.szGapS()-1)*1.0f);
        Debug.Log(zms.szGapL() + "," + zms.NumGapL() + " [RE]: " + minJspAll + "(" + minJspAll * minJspAll + ")" + "," + minJsp1 + "(" + minJsp1 * minJsp1 + ")");
        core.zones.InitcalcPathGoalForLong(128); //128
        bool recal = true;
        while (recal)
        {
            yield return zn.zn.InitJsp_();

            zones.autoCalcPathType calcResult = core.zones.calcPathGoalAuto(c0, c1, minJspAll * minJspAll, minJsp1 * minJsp1);
            switch (calcResult)
            {
                case zones.autoCalcPathType.Found:
                    recal = false;
                    break;
                case zones.autoCalcPathType.EndWithSave:
                    recal = false;
                    zn.wallsLines.CopyOf(_tmpWLines);
                    zn.walls.CopyOf(_tmpWs);
                    core.zells.Set(zm.bd.X0, zm.bd.Z0, zm.bd.X1, zm.bd.Z1, 0, cel1l.Type.Tile, cel1l.ColliType.None);

                    for (int i = 0; i < zn.wallsLines.Count; ++i)
                    {
                        i4 bd = zn.wallsLines[i].bd;
                        core.zells.Set(bd.X0, bd.Z0, bd.X1, bd.Z1, 0, cel1l.Type.Wall, cel1l.ColliType.Cube);
                    }
                    for (int i = 0; i < zn.walls.Count; ++i)
                        core.zells.Set(zn.walls[i].x, zn.walls[i].z, cel1l.Type.Wall, cel1l.ColliType.Cube);
                    js.Inst.SetCellOfWallDel(zn);
                    break;
                case zones.autoCalcPathType.RetryWithSave:
                    _tmpWLines.CopyOf(zn.wallsLines);
                    _tmpWs.CopyOf(zn.walls);
                    break;
            }

            switch (calcResult)
            {
                case zones.autoCalcPathType.RetryWithSave:
                case zones.autoCalcPathType.Retry:
                    resetAddMazeAndNpc(zn, zm, zms);
                    break;
            }
        }

        yield return null;

        cnd[,] cellnodes = new cnd[zms.numGapR, zms.numGapC];
        initCellNodes(zn, ref cellnodes, zm.bd, zms.szGapR, zms.szGapC, zms.numGapR, zms.numGapC);

        generateBlockWalls(zn, zms.szGapR, zms.szGapC);
        generateBushs(zn, zms.szGapR, zms.szGapC);

        generateRprs(zn, zms.szGapR, zms.szGapC);
        generateTraps(zn, ref cellnodes);
        generateMeteor(zn);
        generateNpcs_(zn, ref cellnodes);
    }

    void initCellNodes(js.zone_ zn, ref cnd[,] cellnodes, i4 bd, int szR, int szC, int numR, int numC)
    {
        cnd.pathMiddleNodes.Clear();
        cnd.pathNodes.Clear();
        cnd.Nodes.Clear();

        cel1l c0 = zn.wallsDel[0].getCell00();
        cel1l c1 = zn.wallsDel[1].getCell00();

        for (int r = 0; r < numR; r++)
            for (int c = 0; c < numC; c++)
            {
                cellnodes[r, c] = new cnd(r, c);
                cnd.Nodes.Insert(Random.Range(0, cnd.Nodes.Count), cellnodes[r, c]);
            }

        cnd.bd = bd;
        cnd.szR = szR; cnd.szC = szC;
        cnd.nR = numR; cnd.nC = numC;
        
        int rb = (c0.pt.x - bd.X0) / szR; int cb = (c0.pt.z - bd.Z0) / szC;
        int re = (c1.pt.x - bd.X0) / szR; int ce = (c1.pt.z - bd.Z0) / szC;
        cnd.begin = cellnodes[rb, cb];
        cnd.end = cellnodes[re, ce];

        cnd.begin.visited = true;
        setCellNodeLinks(cnd.begin, ref cellnodes);
        setCellNodePath(cnd.end, zn);
        setCellNodeDist(ref cellnodes);
    }

    void setCellNodeLinks(cnd node, ref cnd[,] cellnodes)
    {
        node.visited = true;
        if (node.c + 1 < cnd.nC && isLinked(node, cellnodes[node.r, node.c + 1]))
        {
            cellnodes[node.r, node.c + 1].o = node;
            setCellNodeLinks(cellnodes[node.r, node.c + 1], ref cellnodes);
        }

        if (node.c > 0 && isLinked(node, cellnodes[node.r, node.c - 1]))
        {
            cellnodes[node.r, node.c - 1].o = node;
            setCellNodeLinks(cellnodes[node.r, node.c - 1], ref cellnodes);
        }

        if (node.r > 0 && isLinked(node, cellnodes[node.r - 1, node.c]))
        {
            cellnodes[node.r - 1, node.c].o = node;
            setCellNodeLinks(cellnodes[node.r - 1, node.c], ref cellnodes);
        }

        if (node.r + 1 < cnd.nR && isLinked(node, cellnodes[node.r + 1, node.c]))
        {
            cellnodes[node.r + 1, node.c].o = node;
            setCellNodeLinks(cellnodes[node.r + 1, node.c], ref cellnodes);
        }
    }

    void setCellNodePath(cnd node, js.zone_ zn)
    {
        if(cnd.begin.DistTo(node) > 15 && cnd.end.DistTo(node) > 10)
            cnd.pathMiddleNodes.Insert(Random.Range(0, cnd.pathMiddleNodes.Count), node);

        cnd.pathNodes.Insert(Random.Range(0, cnd.pathNodes.Count), node);
        node.path = true;
        if (node.o != null)
            setCellNodePath(node.o, zn);
    }

    void setCellNodeDist(ref cnd[,] cellnodes)
    {
        for (int r = 0; r < cnd.nR; r++)
            for (int c = 0; c < cnd.nC; c++)
            {
                cnd node = cellnodes[r, c];
                node.dist = i2.Dist(node.r * cnd.szR, node.c * cnd.szC, cnd.begin.r * cnd.szR, cnd.begin.c * cnd.szC);
                node.distPath = getNodeDistToPath(node);
            }

        cnd.totalDistPath = getNodeDistToEnd(cnd.end);
        INFO.dist += cnd.totalDistPath;
    }
    
    float getNodeDistToPath(cnd node)
    {
        return (!node.path && node.o != null) ? node.DistTo(node.o) + getNodeDistToPath(node.o) : 0;
    }

    float getNodeDistToEnd(cnd node)
    {
        return (node.o != null) ? node.DistTo(node.o) + getNodeDistToEnd(node.o) : 0;
    }

    bool isLinked(cnd n1, cnd n2)
    {
        if (n2.visited)
            return false;
        int x1 = cnd.bd.X0 + n1.r * cnd.szR + 1;
        int z1 = cnd.bd.Z0 + n1.c * cnd.szC + 1;

        int x2 = cnd.bd.X0 + n2.r * cnd.szR + 1;
        int z2 = cnd.bd.Z0 + n2.c * cnd.szC + 1;

        int minX = Mathf.Min(x1, x2);
        int maxX = Mathf.Max(x1, x2);
        int minZ = Mathf.Min(z1, z2);
        int maxZ = Mathf.Max(z1, z2);

        for (int x = minX; x <= maxX; ++x)
            for (int z = minZ; z <= maxZ; ++z)
                if (core.zells[x, z].type == cel1l.Type.Wall)
                    return false;

        return true;
    }


    class cnd
    {
        public static int szR, szC, nR, nC;
        public static i4 bd;
        public static cnd begin, end;
        public static List<cnd> pathMiddleNodes = new List<cnd>();
        public static List<cnd> pathNodes = new List<cnd>();
        public static List<cnd> Nodes = new List<cnd>();
        public static float totalDistPath;

        public cnd o = null;
        public bool visited = false, path = false;
        public int r, c;
        public float dist;
        public float distPath;
        public cnd(int r_, int c_) { r = r_; c = c_; }

        public float DistTo(cnd cnd_)
        {
            return i2.Dist(r * szR, c * szC, cnd_.r * szR, cnd_.c * szC);
        }

        public i2 GetPt(js.zone_ zn)
        {
            return new i2(zn.bd.X0 + r * szR, zn.bd.Z0 + c * szC);
        }

        public i2 GetCenter(js.zone_ zn)
        {
            int x = zn.bd.X0 + r * szR;
            int z = zn.bd.Z0 + c * szC; 
            return new i2(Random.Range(x + 1, x + szR - 2),
                            Random.Range(z + 1, z + szC - 2));
        }

        public i4 GetRightSide(js.zone_ zn)
        {
            return new i4(zn.bd.X0 + (r+1)*szR -1, zn.bd.Z0 + c * szC,
                          zn.bd.X0 + (r+1)*szR -1, zn.bd.Z0 + (c+1)*szC -1);
        }

        public i4 GetLeftSide(js.zone_ zn)
        {
            return new i4(zn.bd.X0 + r*szR, zn.bd.Z0 + c * szC, 
                          zn.bd.X0 + r*szR, zn.bd.Z0 + (c+1)*szC -1);
        }

        public i4 GetForwardSide(js.zone_ zn)
        {
            return new i4(zn.bd.X0 + r * szR,      zn.bd.Z0 + (c+1)*szC -1,
                          zn.bd.X0 + (r+1)*szR -1, zn.bd.Z0 + (c+1)*szC -1);
        }

        public i4 GetBottomSide(js.zone_ zn)
        {
            return new i4(zn.bd.X0 + r*szR,        zn.bd.Z0 + c * szC, 
                          zn.bd.X0 + (r+1)*szR -1, zn.bd.Z0 + c * szC);
        }

        public i4 GetSideToPrevious(js.zone_ zn)
        {
            if (r < o.r)
                return GetRightSide(zn);
            else if (r > o.r)
                return GetLeftSide(zn);
            else if (c < o.c)
                return GetForwardSide(zn);
            return GetBottomSide(zn);
        }
    }

}
