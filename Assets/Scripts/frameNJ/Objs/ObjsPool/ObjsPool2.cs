using UnityEngine;
using UnityEngineEx;

namespace nj
{
    public class ObjsPool2<T, U> : MonoSingleton<T> where T : MonoBehaviour where U : Obj
    {
        protected virtual short Capacity { get { return 0; } }

        public U this[int cdx] { get { return _cObj[cdx]; } }
        protected U[] _cObj = null;

        public short Num { get { return _num; } }
        protected short _num = 0;
        protected short _beginCdx = 0;

        void Awake()
        {
            _awake();
        }

        protected virtual void _awake()
        {
            _cObj = new U[Capacity];
        }

        public U Reactive(byte cdx, Vector3 pos)
        {
            U obj = _cObj[cdx];
            obj.transform.localPosition = pos;
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Unactive(U obj)
        {
            obj.gameObject.SetActive(false);
        }

        public void Unactive(short cdx)
        {
            _cObj[cdx].gameObject.SetActive(false);
        }

        public void UnactiveAll()
        {
            for (short c = _beginCdx; c < _num; ++c)
                _cObj[c].gameObject.SetActive(false);
        }

        public virtual void Clear()
        {
            GameObjectEx.DestroyChildren(transform, _beginCdx);
            _num = _beginCdx;
        }
    }
}
