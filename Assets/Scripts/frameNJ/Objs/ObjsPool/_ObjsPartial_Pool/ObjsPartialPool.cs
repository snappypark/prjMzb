using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

namespace nj
{
    public abstract class ObjsPartialPool<T, U> : ObjsPool<T, U> where T : MonoBehaviour where U : pqObj
    {
        public short NumObj
        {
            get
            {
                short numObj = 0;
                for (byte t = 0; t < NumType; ++t)
                    numObj += _pool[t].Num;
                return numObj;
            }
        }

        protected PartialPool<U>[] _pool = null;

        protected override void _awake()
        {
            base._awake();

            short cntCdx_atBegin = 0;
            _pool = new PartialPool<U>[NumType];
            for (byte t = 0; t < NumType; ++t)
            {
                short capacity = getCapacityOfType(t);
                _pool[t] = new PartialPool<U>(cntCdx_atBegin, capacity, _cObj);
                cntCdx_atBegin += capacity;
            }
        }

        protected void _Move(U obj)
        {
            _pool[obj.type].Move(obj);
        }

        protected U Reuse(byte objType, Vector3 pos)
        {
            return _pool[objType].Reuse(pos);
        }

        public void Unuse(U obj)
        {
            _pool[obj.type].Unuse(obj);
        }

        public void UnuseAllGamObj()
        {
            for (int objtype = 0; objtype < NumType; ++objtype)
                _pool[objtype].UnuseAll();
        }

        public enum qReturn
        {
            Next = 0,
            Next_Exit,
            Unuse_Exit,
        }

        public void Query_EnqueOrDequeByReturn(Pt centPt, Func<U, qReturn> callback)
        {
            for (byte qIdx = 0; qIdx < 9; ++qIdx)
            {
                Pt qPt = centPt + idx.PtNxN(qIdx);
                if (PartialPool<U>.IsOutOfIdx_OnPartial(qPt))
                    continue;

                for (byte type = 0; type < NumType; ++type)
                {
                    Queue<short> pq = _pool[type][qPt];

                    int numObj = pq.Count;
                    for (int i = 0; i < numObj; ++i)
                    {
                        U obj = _cObj[pq.Dequeue()];
                        switch (callback(obj))
                        {
                            case qReturn.Next_Exit:
                                pq.Enqueue(obj.cdx);
                                return;
                            case qReturn.Unuse_Exit:
                                Unuse(obj);
                                return;
                            case qReturn.Next:
                            default:
                                pq.Enqueue(obj.cdx);
                                break;
                        }
                    }
                }
            }
        }

        public void TotalPartialQuery(Func<U, bool> callback)
        {
            for (byte type = 0; type < NumType; ++type)
                for (int qi = 0; qi < PartialPool<U>._widthX; ++qi)
                    for (int qj = 0; qj < PartialPool<U>._widthZ; ++qj)
                    {
                        Queue<short> pq = _pool[type][qi, qj];

                        int numObj = pq.Count;
                        for (int i = 0; i < numObj; ++i)
                        {
                            U obj = _cObj[pq.Dequeue()];
                            if (callback(obj))
                                pq.Enqueue(obj.cdx);
                            else
                                Unuse(obj);
                        }
                    }
        }


        #region Find
        public U FindByPos(Vector3 pos)
        {
            U result = null;
            Query_EnqueOrDequeByReturn(new Pt((int)pos.x, (int)pos.y), delegate (U obj)
            {
                if (obj.transform.IsNearBy(pos, 0.3f))
                    result = obj;
                return qReturn.Next;
            });
            return result;
        }

        public U[] FindObjs_InPartials()
        {
            int num = NumObj;
            if (num == 0)
                return null;
            U[] result = new U[num];
            short adx = 0;
            TotalPartialQuery(delegate (U obj)
            {
                obj.adx = adx;
                result[adx++] = obj;
                return true;
            });
            return result;
        }

        public U[] FindObjs_InPartials(Action<U> callback)
        {
            int num = NumObj;
            if (num == 0)
                return null;
            U[] result = new U[num];
            short adx = 0;
            TotalPartialQuery(delegate (U obj)
            {
                obj.adx = adx;
                result[adx++] = obj;
                callback(obj);
                return true;
            });
            return result;
        }
        #endregion
    }
}