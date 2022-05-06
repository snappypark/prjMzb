using System.Collections;
using UnityEngine;

using UdpKit.Platform.Photon;
using UdpKit.Platform;
using Bolt.Matchmaking;
using Bolt.Photon;


public partial class multis
{
    void onPlay_OnBattle()
    {
    }

    void setState_OnBattle(state state_)
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
                respawnBos_ByServer();
                _dlyNextState.Reset(playDur);
                uis.ingam.Active(ui_ingam.State.multi_ply);
                uis.ingam.play.lbCountdownRemain.Set(lbCountdownRemain.State.Multi, playDur);
                break;
            case state.EndCnt:
                _dlyNextState.Reset(endDur);
                uis.ingam.Active(ui_ingam.State.multi_center);
                break;
            case state.Result:
                lostCtrlOfBos();
                _dlyNextState.Reset(scoreDur);
                uis.pops.result.endMulti.Active(ref _plyors, _mode, playDur);
                break;
            case state.Load:
                core.Inst.flowMgr.Change<Flow_MultiSession>(() => { _dlyNextState.Reset(0.5f); });
                break;
            case state.Wait:
                uis.ingam.Active(ui_ingam.State.multi_wait);
                break;
        }
    }

    void onRoomState_OnBattle(changeState chgState)
    {
        switch (chgState)
        {
            case changeState.Play:
                boltPlyor.StartPlay(playDur + 1, false);
                _rmSeed = (int)_sss.Properties[k_seed];
                setState_OnBattle(state.PlayCnt);

                audios.Inst.SetNeedToPlayDefaultMusic();
                core.loads.SetBgsMusic();
                break;

            case changeState.End:
                ctrls.Inst.Release();
                setState_OnBattle(state.EndCnt);
               // if (_isEscaped)
               //     uis.It.ingam.multi.lbCenter.Active(lbCenter.Type.Victory);
               // else
                    uis.ingam.multi.lbCenter.Active(lbCenter.Type.GameEnd);
                break;
        }
    }
}
