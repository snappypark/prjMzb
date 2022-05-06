using System.Runtime.CompilerServices;
using UnityEngine;

public struct tt
{
    int _cnt;
    float _end;

    public tt(int cnt_ = 1)
    {
        _cnt = cnt_;
        _end = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CountAndCheck()
    {
        if (_cnt > 0) {
            _cnt--;
            return true;
        }
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetCnt()
    {
        return _cnt;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetCnt(int cnt_)
    {
        _cnt = cnt_;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEnd()
    {
        return Time.time > _end;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetEnd(float duration_)
    {
        _end = Time.time + duration_;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float fRemain() { return _end - Time.time; }

}
