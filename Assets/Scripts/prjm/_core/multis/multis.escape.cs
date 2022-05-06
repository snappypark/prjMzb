using UnityEngine;
using Bolt.Matchmaking;
using Bolt.Photon;

public partial class multis
{
    public static int BestZdx = 0;
    bool _isEscaped = false;

    public void OnEven_Escape_Victory()
    {
        if (_chgState == changeState.End)
            return;
        _isEscaped = true;
        _chgState = changeState.End;
        onRoomState_OnEscape(changeState.End);
        BoltMatchmaking.UpdateSession(getRoomPrp_endRoom());
        boltPlyor.EndPlay(playDur - _dlyNextState.fRemain());
    }
    
    void onPlay_OnEscape()
    {
        if (ctrls.Inst.IsNone)
            return;
        boltPlyor.CheckZdx(playDur - _dlyNextState.fRemain());
    }

    void setState_OnEscape(state state_)
    {
        _state = state_;
        if (!BoltNetwork.IsRunning)
            return;
        switch (state_)
        {
            case state.PlayCnt:
                _dlyNextState.Reset((float)_sss.Properties[k_startTime] - BoltNetwork.ServerTime);
                uis.ingam.Active(ui_ingam.State.multi_center);
                uis.ingam.multi.lbCenter.Active(lbCenter.Type.CountDownToStart, _dlyNextState.duration);
                break;
            case state.Play:
                _dlyNextState.Reset(playDur);
                uis.ingam.Active(ui_ingam.State.multi_ply);
                uis.ingam.play.lbCountdownRemain.Set(lbCountdownRemain.State.Multi, playDur);
                break;
            case state.EndCnt:
                _dlyNextState.Reset(endDur);
                uis.ingam.Active(ui_ingam.State.multi_center);
                break;
            case state.Result:
                ctrls.Inst.Release();
                _dlyNextState.Reset(scoreDur);
                uis.pops.result.endMulti.Active(ref _plyors, _mode, playDur);
                break;
            case state.Load:
                core.Inst.flowMgr.Change<Flow_MultiSession>(()=> { _dlyNextState.Reset(0.5f);  });
                break;
            case state.Wait:
                uis.ingam.Active(ui_ingam.State.multi_wait);
                break;
        }
    }

    void onRoomState_OnEscape(changeState chgState)
    {
        switch (chgState)
        {
            case changeState.Play:
                _isEscaped = false;
                boltPlyor.StartPlay(playDur + 1);
                setState_OnEscape(state.PlayCnt);

                audios.Inst.SetNeedToPlayDefaultMusic();
                core.loads.SetBgsMusic();
                break;

            case changeState.End:
                _rmSeed = (int)_sss.Properties[k_seed];
                setState_OnEscape(state.EndCnt);
                if (_isEscaped)
                    uis.ingam.multi.lbCenter.Active(lbCenter.Type.Victory);
                else
                    uis.ingam.multi.lbCenter.Active(lbCenter.Type.GameEnd);

                audios.Inst.StopMusic();
                lostCtrlOfMobs();
                break;
        }
    }
}
