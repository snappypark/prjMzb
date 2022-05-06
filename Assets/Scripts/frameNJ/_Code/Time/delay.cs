using System.Runtime.CompilerServices;
using UnityEngine;

public struct delay
{
    bool _onceAfterEnd;
    public float duration;
    float _durationOver;
    float _end;
    
    public delay(float duration_, bool onceAfterEnd_ = true)
    {
        _onceAfterEnd = onceAfterEnd_;
        duration = duration_;
        _durationOver = 1 / duration;
        _end = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ResetWithTime(float curTime)
    {
        _onceAfterEnd = true;
        _end = curTime + duration;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _onceAfterEnd = true;
        _end = Time.time + duration;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset(float duration_)
    {
        duration = duration_;
        _durationOver = 1 / duration;
        _onceAfterEnd = true;
        _end = Time.time + duration;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ResetRandom(float waittime)
    {
        duration = waittime;
        _durationOver = 1 / duration;
        _onceAfterEnd = true;
        _end = Time.time + UnityEngine.Random.Range(0, duration);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int iRemain()  { return (int)(_end - Time.time); }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float fRemain() { return _end - Time.time; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEnd()
    {
        return Time.time > _end;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEnd(float gap = 0)
    {
        return Time.time > _end + gap;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEndAndReset()
    {
        return IsEndAndReset(Time.time);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEndAndReset(float curTime)
    {
        if (curTime > _end)
        {
            ResetWithTime(curTime);
            return true;
        }
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void End()
    {
        _onceAfterEnd = false;
        _end = Time.time- 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool InTime()
    {
        return Time.time < _end;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool InTime(float gap)
    {
        return Time.time < _end+ gap;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Ratio10()
    {
        return Mathf.Clamp01((_end - Time.time)* _durationOver);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float Ratio01()
    {
        return 1-Mathf.Clamp01((_end - Time.time) * _durationOver);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool afterOnceEnd()
    {
        if (_onceAfterEnd && Time.time > _end)
        {
            _onceAfterEnd = false;
            return true;
        }
        return false; 
    }
}
