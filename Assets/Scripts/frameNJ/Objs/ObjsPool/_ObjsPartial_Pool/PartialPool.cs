using System.Collections.Generic;
using System.Collections.GenericEx;
using UnityEngine;
using UnityEngineEx;

namespace nj
{
    public class PartialPool<U> where U : pqObj
    {
        public const int _widthX = 10;
        public const int _widthZ = 10;

        public short Capacity { get { return _capacity; } }
        public short Num { get { return (short)(_capacity - _waitCdxes.Count); } }
        public int Remain { get { return _waitCdxes.Count; } }

        short _capacity = 0;
        short _cdx_begin = 0;

        Queue<short> _waitCdxes = new Queue<short>();

        U[] _objs = null;

        Queue<short>[,] _q = null;
        public Queue<short> this[Pt pt] { get { return _q[pt.x, pt.y]; } }
        public Queue<short> this[int idxX, int idxY] { get { return _q[idxX, idxY]; } }

        public PartialPool(short cdx_begin, short capacity, U[] objs)
        {
            _cdx_begin = cdx_begin;
            _objs = objs;
            _capacity = capacity;

            for (short i = 0; i < _capacity; ++i)
                _waitCdxes.Enqueue((short)(_cdx_begin + i));

            _q = new Queue<short>[_widthX, _widthZ];
            for (int x = 0; x < _widthX; ++x)
                for (int y = 0; y < _widthZ; ++y)
                    _q[x, y] = new Queue<short>();
        }

        public void Move(U obj)
        {
            if (IsOutOfPos_OnPartial(obj.transform.localPosition))
            {
                if (obj.IsInPartial &&
                    this[obj.pdx].DequeueOne(obj.cdx))
                    obj.pdx = Pt.Huge;
                obj.IsInPartial = false;
            }
            else
            {
                Pt idxPt = new Pt((int)obj.transform.localPosition.x, (int)obj.transform.localPosition.y);
                if (obj.pdx != idxPt)
                {
                    if (obj.IsInPartial)
                        this[obj.pdx].DequeueOne(obj.cdx);
                    else
                        obj.IsInPartial = true;
                    obj.pdx = idxPt;
                    this[obj.pdx].Enqueue(obj.cdx);
                }
            }
        }

        public U Reuse(Vector3 pos)
        {
            if (_waitCdxes.Count == 0)
                return null;

            U obj = _objs[_waitCdxes.Dequeue()];
            obj.transform.localPosition = pos;
            obj.pdx = Pt.Huge;
            obj.gameObject.SetActive(true);

            Move(obj);

            return obj;
        }

        public void Unuse(U obj)
        {
            if (obj.IsInPartial)
                this[obj.pdx].DequeueOne(obj.cdx);
            _waitCdxes.Enqueue(obj.cdx);
            obj.OnUnuse();
            obj.transform.localPosition = VectorEx.Huge;
            obj.pdx = Pt.Huge;
            obj.IsInPartial = false;
            obj.gameObject.SetActive(false);
        }

        public void UnuseAll()
        {
            for (int i = 0; i < _widthX; ++i)
                for (int j = 0; j < _widthZ; ++j)
                    this[i, j].Clear();
            _waitCdxes.Clear();
            for (short i = 0; i < _capacity; ++i)
            {
                short cdx = (short)(_cdx_begin + i);
                _waitCdxes.Enqueue(cdx);

                U obj = _objs[cdx];
                obj.OnUnuse(cObj.eUnuse.AllClear);
                obj.transform.localPosition = VectorEx.Huge;
                obj.pdx = Pt.Huge;
                obj.IsInPartial = false;
                obj.gameObject.SetActive(false);
            }
        }



        public static bool IsOutOfPos_OnPartial(Vector3 pos)
        {
            return pos.x < 0.0f
                || pos.x > _widthX - 0.1f
                || pos.y < 0.0f
                || pos.y > _widthZ - 0.1f;
        }

        public static bool IsOutOfIdx_OnPartial(Pt pt)
        {
            return pt.x < 0 || pt.x >= _widthX
                || pt.y < 0 || pt.y >= _widthZ;
        }

        public static bool IsInPartial(Pt pt)
        {
            return pt.x >= 0 && pt.x < _widthX
                && pt.y >= 0 && pt.y < _widthZ;
        }

    }
}