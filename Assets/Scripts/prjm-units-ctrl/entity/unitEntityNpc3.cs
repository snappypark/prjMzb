using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitEntityNpc3 : unitEntity
{
    const int aniIdle = 0, aniWalk = 1, aniRun = 2, aniMelee = 3, aniDie = 4;
    enum state { Idle = 0, Melee, Die,
        RunSmoothly, RunSmoothly2, RunSmoothly3, RunRoughly, RunRoughly2,
        LookAt,
    }

    state _state = state.Idle;

    npc _npc;
    pather _pather = new pather();

    const float _speedRun = 3.4f;//2.8f;
    int _Hp0RandomIdx;
    state _startRun;

    void setStateIdle()
    {
        _state = state.Idle; _o.model.Ani.SetInteger(model.AniId_State, aniIdle);
    }

    void setState(state state, int aniState, float moveSpeed)
    {
        _pather.SetSpeed(moveSpeed * _o.model.Ani.speed);
        _state = state;
        _o.model.Ani.SetInteger(model.AniId_State, aniState);
    }

    public override void Init(unit o, unit.Type type, unit.SubType sub)
    {
        _o = o;
        _npc = o as npc;
        _startRun = (state)UnityEngine.Random.Range(3, 5);
        _Hp0RandomIdx = UnityEngine.Random.Range(4, 6);

        int gap = sub - unit.SubType.ZombieLv1;
        _pather.Init(_o, npc.maxPathSqr170 + gap * 20);
        _o.attb.hp.ResetMax(200);
        _o.model.Ani.speed = 0.8f + gap * 0.13f;
        
    }

    public override void OnSpawn()
    {
        setStateIdle();
        _o.attb.hp.ResetMax();
        _o.hud.SetPos(_o.tran.localPosition, unit.hud_.Type.OnlyMiniMap);
        _o.SetCell();
        _o.gameObject.SetActive(true);
    }

    public override void OnHp0()
    {
        _state = state.Die;
        _o.model.Ani.SetInteger(model.AniId_State, _Hp0RandomIdx);
        _o.OutCell();
        _o.hud.Set(unit.hud_.Type.None);
        enabled = false;
    }



    const float _sqrDistStandard = 111;//95.0f;
    public override bool Command_1by1()
    {
        switch (_state)
        {
            case state.Idle:
                if (_pather.TryFollowTarget())
                {
                    _o.hud.ring.enabled = false;
                    if (_pather.sqrDist < _sqrDistStandard)
                        setState(_startRun, aniRun, _speedRun);
                    else
                        setState(state.RunRoughly, aniRun, _speedRun);
                }
                break;
            case state.RunSmoothly:
            case state.RunSmoothly2:
            case state.RunSmoothly3:
            case state.RunRoughly:
            case state.RunRoughly2:
                if (_pather.TryFollowTarget())
                {
                    if (_pather.sqrDist < _sqrDistStandard)
                        setState(_startRun, aniRun, _speedRun);
                    else
                        setState(state.RunRoughly, aniRun, _speedRun);
                }
                break;
            case state.Die:
                return false;
        }
        checkOutZone();
        return true;
    }

    void checkOutZone()
    {
        if (ctrls.Unit.cell.zn.idx > _o.cell.zn.idx + 1)
            OnHp0();
    }

}
