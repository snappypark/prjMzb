using System;
using System.Collections;

namespace nj
{ 
public abstract class Flow_Abs
{
    public virtual byte iType { get { return iTypeNone; } }
        
    public virtual IEnumerator OnEnter_() { yield return null; }
    public virtual IEnumerator OnExit_() { yield return null; }

    public const byte iTypeNone = 0;
    public Action callback;
}
}