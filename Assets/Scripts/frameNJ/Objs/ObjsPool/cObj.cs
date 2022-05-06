using UnityEngine;

namespace nj
{ 
public abstract class cObj : Obj
{
    [HideInInspector] public short cdx;

    public virtual void OnUnuse(eUnuse type_ = eUnuse.Default) { }

    public enum eUnuse
    {
        Default,
        AllClear,
    }
}
}