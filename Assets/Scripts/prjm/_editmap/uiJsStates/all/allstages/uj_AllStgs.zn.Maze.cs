using System.Collections;
using System.Collections.Generic;
using System.Collections.GenericEx;
using UnityEngine;
using UnityEngineEx;

public partial class uj_AllStgs
{
    static List<openCell> _openLst = new List<openCell>();
    void addMazeAndNpc(js.zone_ zn, i4 bd, int szR, int szC, int numR, int numC, wal4Mat mats, cel1l.Type type, js.SeriType seriType)
    {
        core.loads.InitMazeRandState();
        Random.InitState(UnityEngine.Random.Range(10, 10000000));

        cell[,] cells = new cell[numR, numC];
        for (int r = 0; r < numR; r++)
            for (int c = 0; c < numC; c++)
                cells[r, c] = new cell(false);

        openCell.eDir[] openDirs = new openCell.eDir[4];
        int openCnt = 0;
        int randR = Random.Range(0, numR);
        int randC = Random.Range(0, numC);
        _openLst.Clear();
        cells[randR, randC].IsOpen = true;
        _openLst.Add(new openCell(randR, randC, openCell.eDir.start));

        // MW
        while (_openLst.Count > 0)
        {
            openCnt = 0;
            openCell ctv = _openLst[Random.Range(0, _openLst.Count)];

            if (ctv.c + 1 < numC && !cells[ctv.r, ctv.c + 1].IsVisited && !cells[ctv.r, ctv.c + 1].IsOpen)
                openDirs[openCnt++] = openCell.eDir.front;
            else if (!cells[ctv.r, ctv.c].IsVisited && ctv.fromDir != openCell.eDir.back)
            {
                js.Inst.AddWallsLine(zn, new i4(
                    bd.X0 + ctv.r * szR,           bd.Z0 + ctv.c * szC + (szC - 1),
                    bd.X0 + ctv.r * szR + szR - 1, bd.Z0 + ctv.c * szC + (szC - 1)
                    ), mats, type, seriType);
            }

            if (ctv.r + 1 < numR && !cells[ctv.r + 1, ctv.c].IsVisited && !cells[ctv.r + 1, ctv.c].IsOpen)
                openDirs[openCnt++] = openCell.eDir.right;
            else if (!cells[ctv.r, ctv.c].IsVisited && ctv.fromDir != openCell.eDir.left)
            {
                js.Inst.AddWallsLine(zn, new i4(
                    bd.X0 + ctv.r * szR + (szR - 1), bd.Z0 + ctv.c * szC,
                    bd.X0 + ctv.r * szR + (szR - 1), bd.Z0 + ctv.c * szC + szC - 1
                    ), mats, type, seriType);
            }

            if (ctv.c > 0 && !cells[ctv.r, ctv.c - 1].IsVisited && !cells[ctv.r, ctv.c - 1].IsOpen)
                openDirs[openCnt++] = openCell.eDir.back;
            else if (!cells[ctv.r, ctv.c].IsVisited && ctv.fromDir != openCell.eDir.front)
            {
                js.Inst.AddWallsLine(zn, new i4(
                    bd.X0 + ctv.r * szR,           bd.Z0 + ctv.c * szC,
                    bd.X0 + ctv.r * szR + szR - 1, bd.Z0 + ctv.c * szC
                    ), mats, type, seriType);
            }

            if (ctv.r > 0 && !cells[ctv.r - 1, ctv.c].IsVisited && !cells[ctv.r - 1, ctv.c].IsOpen)
                openDirs[openCnt++] = openCell.eDir.left;
            else if (!cells[ctv.r, ctv.c].IsVisited && ctv.fromDir != openCell.eDir.right)
            {
                js.Inst.AddWallsLine(zn, new i4(
                    bd.X0 + ctv.r * szR, bd.Z0 + ctv.c * szC,
                    bd.X0 + ctv.r * szR, bd.Z0 + ctv.c * szC + szC - 1
                    ), mats, type, seriType);
            }

            if (!cells[ctv.r, ctv.c].IsVisited && openCnt == 0)
                cells[ctv.r, ctv.c].IsGoal = true;

            cells[ctv.r, ctv.c].IsVisited = true;

            if (openCnt > 0)
            {
                openCell.eDir dirtype = openDirs[Random.Range(0, openCnt)];
                if (dirtype == openCell.eDir.right)
                    _openLst.Add(new openCell(ctv.r + 1, ctv.c, openCell.eDir.right));
                else if (dirtype == openCell.eDir.front)
                    _openLst.Add(new openCell(ctv.r, ctv.c + 1, openCell.eDir.front));
                else if (dirtype == openCell.eDir.left)
                    _openLst.Add(new openCell(ctv.r - 1, ctv.c, openCell.eDir.left));
                else if (dirtype == openCell.eDir.back)
                    _openLst.Add(new openCell(ctv.r, ctv.c - 1, openCell.eDir.back));

                if (dirtype != openCell.eDir.start)
                    cells[_openLst[_openLst.Count - 1].r, _openLst[_openLst.Count - 1].c].IsOpen = false;
            }
            else
            {
                cells[ctv.r, ctv.c].IsOpen = false;
                _openLst.Remove(ctv);
            }
        }
        
        // Extra
        List<i2> lstRand = new List<i2>();

        for (int xt = bd.left; xt < bd.right; xt += szR)
            for (int zt = bd.bottom; zt < bd.top; zt += szC)
                lstRand.Insert(Random.Range(0, lstRand.Count), new i2(xt, zt));

        for (int i = 0; i < lstRand.Count; ++i)
        {
            i2 pt = lstRand[i];

            if (core.zells[pt.x - 1, pt.z - 1].type != cel1l.Type.Wall)
                js.Inst.AddWallsSingle(zn, pt.x, pt.z, mats, cel1l.Type.Wall, seriType);
            if (core.zells[pt.x + szR, pt.z + szC].type != cel1l.Type.Wall)
                js.Inst.AddWallsSingle(zn, pt.x + szR - 1, pt.z + szC - 1, mats, cel1l.Type.Wall, seriType);
            if (core.zells[pt.x - 1, pt.z + szC].type != cel1l.Type.Wall)
                js.Inst.AddWallsSingle(zn, pt.x, pt.z + szC - 1, mats, cel1l.Type.Wall, seriType);
            if (core.zells[pt.x + szR, pt.z - 1].type != cel1l.Type.Wall)
                js.Inst.AddWallsSingle(zn, pt.x + szR - 1, pt.z, mats, cel1l.Type.Wall, seriType);
        }
    }

