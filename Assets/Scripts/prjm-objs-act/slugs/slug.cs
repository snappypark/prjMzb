using UnityEngine;

public class slug : nj.qObj
{
    protected short _oCdx = 19999;
    protected byte _ally;
    protected int _dmg;
    public virtual void OnActive(cel1l c) { }
    public virtual void OnActive(unit u) { }
    public virtual void OnActive(unit u, Vector3 nV, float speed, int dmg_) { }
    public virtual void OnActive(byte ally_, Vector3 nV, float speed, int dmg_){}
}
