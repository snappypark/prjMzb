using System.Collections;
using UnityEngine;

namespace UnityEngineEx
{
    public static class MonoBehaviourEx
    {
        public static IEnumerator Active_(this MonoBehaviour monoBevahiour)
        {
            monoBevahiour.gameObject.SetActive(true);
            yield return null;
        }

        public static IEnumerator Inactive_(this MonoBehaviour monoBevahiour)
        {
            monoBevahiour.gameObject.SetActive(false);
            yield return null;
        }
    }
}
