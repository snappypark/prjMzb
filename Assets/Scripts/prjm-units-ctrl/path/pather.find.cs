using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngineEx;

public partial class pather
{
    public unit Target { get { return _followTarget; } }
    public float sqrDist { get { return _sqrDist; } }

    unit _o = null;
    unit _followTarget = null;
    cel1l _wanderCell = null;
    path_ _path = new path_();

    float _sqrDist = 999;
    float _sqrPathDist;
    float _sqrPathMax;
    float _speed;

    public void Init(unit o_, float sqrPathMax_)
    {
        _o = o_;
        _path.dir = o_.tran.forward;
        _sqrPathMax = sqrPathMax_;
    }

    public void SetSpeed(float speed_)
    {
        _speed = speed_;
    }

    public bool TryLookAtTarget(Transform tran, float spdDir = 0.24f)
    {
        if (null != _followTarget)
        {
            tran.forward = VectorEx.Lerp(tran.forward, _followTarget.tran.NorXzVecFrom(tran), spdDir);
            return true;
        }
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryFollowTarget()
    {
        _sqrDist = _sqrPathDist = _sqrPathMax;

        if (_followTarget != null)
        {
            if (!getPath(_followTarget))
                _followTarget = null;
        }
        else if (_o.attb.tdxs.Count > 0)
        {
            unit tryUnit = _o.attb.RollTryUnit;
            if (getPath(tryUnit))
                _followTarget = tryUnit;
        }
        return _path.Num > 0;
    }



    #region Get Path
    class path_
    {
        public Vector3 dir;

        public int Num { get { return _pts[curIdx].Num; } }
        public Vector3 Target { get { return _pts[curIdx].Peek; } }
        public Vector3 PeekOnNext { get { return _pts[nextIdx].Peek; } }

        protected nj.arr<Vector3>[] _pts = new nj.arr<Vector3>[2] { new nj.arr<Vector3>(6), new nj.arr<Vector3>(6) };
        protected byte curIdx = 0;
        protected byte nextIdx = 1;


        public void Add_OnNext(Vector3 pos) { _pts[nextIdx].Add(pos); }
        public void Reset_Add_OnNext(Vector3 pos) { _pts[nextIdx].Reset_Add(pos); }

        public void Add_OnCur(Vector3 pos) { _pts[curIdx].Add(pos); }
        public void Reset_Add_OnCur(Vector3 pos)
        {
            _pts[curIdx].Reset_Add(pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SwitchIdx(Vector3 pos)
        {
            _pts[curIdx].Reset();
            byte tmp = curIdx;
            curIdx = nextIdx;
            nextIdx = tmp;
            SetDirByPos(pos);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DequeueTarget(Vector3 pos)
        {
            --_pts[curIdx].Num;
            SetDirByPos(pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetDirByPos(Vector3 pos)
        {
            if (_pts[curIdx].Num == 0)
                return;
            dir = (_pts[curIdx].Peek - pos).normalized;
        }

        public void DrawLineOnDebug()
        {
            for (int i = 1; i < _pts[curIdx].Num; ++i)
            {
                Debug.DrawLine(
                    _pts[curIdx][i - 1] + Vector3.up * 0.1f,
                    _pts[curIdx][i] + Vector3.up * 0.1f);
            }

        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool getPath(unit u)
    {
        _sqrDist = f2.SqrMagnitude(new f2(_o.cell.pt.x, _o.cell.pt.z), new f2(u.cell.pt.x, u.cell.pt.z));
        if (_sqrDist > _sqrPathMax)
            return false;

        if (u.CanBeTraced(_o, _sqrPathMax))
        {
            if (calcPathGoal(u))
            {
                core.zones.clearQuq();
                return true;
            }
            core.zones.clearQuq();
        }
        return false;
    }

    bool calcPathGoal(unit u_)
    {
        if (_o.cell.zn.idx == u_.cell.zn.idx)
        {
            if (_o.cell.pt == u_.cell.pt)
            {
                _path.Reset_Add_OnCur(u_.tran.localPosition);
                _path.SetDirByPos(_o.tran.localPosition);
                _sqrPathDist = VectorEx.SqrMagnitudeXZ(_o.tran.localPosition, u_.tran.localPosition);
                return true;
            }
            else if (core.zones.calcPathGoal(_o.cell, u_.cell, _sqrPathDist))
            {
                _path.Reset_Add_OnNext(u_.tran.localPosition); // +(current.cell.ps - unit1.tran.localPosition).normalized * 0.2f
                if (makePath(u_.cell))
                    return _path.SwitchIdx(_o.tran.localPosition);
            }
        }
        else
        {
            i4 way;
            if (_o.cell.zn.GetClosestWay(u_.cell.pt, u_.cell.zn, out way))
            {
                cel1l exit1 = core.zells[way.X0, way.Z0];
                cel1l exit2 = core.zells[way.X1, way.Z1];

                if (_o.cell.pt == exit1.pt)
                {
                    _path.Reset_Add_OnCur(exit2.ct);
                    _path.SetDirByPos(_o.tran.localPosition);
                    _sqrPathDist = VectorEx.SqrMagnitudeXZ(_o.tran.localPosition, u_.tran.localPosition);
                    return true;
                }
                else if(core.zones.calcPathGoal(_o.cell, exit1, _sqrPathDist))
                {
                    _path.Reset_Add_OnNext(exit2.ct);
                    if (makePath(exit1))
                        return _path.SwitchIdx(_o.tran.localPosition);
                }

            }
        }
        return false;
    }

    bool makePath(cel1l cell, float newMinSqrDist = 0.0f)
    {
        PathNode to = cell.pNode;
        PathNode cur = to.from;
        PathNode from = cur.from;

        while (from != null)
        {
            if (checkObstacle_InBound(to.cell, from.cell))
            {
                newMinSqrDist += VectorEx.SqrMagnitudeXZ(cur.cell.ct, _path.PeekOnNext);
                if (newMinSqrDist > _sqrPathDist)
                    return false;
                _path.Add_OnNext(cur.cell.ct);
                to = cur;
            }
            cur = cur.from;
            from = cur.from;
        }

        if (checkObstacle_InBound2(core.zells[cel1l.Pt(_path.PeekOnNext)], _o.cell))
        {
            // Debug.Log("wrong");
            return false;
        }

        // if (current.cell.pt == _npc.cell.pt)
        //    Debug.Log("sdfsfsdf");

        //_path.Add_OnNext(current.cell.ct);

        newMinSqrDist += VectorEx.SqrMagnitudeXZ(_o.tran.localPosition, _path.PeekOnNext);
        if (newMinSqrDist > _sqrPathDist)
            return false;
        _sqrPathDist = newMinSqrDist;
        return true;
    }
    /*
    public static PointF Normalize(this PointF A)
    {
        float distance = Math.Sqrt(A.X * A.X + A.Y * A.Y);
        return new PointF(A.X / distance, A.Y / distance);
    }*/
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool checkObstacle_InBound(cel1l c0, cel1l c1)
    {
        int y = c0.pt.y;
        i2 max = new i2(Mathf.Max(c0.pt.x, c1.pt.x), Mathf.Max(c0.pt.z, c1.pt.z));
        i2 min = new i2(Mathf.Min(c0.pt.x, c1.pt.x), Mathf.Min(c0.pt.z, c1.pt.z));
        i2 gap = new i2(max.x - min.x, max.z - min.z);
        if (gap.x == 0 || gap.z == 0 || gap.x > 3 || gap.z > 3)
        {
            //Debug.Log("test");
            return true;
        }
        for (int x = min.x; x <= max.x; ++x)
            for (int z = min.z; z <= max.z; ++z)
                if (!core.zells[x, z].IsPath)
                    return true;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool checkObstacle_InBound2(cel1l c0, cel1l c1)
    {
        int y = c0.pt.y;
        i2 max = new i2(Mathf.Max(c0.pt.x, c1.pt.x), Mathf.Max(c0.pt.z, c1.pt.z));
        i2 min = new i2(Mathf.Min(c0.pt.x, c1.pt.x), Mathf.Min(c0.pt.z, c1.pt.z));
        i2 gap = new i2(max.x - min.x, max.z - min.z);

        for (int x = min.x; x <= max.x; ++x)
            for (int z = min.z; z <= max.z; ++z)
                if (!core.zells[x, z].IsPath)
                    return true;
        return false;
    }

    #endregion
}
