using UnityEngine;

public class unitEntityNpc : unitEntityAbsNpc
{
    npc _npc;

    const float _speedRun = 3.4f;//2.8f;
    state _startRun;

    public override void Init(unit o, unit.Type type, unit.SubType sub)
    {
        _o = o;
        _npc = o as npc;
        _startRun = (state)UnityEngine.Random.Range(3, 5);
        _Hp0RandomIdx = UnityEngine.Random.Range(4, 6);

        int gap = sub - unit.SubType.ZombieLv1;
        _pather.Init(_o, npc.maxPathSqr170 + gap * 20);
        _o.attb.hp.ResetMax((int)( 14 + (_npc.tran.localScale.y-1.0f)*99.0f ) );
        _o.model.Ani.speed = 0.8f + gap * 0.13f;

        _speedAttackScale = _speedAttack / _o.model.Ani.speed; //Debug.Log(_speedAttackScale);
        _o.wp.dlyMelee.Reset(_speedAttackScale);// = new delay(1.888666666f);
    }


    public override void OnSpawn()
    {
        _o.attb.hp.ResetMax();
        _o.hud.SetPos(_o.tran.localPosition, unit.hud_.Type.OnlyMiniMap);
        _o.SetCell();
        _o.gameObject.SetActive(true);
        setStateIdle();
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
        _o.hud.ActiveMini_BySqrDist(_pather.sqrDist);
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
            case state.LookAt:
                break;
            case state.Die:
                return false;
        }
        checkOutZone();
        return true;
    }

    protected const float _speedAttack = 1.18f;
    protected float _speedAttackScale = 1.0f;
    void Update()
    {
        switch (_state)
        {
            case state.Idle:
                break;
            case state.RunSmoothly:
                if (_pather.TryMoveOnPath_withAlignment(_o.tran))
                    _state = state.RunSmoothly2;
                else
                    setStateIdle();
                break;
            case state.RunSmoothly2:
                if (_o.TryMeleeAtack(_pather.Target, _speedAttackScale))
                {
                    abjs.slugs.HitByUnit(_o);
                    setState(state.Melee, aniMelee, _speedAttack);
                }
                else if (_pather.TryMoveOnPath(_o.tran))
                    _state = state.RunSmoothly;
                else
                    setStateIdle();
                break;

            case state.RunRoughly:
                if (_pather.TryMoveOnPath2(_o.tran))
                    _state = state.RunRoughly2;
                else
                    setStateIdle();
                break;
            case state.RunRoughly2:
                _pather.TryAlignments(_o.tran);
                _state = state.RunRoughly;
                break;
            case state.Melee:
                _pather.TryLookAtTarget(_o.tran);
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
        _state = state.Idle; 
        _o.model.Ani.SetInteger(model.AniId_State, aniIdle);
    }

    void setState(state state, int aniState, float moveSpeed)
    {
        _pather.SetSpeed(moveSpeed * _o.model.Ani.speed);
        _state = state;
        _o.model.Ani.SetInteger(model.AniId_State, aniState);
    }

    void checkOutZone()
    {
        if (ctrls.Unit.cell.zn.idx > _o.cell.zn.idx + 1)
            OnHp0();
    }
}
