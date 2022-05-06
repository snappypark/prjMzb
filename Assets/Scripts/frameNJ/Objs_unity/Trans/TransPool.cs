using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

namespace nj
{
    public class Trans<T> : MonoSingleton<T> where T : MonoBehaviour
    {
        [HideInInspector] public int NumType;
        Transform _rootPreLoadObjs;

        void Awake()
        {
            _awake();
        }

        protected virtual void _awake()
        {
            _rootPreLoadObjs = transform.GetChild(0);
            NumType = _rootPreLoadObjs.childCount;
        }

        protected Transform CloneObj(byte objtype, Transform parent)
        {
            Transform tran = CreateGameObject.With(objtype, _rootPreLoadObjs, parent);
            tran.gameObject.SetActive(true);
            tran.localPosition = VectorEx.Huge;
            return tran;
        }
    }
    
    public abstract class TransPool<T> : Trans<T> where T : MonoBehaviour
    {
        Transform _rtClones;

        public virtual short Capacity { get { return 0; } }
        public short Num { get { return (short)(_capacity - _waitCdxes.Count); } }
        
        Queue<Transform> _waitCdxes = new Queue<Transform>();
        Transform[] _trans = null;
        short _capacity = 0;

        protected override void _awake()
        {
            base._awake();
            _capacity = Capacity;
            for (short i = 0; i < _capacity; ++i)
            {
                _trans[i] = CloneObj(0, _rtClones);
                _waitCdxes.Enqueue(_trans[i]);
            }
        }

        public Transform Reuse(Vector3 pos)
        {
            if (_waitCdxes.Count == 0)
                return null;
            Transform tran = _waitCdxes.Dequeue();
            tran.localPosition = pos;
            return tran;
        }

        public void Unuse(Transform tran)
        {
            _waitCdxes.Enqueue(tran);
            tran.localPosition = VectorEx.Huge;
            tran.gameObject.SetActive(false);
        }

        public void UnuseAll()
        {
            _waitCdxes.Clear();
            for (short i = 0; i < _capacity; ++i)
                Unuse(_trans[i]);
        }
    }
}