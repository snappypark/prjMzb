using UnityEngine;
using UnityEngineEx;

namespace nj
{
    public class Objs<T, U> : MonoSingleton<T> where T : MonoBehaviour where U : Obj
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

        protected U CloneObj(byte objtype, Transform parent)
        {
            U obj = CreateGameObject.With<U>(objtype, _rootPreLoadObjs, parent);
            obj.type = objtype;
            obj.OnClone();
            return obj;
        }
    }
}