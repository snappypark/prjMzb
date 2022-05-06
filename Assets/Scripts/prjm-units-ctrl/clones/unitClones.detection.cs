using System.Runtime.CompilerServices;
using System.Collections.GenericEx;
using UnityEngine;

public partial class unitClones/*.detection :*/
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void MovePosCell(unit unit, Vector3 pos)
    {
        unit.tran.localPosition = pos;
        Pt newPt = cel1l.Pt(pos);
        if (newPt != unit.pt)
        {
            unit.pt = newPt;
            unit.cell.units.Deq(unit.cdx);
            if (!cel1ls.IsOutIdx(newPt))
            {
                unit.cell = core.zells[newPt];
                unit.cell.units.Enqueue(unit.cdx);
            }
        }
    }

    /*
    public static void MoveCellFast(unit unit, Vector3 pos)
    {
        Pt newPt = cel1l.Pt(pos);
        if (newPt != unit.pt)
        {
            unit.pt = newPt;
            unit.cell.units.Deq(unit.cdx);
            unit.cell = core.cel1ls[newPt];
            unit.cell.units.Enqueue(unit.cdx);
        }
    }

    public static void MoveCell(unit unit, Vector3 pos)
    {
        Pt newPt = cel1l.Pt(pos);
        if (newPt != unit.pt)
        {
            unit.pt = newPt;
            unit.cell.units.Deq(unit.cdx);
            if (!cel1ls.IsOutIdx(newPt))
            {
                unit.cell = core.cel1ls[newPt];
                unit.cell.units.Enqueue(unit.cdx);
            }
        }
    }
    */

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unit Detect(Vector3 pos, short cdx)
    {
        Pt pt11 = cel1l.Pt11(pos);
        if (cel1ls.IsOutIdx(pt11))
            return null;

        cel1l c11 = core.zells[pt11];

        for (int i = 0; i < nj.idx.MaxNum2x2; ++i)
        {
            Pt pt = new Pt(pt11.x + nj.idx.INxN(i), pt11.y,
                            pt11.z + nj.idx.JNxN(i) );

            if (cel1ls.IsOutIdx(pt))
                continue ;

            cel1l c = core.zells[pt];
            int num = c.units.Count;
            for (int r = 0; r < num; ++r)
            {
                unit unit = c.RollUnit;
                if (unit.checkHp0_SameCdx(cdx))
                    continue;
                if(unit.tran == null)
                    continue;
                if ( f2.VecXZ(unit.tran.localPosition, pos).SqrMagnitude() < dist.sqr0_4)
                    return unit;
            }
        }
        return null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Alignments(unit u_, Vector3 pos, Vector3 dir, out Vector3 newDir)
    {
        bool result = false;
        newDir = Vector3.zero;
        cel1l c11 = core.zells[cel1l.Pt11(pos)];
        for (int i = 0; i < nj.idx.MaxNum2x2; ++i)
        {
            cel1l c = c11.C2X2(i);
            int num = c.units.Count;
            for (int r = 0; r < num; ++r)
            {
                unit unit = c.RollUnit;
                if (unit.checkHp0_SameCdx(u_.cdx) )
                    continue;

                f2 vTo = f2.VecXZ(unit.tran.localPosition, pos);
                if (vTo.DotXZ(dir) < 0)
                    continue;
                float sv = vTo.SqrMagnitude();
                if (sv < dist.sqr0_64)
                {
                    float srv = (1.2f - sv);// *0.5f;
                    //float srv = (Dist.sqr0_7 - sv);
                    f2 right = vTo.CrossProduct();

                    if (right.DotXZ(dir) > 0)
                        newDir += new Vector3(right.x - vTo.x, 0, right.z - vTo.z) * srv;
                    else
                        newDir -= new Vector3(right.x + vTo.x, 0, right.z + vTo.z) * srv;

                    result = true;
                }
            }
        }
        return result;
    }
}
