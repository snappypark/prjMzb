using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ctrls
{
    public static bool isMoved;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector3 GetNextPos(float deltaFrame)
    {
        return _o.tran.localPosition + _rigid.velocity * deltaFrame;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnCommandForUnit(float deltaFrame)
    {
        if (hasMove)
            _rigid.AddForce(iputNDir * _o.attb.moveSpd);
        _o.tran.forward = iputNLook;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddForce(Vector3 dir, float spd)
    {
        _rigid.AddForce(dir * spd);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnCollisionWithUnit(Vector3 pos)
    {
        if (unitClones.Detect(pos, _o.cdx))
            _rigid.velocity *= 0.7175421f;
    }

    public bool IsMoving()
    {
        return _rigid.velocity.sqrMagnitude > 0.52f;
    }
}
