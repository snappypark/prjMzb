using UnityEngine;

namespace UnityEngineEx
{
    public static class GameObjectEx
    {
        public static void DestroyChildren(Transform rootTrans, int beginIdx = 0)
        {
            int numObj = rootTrans.childCount;
            for (int i = numObj-1; i > -1+ beginIdx; --i)
                GameObject.DestroyImmediate(rootTrans.GetChild(i).gameObject);
        }


    }
}

