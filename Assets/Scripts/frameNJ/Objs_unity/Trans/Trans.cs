using UnityEngine;
using UnityEngineEx;

namespace nj
{
    public class Trans : MonoBehaviour
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

        public Transform CloneWithInactive(byte objtype, Transform parent)
        {
            Transform tran = CreateGameObject.With(objtype, _rootPreLoadObjs, parent);
            return tran;
        }

        public Transform Clone(byte objtype, Transform parent)
        {
            Transform tran = CreateGameObject.With(objtype, _rootPreLoadObjs, parent);
            tran.gameObject.SetActive(true);
            return tran;
        }

        public Transform Clone(byte objtype, Vector3 pos, Transform parent)
        {
            Transform tran = CreateGameObject.With(objtype, _rootPreLoadObjs, parent);
            tran.localPosition = pos;
            tran.gameObject.SetActive(true);
            return tran;
        }
    }
}