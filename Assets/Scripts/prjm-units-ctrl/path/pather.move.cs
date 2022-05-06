using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngineEx;

public partial class pather
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryMoveOnPath_withAlignment(Transform tran, float spdDir = 0.25f)
    {
        if (_path.Num == 0)
            return false;
        Vector3 v = _path.Target - tran.localPosition;
        Vector3 toward = _path.dir * _speed * Time.smoothDeltaTime;
        bool isTarget = v.sqrMagnitude < toward.sqrMagnitude;

        Vector3 newPos = isTarget ? _path.Target : tran.localPosition + toward;

        Vector3 dir2;
        if (unitClones.Alignments(_o, newPos, _path.dir, out dir2))
        {
            newPos = tran.localPosition + dir2 * _speed * Time.smoothDeltaTime;
            newPos = cel1l.Clamp2(tran.localPosition, newPos, dist.sqr0_48, 0.48f);
            _path.SetDirByPos(newPos);
        }
        _o.SetPosDir(newPos, VectorEx.Lerp(tran.forward, _path.dir, spdDir));
        _o.SetCell2();

        if (isTarget) // sqr0_27 sqr0_4
            _path.DequeueTarget(tran.localPosition);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryMoveOnPath(Transform tran, float spdDir = 0.25f)
    {
        if (_path.Num == 0)
            return false;
        Vector3 v = _path.Target - tran.localPosition;
        Vector3 toward = _path.dir * _speed * Time.smoothDeltaTime;
        bool isTarget = v.sqrMagnitude < toward.sqrMagnitude;
        Vector3 newPos = isTarget ? _path.Target : tran.localPosition + toward;

        _o.SetPosDir(newPos, VectorEx.Lerp(tran.forward, _path.dir, spdDir));
        _o.SetCell2();
        _o.hud.SetPos(tran.localPosition);

        if (isTarget) // sqr0_27 sqr0_4
            _path.DequeueTarget(tran.localPosition);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void TryAlignments(Transform tran, float spdDir = 0.25f)
    {
        Vector3 dir2;
        if (unitClones.Alignments(_o, tran.localPosition, _path.dir, out dir2))
        {
            Vector3 newPos = tran.localPosition + dir2 * _speed * Time.smoothDeltaTime;
            newPos = cel1l.Clamp2(tran.localPosition, newPos, dist.sqr0_48, 0.48f);
            _path.SetDirByPos(newPos);

            _o.SetPosDir(newPos, VectorEx.Lerp(tran.forward, _path.dir, spdDir));
            _o.SetCell2();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryMoveOnPath2(Transform tran, float spdDir = 0.25f)
    {
        if (_path.Num == 0)
            return false;
        Vector3 v = _path.Target - tran.localPosition;
        Vector3 toward = _path.dir * _speed * 2 * Time.smoothDeltaTime;
        bool isTarget = v.sqrMagnitude < toward.sqrMagnitude;
        Vector3 newPos = isTarget ? _path.Target : tran.localPosition + toward;

        _o.SetPosDir(newPos, VectorEx.Lerp(tran.forward, _path.dir, spdDir));
        _o.SetCell2();
        _o.hud.SetPos(tran.localPosition);

        if (isTarget) // sqr0_27 sqr0_4
            _path.DequeueTarget(tran.localPosition);
        return true;
    }

}
