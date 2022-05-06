using System.Runtime.CompilerServices;
using UnityEngine;

public class effs : nj.ObjsQuePool<effs, eff>
{
    public const byte bomb = 0, effWall = 1, hitMelee = 2, hitBullet = 3, 
                      getKey = 4, trig = 5, spd = 6, fire = 7, elect = 8, firework = 9;
    short[] _numClones = new short[] { 24,24, 16, 16,  5, 3, 2, 24,  24,  2 };
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }

    float[] _times = new float[] { 4.8f, 1.0f, 0.5f, 0.4f,
                                    1.2f, 1.2f, pPlat.duration, 1.0f,  0.7f, 2.5f };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Play(byte type, Vector3 pos)
    {
        if (_pool[type].IsNotFull)
            Reactive(type, pos).Play(Time.time + _times[type]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Play(byte type, Vector3 pos, Color color)
    {
        if (_pool[type].IsNotFull)
            Reactive(type, pos).Play(Time.time + _times[type], color);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Play_HitBullet(Vector3 pos, f2 v)
    {
        if (_pool[hitBullet].IsFull)
            return;
        eff s = Reactive(hitBullet, pos);
        s.transform.forward = new Vector3(v.x, 0, v.z);
        s.Play(Time.time + 0.4f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Play(byte type, Transform tr)
    {
        if (_pool[type].IsNotFull)
            Reactive(type, tr.localPosition).Play(tr, Time.time + _times[type]);
    }
}
