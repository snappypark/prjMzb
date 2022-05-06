using UnityEngine;

public class unitEntity : MonoBehaviour
{
    [HideInInspector] protected unit _o;

    public virtual void Init(unit o, unit.Type type, unit.SubType sub) { _o = o; }

    public virtual void OnSpawn() { }
    public virtual void OnHp0() { }

    public virtual bool Command_1by1() { return true; }
    public virtual void OnUpdate() { }
}