    void digWallsOfZn(bool b1 = true, bool b2 = true, bool b3 = true, bool b4 = true)
    {
        switch (INFO.iBy6)
        {
            case 4:
                if (b1) digWallOfZn(js.Inst.zones[1], 8, 17);
                if (b2) digWallOfZn(js.Inst.zones[3], 14, 22);
                if (b3) digWallOfZn(js.Inst.zones[5], 17, 33);
                if (b4) digWallOfZn(js.Inst.zones[7], 22, 55);
                break;
            case 5:
                if (b1) digWallOfZn(js.Inst.zones[1], 14, 22);
                if (b2) digWallOfZn(js.Inst.zones[3], 18, 33);
                if (b3) digWallOfZn(js.Inst.zones[5], 25, 44);
                if (b4) digWallOfZn(js.Inst.zones[7], 33, 66);
                break;
        }

    }


    //Re wall For s
    //16 20 25 33 50 67 75 80 84
    List<js.wallsRect_> _newLines = new List<js.wallsRect_>();
    void digWallOfZn(js.zone_ zn, int perctOfDig, int perctOfDig2)
    {
        _newLines.Clear();
        addBoundaryOfLines(zn);
        splitLines(zn, perctOfDig, perctOfDig2);
        zn.wallsLines.CopyOf(_newLines);
    }

    void addBoundaryOfLines(js.zone_ zn)
    {
        int i = 0;
        while (i < zn.wallsLines.Count)
        {
            i4 wbd = zn.wallsLines[i].bd;
            if ((zn.bd.X0 == wbd.X0 && zn.bd.X0 == wbd.X1) ||
                 (zn.bd.X1 == wbd.X0 && zn.bd.X1 == wbd.X1) ||
                 (zn.bd.Z0 == wbd.Z0 && zn.bd.Z0 == wbd.Z1) ||
                 (zn.bd.Z1 == wbd.Z0 && zn.bd.Z1 == wbd.Z1))
            {
                _newLines.Add(new js.wallsRect_(wbd, wal4Mat.N(_mats.wall), cel1l.Type.Wall));
                zn.wallsLines.RemoveAt(i);
            }
            else
                ++i;
        }
    }

    List<Pt> _wals = new List<Pt>();
    void splitLines(js.zone_ zn, int perctOfDig, int perctOfDig2)
    {
        while (zn.wallsLines.Count > 0)
        {
            i4 wbd = zn.wallsLines[0].bd;
            zn.wallsLines.RemoveAt(0);

            if (RandEx.TruePerCt(perctOfDig))//ddddddd
            {
                _wals.Clear();
                for (int x = wbd.X0; x <= wbd.X1; ++x)
                    for (int z = wbd.Z0; z <= wbd.Z1; ++z)
                    {
                        if ((x == wbd.X0 && z == wbd.Z0) || (x == wbd.X1 && z == wbd.Z1))
                            js.Inst.AddWallsSingle(zn, x, z, wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.Default);
                        else
                            _wals.Add(new Pt(x, z));
                    }

                switch (_wals.Count)
                {
                    case 2:
                        if (RandEx.TruePerCt(50))
                            js.Inst.AddWallsSingle(zn, _wals[0].x, _wals[0].z, wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.Default);
                        else
                            js.Inst.AddWallsSingle(zn, _wals[1].x, _wals[1].z, wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.Default);
                        break;
                    case 3:
                        if (RandEx.TruePerCt(perctOfDig2))
                            js.Inst.AddWallsSingle(zn, _wals[0].x, _wals[0].z, wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.Default);
                        if (RandEx.TruePerCt(perctOfDig2))
                            js.Inst.AddWallsSingle(zn, _wals[1].x, _wals[1].z, wal4Mat.N(_mats.wall), cel1l.Type.Wall, js.SeriType.Default);
                        break;
                }
            }
            else
                _newLines.Add(new js.wallsRect_(wbd, wal4Mat.N(_mats.wall), cel1l.Type.Wall));
        }
    }

    struct cell
    {
        public bool IsVisited, IsGoal, IsOpen;
        public cell(bool reset)
        {
            IsVisited = reset; IsGoal = reset; IsOpen = reset;
        }
    }

    struct openCell
    {
        public enum eDir
        {
            start = 0, right, front, left, back,
        };

        public int r, c;
        public eDir fromDir;

        public openCell(int r_, int c_, eDir fromDir_)
        {
            r = r_; c = c_; fromDir = fromDir_;
        }
    }

}
