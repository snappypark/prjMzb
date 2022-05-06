using UnityEngine;

public class unitEntityMultiPlyor : unitEntity
{
    enum state { none, ctrl, hp0,}
    boltMob _m;
    state _state = state.none;

    public override void Init(unit o, unit.Type type, unit.SubType sub)
    {
        _o = o;
    }

    public override void OnSpawn()
    {
        ctrls.Inst.SpawnOnMulti(_o);
    }

    public override void OnHp0()
    {
        _state = state.hp0;
        _o.model.Ani.SetInteger(model.AniId_nAct, -1);
        ctrls.Inst.Release();
    }

    private void Update()
    {
        switch (_state)
        {
            case state.none:
                break;
            case state.ctrl:
                break;
            case state.hp0:
                break;
        }
    }
}
