using UnityEngine;

public partial class ctrls
{
    public const int evHp0 = 2048, evTimeOut = 4096, evWin = 8192;

    public void OnEvent(int eventType)
    {
        int eventOnState = eventType + (int)_state;
        switch (eventOnState)
        {
            case evMultiMaze_Hp0:
            case evMultiTeam_Hp0:
                break;

            case evMultiEscape_Victory:
                gjs.effs.Play(effs.firework, _o.tran.localPosition + Vector3.up * 5);
                core.multis.OnEven_Escape_Victory();
                Release();
                break;

            case evenVictory_Stage:
                onEvent_StageCommonResult(unit.Type.SoloLadder);
                break;
            case evenHp0_Stage:
                onEvent_StageCommonResult(unit.Type.None);
                onEvent_StageResult_Defeated(false);
                break;
            case evenTimeOut_Stage:
                onEvent_StageCommonResult(unit.Type.None);
                onEvent_StageResult_Defeated(true);
                break;
        }
    }

    const int evMultiMaze_Hp0 = (int)State.Multi_Escape + evHp0;
    const int evMultiEscape_Victory = (int)State.Multi_Escape + evWin;

    const int evMultiTeam_Hp0 = (int)State.Multi_Battle + evHp0;


    const int evenVictory_Stage = (int)State.Stage + evWin;
    const int evenHp0_Stage = (int)State.Stage + evHp0;
    const int evenTimeOut_Stage = (int)State.Stage + evTimeOut;

    void onEvent_StageCommonResult(unit.Type unitType)
    {
        _o.hud.Set(unit.hud_.Type.None);
        _o.Init(unitType);
        Release();
        uis.ingam.play.lbCountdownRemain.Set(lbCountdownRemain.State.None);
    }
    
    void onEvent_StageResult_Defeated(bool isTimout)
    {
        audios.Inst.StopMusic();
        audios.Inst.PlaySound(audios.eSoundType.lose);
        uis.pops.result.endSolo.Active(dStage.PlayingIdx, popEndSolo.State.RetryGame, isTimout ? langs.TimeOut() : langs.GameOver());
    }
}
