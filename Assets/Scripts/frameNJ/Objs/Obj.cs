using UnityEngine;

namespace nj
{
    public class Obj : MonoBehaviour
    {
        [HideInInspector] public byte type;
        [HideInInspector] public short adx;

        public virtual void OnClone() { }
    }

}