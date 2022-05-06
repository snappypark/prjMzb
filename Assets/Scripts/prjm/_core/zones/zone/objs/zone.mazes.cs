using System.Collections.Generic;
using UnityEngine;

public partial class zone
{
    public struct mz
    {
        public i4 bd;
        public int szR, szC, numR, numC;
        public byte mat;
        public mz(i4 bd_, int szR_, int szC_, int numR_, int numC_, byte mat_)
        {
            bd = bd_;
            szR = szR_;  szC = szC_;
            numR = numR_;  numC = numC_;
            mat = mat_;
        }
        public int hszR { get { return ((szR-1) + Random.Range(0, 2)) >> 1; } }
        public int hszC { get { return ((szC-1) + Random.Range(0, 2)) >> 1; } }

        public int bdR(int numCell)
        {
            return bd.X0 + szR * numCell + hszR;
        }
        public int bdC(int numCell)
        {
            return bd.Z0 + szC * numCell + hszC;
        }
    }

    public List<mz> mzs = new List<mz>();
    static List<openCell> _openLst = new List<openCell>();
    
    public void AddMazeWalls(
        i4 bd, int szR, int szC, int numR, int numC, wal4Mat mats, cel1l.Type type,
        int rand0_, int option0_, bool usdRandJs = true)
    {
        mzs.Add(new mz(bd, szR, szC, numR, numC, mats.Top));

        if (usdRandJs)
            Random.InitState(rand0_);
        else
            core.loads.InitMazeRandState();

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

        while (_openLst.Count > 0)
        {
            openCnt = 0;
            openCell ctv = _openLst[Random.Range(0, _openLst.Count)];
            
            if (ctv.c + 1 < numC && !cells[ctv.r, ctv.c + 1].IsVisited && !cells[ctv.r, ctv.c + 1].IsOpen)
                openDirs[openCnt++] = openCell.eDir.front;
            else if (!cells[ctv.r, ctv.c].IsVisited && ctv.fromDir != openCell.eDir.back)
            {
                cells[ctv.r, ctv.c].WallFront = true;
                if (ctv.c + 1 < numC)
                    cells[ctv.r, ctv.c + 1].WallBack = true;
                addWallX(bd.X0 + ctv.r * szR, bd.Z0 + ctv.c * szC + (szC - 1), szR - 1, mats, type);
            }
            if (ctv.r + 1 < numR && !cells[ctv.r + 1, ctv.c].IsVisited && !cells[ctv.r + 1, ctv.c].IsOpen)
                openDirs[openCnt++] = openCell.eDir.right;
            else if (!cells[ctv.r, ctv.c].IsVisited && ctv.fromDir != openCell.eDir.left)
            {

                cells[ctv.r, ctv.c].WallRight = true;
                if (ctv.r + 1 < numR)
                    cells[ctv.r + 1, ctv.c].WallLeft = true;
                addWallZ(bd.X0 + ctv.r * szR + (szR - 1), bd.Z0 + ctv.c * szC, szC - 1, mats, type);
            }
            if (ctv.c > 0 && ctv.c - 1 >= 0 && !cells[ctv.r, ctv.c - 1].IsVisited && !cells[ctv.r, ctv.c - 1].IsOpen)
                openDirs[openCnt++] = openCell.eDir.back;
            else if (!cells[ctv.r, ctv.c].IsVisited && ctv.fromDir != openCell.eDir.front)
            {

                cells[ctv.r, ctv.c].WallBack = true;
                if (ctv.c > 0 && ctv.c - 1 >= 0)
                    cells[ctv.r, ctv.c - 1].WallFront = true;
                addWallX(bd.X0 + ctv.r * szR, bd.Z0 + ctv.c * szC, szR - 1, mats, type);
            }
            if (ctv.r > 0 && ctv.r - 1 >= 0 && !cells[ctv.r - 1, ctv.c].IsVisited && !cells[ctv.r - 1, ctv.c].IsOpen)
                openDirs[openCnt++] = openCell.eDir.left;
            else if (!cells[ctv.r, ctv.c].IsVisited && ctv.fromDir != openCell.eDir.right)
            {
                cells[ctv.r, ctv.c].WallLeft = true;
                if (ctv.r > 0 && ctv.r - 1 >= 0)
                    cells[ctv.r - 1, ctv.c].WallRight = true;
                addWallZ(bd.X0 + ctv.r * szR, bd.Z0 + ctv.c * szC, szC - 1, mats, type);
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
    }

    struct cell
    {
        public bool IsVisited, IsGoal, IsOpen;
        public bool WallRight, WallFront, WallLeft, WallBack;
        public cell(bool reset)
        {
            IsVisited = reset; IsGoal = reset; IsOpen = reset;
            WallRight = reset; WallFront = reset;
            WallLeft = reset; WallBack = reset;

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
