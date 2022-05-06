using UnityEngine;

public class unitEntityMultiBos : unitEntityAbsNpc
{
    boltBos _m = null;

    public override void Init(unit o, unit.Type type, unit.SubType sub)
    {
        _o = o;
        _m = _o.tran.GetComponent<boltBos>();
        _pather.Init(_o, npc.maxPathSqr190);

        _Hp0RandomIdx = UnityEngine.Random.Range(4, 6);
        _o.hud.ActiveMini(true);
        _o.hud.ring.enabled = false;
    }

    public override void OnSpawn()
    {
        setStateIdle();
        _o.attb.hp.ResetMax();
        _o.hud.SetPos(_o.tran.localPosition, unit.hud_.Type.OnlyMiniMap);
        _o.SetCell();
    }
    
    const float _speedRun = 3.4f;//2.8f;
    const float _sqrDistStandard = 111;//95.0f;
    public override bool Command_1by1()
    {
        switch (_state)
        {
            case state.Idle:
                if (_pather.TryFollowTarget())
                    setState(state.RunRoughly, aniRun, _speedRun);
                break;
            case state.RunSmoothly:
            case state.RunRoughly:
                if (_pather.TryFollowTarget())
                {
                    if (_pather.sqrDist < _sqrDistStandard)
                        setState(state.RunSmoothly, aniRun, _speedRun);
                    else
                        setState(state.RunRoughly, aniRun, _speedRun);
                }
                break;
        }
        return true;
    }

    protected const float _speedAttack = 1.18f;
    protected float _speedAttackScale = 1.0f;
    public override void OnUpdate()
    {
        switch (_state)
        {
            case state.Idle:
                break;
            case state.RunSmoothly:
                if (_o.TryMeleeAtack(_pather.Target, _speedAttackScale))
                {
                    _m.state.OnUseEquip();
                    setState(state.Melee, aniMelee, _speedAttack);
                }
                else if (_pather.TryMoveOnPath(_m.transform, 0.6f))
                    _state = state.RunSmoothly;
                else
                    setStateIdle();
                break;
            case state.RunRoughly:
                if (_pather.TryMoveOnPath2(_m.transform, 0.6f))
                    _pather.TryAlignments(_m.transform, 0.6f);
                else
                    setStateIdle();
                break;
            case state.Melee:
                _pather.TryLookAtTarget(_m.transform, 0.5f);
                if (_o.wp.dlyMelee.IsEnd())
                    setStateIdle();
                break;
            case state.LookAt:
                break;
            case state.Die:
                break;
        }

        switch (_o.cell.type)
        {
            case cel1l.Type.Trap_Fire:
                pTrap.OnFireTrap(_o);
                break;
            case cel1l.Type.Trap_FireWall:
                pTrap.OnFireWallTrap(_o);
                break;
        }
    }

    void setStateIdle()
    {
        _state = state.Idle; _o.model.Ani.SetInteger(model.AniId_State, aniIdle);
        _m.state.ani = 0;
    }

    void setState(state state, int aniState, float moveSpeed)
    {
        _m.state.ani = aniState;
        _pather.SetSpeed(moveSpeed * _o.model.Ani.speed);
        _state = state;
    }
}
