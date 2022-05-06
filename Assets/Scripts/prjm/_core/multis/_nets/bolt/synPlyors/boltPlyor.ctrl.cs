using Bolt;
using UnityEngine;

public partial class boltPlyor
{
    static boltPlyor Inst = null;
    public static void Chat(string str) {  if (Inst != null) Inst.state.chat = str; }
    public static void StartPlay(float endtime, bool needZdx = true)
    {
        if (Inst == null)
            return;
        Inst.state.endTime = endtime;
        Inst.state.score = 0;
        if(needZdx)
            Inst.state.zdx = 0;
        multis.BestZdx = 0;
    }

    public static void CheckZdx(float endtime)
    {
        if (Inst == null) return;
        if (Inst._o == null)/*---*/return;
        if (Inst.state.zdx < Inst._o.cell.zn.idx)
        {
            Inst.state.endTime = endtime;
            Inst.state.zdx = Inst._o.cell.zn.idx;
        }
    }

    public static void EndPlay(float endtime)
    {
        if (Inst == null)
            return;
        Inst.state.endTime = endtime;
    }

    public override void ControlGained()
    {
        Debug.Log("ControlGained");
        firstControll = true;
        Inst = this;
    }
    public override void ControlLost() {
        Inst = null;
    }


    bool firstControll = false;
    public override void SimulateController()
    {
        if (!multis.isLoadedMaps) return;
        if (_o == null) return;
        if (firstControll)
        {
            _o.Init(unit.Type.MultiPlyor);
            _o.entity.OnSpawn();
            _o.hud.ActiveMiniArea(false);
            firstControll = false;
        }
        if (ctrls.Inst.IsNone) return;
        IPlayerCommInput input = PlayerComm.Create();
        ctrls.Inst.SetInputValue(_o.model.tranHand.position, _o.tran.forward);
        getInput(ref input);
        //   if (entity.IsInputQueueFull)  entity.ClearInputQueue();
        entity.QueueInput(input);
        Debug.Log("SimulateController" + GetInstanceID());
    }

    public override void ExecuteCommand(Bolt.Command c, bool resetState)
    {
        if (ctrls.Inst.IsNone) return;
        if (_o == null) return;

        PlayerComm uc = c as PlayerComm;

        Debug.Log("ExecuteCommand" +  GetInstanceID());
        if (resetState)// we got a correction from the server, reset (this only runs on the client) Controller
        {
          //  /*
            if ((_o.tran.localPosition - uc.Result.position).sqrMagnitude > 0.25f)
            {
                _o.tran.localPosition = uc.Result.position;
                _o.SetCell();
                _o.hud.SetPos(_o.tran.localPosition);
            }//*/
        }
        else
        {  // apply both server and client)  Debug.Log(entity.IsOwner  + " ower sdfsdf " + entity.NetworkId);
            
            bool hasMove = uc.Input.IsMove;
            bool hasAct = uc.Input.IsAct;
            Vector3 iputNDir = new Vector3(uc.Input.dirX, 0, uc.Input.dirZ);
            Vector3 iputNLook = new Vector3(uc.Input.lookX, 0, uc.Input.lookZ);
            _o.MoveAndLook(hasMove, _rigid, iputNDir, iputNLook);
            uc.Result.position = _o.GetNextPos(_rigid, BoltNetwork.FrameDeltaTime);
        
            //state.tran = transform;

            if (c.IsFirstExecution)
            {
                if (state.isMoved != uc.Input.IsMove)
                    state.isMoved = uc.Input.IsMove;

                if (state.wp != uc.Input.weapon)
                    state.wp = uc.Input.weapon;

                if (ctrls.hasAct && _o.CheckEquip((model.Equip)state.wp, BoltNetwork.ServerTime))
                    state.OnUseEquip();
                else
                    _o.model.Ani.SetInteger(model.AniId_nAct, 0);

            }
        }
    }

    public override void MissingCommand(Command previous)
    {
        if (previous == null) { return; }
        //  ExecuteCommand(previous, true);
    }

    static void setInput(IPlayerCommInput input)
    {
        ctrls.hasMove = input.IsMove;
        ctrls.hasAct = input.IsAct;
        ctrls.iputNDir = new Vector3(input.dirX, 0, input.dirZ);
        ctrls.iputNLook = new Vector3(input.lookX, 0, input.lookZ);
    }

    static void getInput(ref IPlayerCommInput input)
    {
        input.dirX = ctrls.iputNDir.x;
        input.dirZ = ctrls.iputNDir.z;
        input.lookX = ctrls.iputNLook.x;
        input.lookZ = ctrls.iputNLook.z;
        input.IsAct = ctrls.hasAct;
        input.IsMove = ctrls.hasMove;
        input.weapon = (int)ctrls.Unit.wp.state;
    }
}
