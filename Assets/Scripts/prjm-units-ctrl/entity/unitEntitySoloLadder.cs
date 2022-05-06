using UnityEngine;

public class unitEntitySoloLadder : unitEntity
{
    enum state { none, walk, up }
    state _state = state.none;

    public override void Init(unit o, unit.Type type, unit.SubType sub) 
    {
         _o = o;

         if(_o.cell.zprp == null)
         {
            setStageEndUI();
            return;
         }
        
        zprp zprp = o.cell.zprp as zprp;

        cel1l c = core.zells[zprp.bd.X0+1,  zprp.bd.Z1];
        pos0 = o.tran.localPosition;
        pos1 = c.ct;
        pos2 = c.ct + new Vector3(0,2.2f,0);

        _delay.Reset(1.3f);
        _state = state.walk;

        _delay2.Reset(1.2f);
        offset = tranCam.OffsetPlay;
        offsetX = Random.Range(-1.0f,1.0f);

        uis.ingam.Active(ui_ingam.State.none);
    }

    Vector3 pos0, pos1, pos2;
    delay _delay = new delay(1.2f);
    void Update()
    {
        switch(_state)
        {
            case state.walk:
            _o.tran.localPosition = Vector3.Lerp(pos0, pos1, _delay.Ratio01());
            _o.tran.forward = Vector3.Lerp(_o.tran.forward, Vector3.forward, 0.1f);
            if(_delay.IsEnd())
            {
                _state = state.up;
                _delay.Reset(1.4f);
            }
            break;

            case state.up:
            _o.tran.localPosition = Vector3.Lerp(pos1, pos2, _delay.Ratio01());
            if(_delay.IsEnd())
            {
                _state = state.none;
                ads.Inst.Check_ForNextStage(dStage.PlayingIdx);
                setStageEndUI();
                _o.gameObject.SetActive(false);
            }
            break;
        }
    }

    delay _delay2 = new delay(3);
    Vector3 offset;
    float offsetX = 1.0f;
    void FixedUpdate()
    {
        if(_delay2.InTime())
            offset = Vector3.Lerp(tranCam.OffsetPlay, new Vector3(offsetX, 9.0f, -6.0f), _delay2.Ratio01());

        cams.Inst.mainTran.SetTarget(_o.tran, offset);
        cams.Inst.mainTran.OnFixedUpdate(_o.tran);

        
    }

    void setStageEndUI()
    {
        gjs.effs.Play(effs.firework, _o.tran.localPosition + Vector3.up);
        audios.Inst.PlaySound(audios.eSoundType.victory);
        uis.pops.result.endSolo.Active(dStage.PlayingIdx,
            dStage.PlayingIdx == dStage.LastIdx ? popEndSolo.State.LastStage : popEndSolo.State.NextGame, 
            langs.GameClear());
    }

}
