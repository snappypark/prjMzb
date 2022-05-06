using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngineEx;
using System.Collections.GenericEx;

public partial class unit
{
    #region Set 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetPosDir(Vector3 pos, Vector3 forward)
    {
        tran.localPosition = pos;
        tran.forward = forward;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetCell()
    {
        Pt newPt = cel1l.Pt(tran.localPosition);
        if (newPt != pt)
        {
            pt = newPt;
            cell.units.Deq(cdx);
            if (!cel1ls.IsOutIdx(newPt))
            {
                cell = core.zells[newPt];
                cell.units.Enqueue(cdx);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OutCell()
    {
        pt = Pt.Zero;
        cell.units.Deq(cdx);
    }

    #endregion


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetCell2()
    {
        Pt newPt = cel1l.Pt(tran.localPosition);
        if (newPt != pt)
        {
            pt = newPt;
            cell.units.Deq(cdx);
            cell = core.zells[newPt];
            cell.units.Enqueue(cdx);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Move(Vector3 pos)
    {
        tran.localPosition = pos;
        Pt newPt = cel1l.Pt(pos);
        if (newPt != pt)
        {
            pt = newPt;
            cell.units.Deq(cdx);
            if (!cel1ls.IsOutIdx(newPt))
            {
                cell = core.zells[newPt];
                cell.units.Enqueue(cdx);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public cel1l ForwardCell()
    {
        int gapX = (int)(tran.forward.x * 1.4143f);
        int gapZ = (int)(tran.forward.z * 1.4143f);
        return core.zells[cell.pt.x + gapX, cell.pt.z + gapZ];
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDetected(Vector3 pos, float sqrRadius)
    {
        return VectorEx.SqrMagnitudeXZ(pos, tran.localPosition) < sqrRadius;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector3 RandForward()
    {
        Vector3 right = tran.forward.CrossProductOnXZ();
        float f = Random.Range(-0.74f, 0.74f);
        return (tran.forward+ right * f)*0.17f;// * 0.4f;
    }
}
