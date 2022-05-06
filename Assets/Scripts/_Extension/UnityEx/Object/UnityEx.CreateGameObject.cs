using UnityEngine;

namespace UnityEngineEx
{
    public static class CreateGameObject
    {
        public static GameObject With(string name, Transform parent)
        {
            GameObject go = new GameObject(name + "(Clone)");
            go.transform.SetParent(parent);
            return go;
        }

        public static Transform With(byte preload_idx, Transform preloads, Transform parent)
        {
            return GameObject.Instantiate(preloads.GetChild(preload_idx), parent, false);
        }

        public static T With<T>(string name, Transform parent) where T : Component
        {
            GameObject go = new GameObject(name + "(Clone)");
            go.transform.SetParent(parent);
            return go.AddComponent<T>();
        }
        
        public static T With<T>(byte preload_idx, Transform preloads, Transform parent) where T : Component
        {
            return GameObject.Instantiate<T>(preloads.GetChild<T>(preload_idx), parent, false);
            /*
            T t = preloads.GetChild<T>(preload_idx);
            T result = GameObject.Instantiate<T>(t, parent, false);
            result.transform.localPosition = t.transform.localPosition;
            result.transform.localRotation = t.transform.localRotation;
            return result;*/
        }

        public static T WithRes<T>(string resourcePath, Transform parent) where T : Component
        {
            GameObject gameObj = Resources.Load(resourcePath) as GameObject;
            return GameObject.Instantiate<T>(gameObj.GetComponent<T>(), parent, false);
        }

        public static Transform TranRes(string resourcePath, Transform parent)
        {
            GameObject gameObj = Resources.Load(resourcePath) as GameObject;
            return GameObject.Instantiate<Transform>(gameObj.GetComponent<Transform>(), parent, false);
        }
    }
}
