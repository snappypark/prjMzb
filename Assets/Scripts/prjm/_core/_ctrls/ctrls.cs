using System.Runtime.CompilerServices;
using UnityEngine;

public partial class ctrls : MonoBehaviour
{
    public enum State
    {
        None=0, Menu=1, Edit=2, Stage=4, Multi_Escape=8, Multi_Battle=16,
    }

    public static ctrls Inst;
    public static unit Unit { get { return Inst._o; } }

    public bool IsNone { get { return _state == State.None; } }
    State _state = State.None;

    unit _o;
    Rigidbody _rigid;

    void Awake()
    {
        Inst = this;
        Inst._o = unit0.inst;
    }

    public void SetState(State state_, unit o_)
    {
        setInputNone();
        _state = state_;
        _o = o_;
        if (o_.tran != null)
        {
            Inst._rigid = o_.tran.GetComponent<Rigidbody>();
            Inst._rigid.velocity = Vector3.zero;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnFixedUpdate()
    {
        //if (_o == null)
       //     return;
        switch (_state)
        {
            case State.Menu:
                setInputNone();
                core.stages.Update_OnMenu(_o);
                cams.Inst.mainTran.OnFixedUpdate(_o.tran);
                break;
            case State.Edit:
                SetInputValue(_o.model.tranHand.position, _o.tran.forward);
                core.stages.OnUpdate_OnEdit(_o);
                cams.Inst.mainTran.OnFixedUpdate(_o.tran);
                break;
            case State.Stage:
                SetInputValue(_o.model.tranHand.position, _o.tran.forward);
                onFixedUpdate_Play();
                cams.Inst.mainTran.OnFixedUpdate(_o.tran);
                break;
            case State.Multi_Escape:
                onFixedUpdate_Play();
                cams.Inst.mainTran.OnFixedUpdate(_o.tran);
                break;
            case State.Multi_Battle:
                onFixedUpdate_Play();
                cams.Inst.mainTran.OnFixedUpdate(_o.tran);
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void onFixedUpdate_Play()
    {
        core.zones.OnUpdate_NextZone(_o.cell.zn);
        _o.cell.zn.onUpdate_Jsp();

        switch (_o.cell.type)
        {
            case cel1l.Type.TrigCircleBomb:
                zTrigger.OnTrig(_o);
                break;

            case cel1l.Type.Key1: case cel1l.Type.Key2: case cel1l.Type.Key3:
            case cel1l.Type.Key4: case cel1l.Type.Key5: case cel1l.Type.Key6:
                zKey.OnKey(_o.cell.zn, _o.cell.type);
                break;

            case cel1l.Type.Ammo:
                zAmmo.OnAmmo(_o); break;
            case cel1l.Type.Bomb:
                zBomb.OnBomb(_o); break;

            case cel1l.Type.AreaWin:
                OnEvent(evWin); break;

            case cel1l.Type.Trap_Slow:
                pTrap.OnSlowTrap(_o.cell, _rigid);
                break;
            case cel1l.Type.Trap_Fire:
                pTrap.OnFireTrap(_o);
                break;
            case cel1l.Type.Trap_FireWall:
                pTrap.OnFireWallTrap(_o);
                break;
                

            case cel1l.Type.PlatX:
                pPlat.OnPlatMove(_o, msgType.SpdX);
                break;
            case cel1l.Type.Plat_X:
                pPlat.OnPlatMove(_o, msgType.Spd_X);
                break;
            case cel1l.Type.PlatZ:
                pPlat.OnPlatMove(_o, msgType.SpdZ);
                break;
            case cel1l.Type.Plat_Z:
                pPlat.OnPlatMove(_o, msgType.Spd_Z);
                break;

            case cel1l.Type.PlatSpd:
                pPlat.OnEvent_Spd(_o);
                break;

            default:
                break;
        }

        gjs.ctrlHuds.transform.localPosition = _o.tran.localPosition;
    }
}
