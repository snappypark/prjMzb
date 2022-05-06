using UnityEngine;

public class unitEntitySolo : unitEntity
{
    Rigidbody _rigid;

    public override void Init(unit o, unit.Type type, unit.SubType sub) 
    {
         _o = o;
         _rigid = o.tran.GetComponent<Rigidbody>();
         _rigid.velocity = Vector3.zero;
    }

    public override void OnHp0()
    {
        enabled = false;
        _o.model.Ani.SetInteger(model.AniId_nAct, -1);
        ctrls.Inst.OnEvent(ctrls.evHp0);

        ads.Inst.ShowInterstitial_byHp0Count();
    }

    int _znIdx = -1;
    delay _delaySound0 = new delay(0.253f);
    void Update()
    {
        Vector3 nextPos = _o.GetNextPos(_rigid, Time.fixedDeltaTime);
        bool isMoving = ctrls.Inst.IsMoving();

        _o.hud.SetPos(nextPos);
        _o.SetCell();

        _o.model.Ani.SetBool(model.AniId_bRun, isMoving);

        ctrls.Inst.OnCollisionWithUnit(nextPos);

        if (isMoving && _delaySound0.IsEndAndReset())
            audios.Inst.step.PlaySound();

        if(_znIdx != _o.cell.zn.idx)
        {
            _znIdx = _o.cell.zn.idx;
            if(_znIdx > 0 && _znIdx < 8)
                ads.Inst.HideBanner();
            else
                ads.Inst.ShowBanner();
        } 
    }

    void FixedUpdate()
    {
        _o.MoveAndLook(ctrls.hasMove, _rigid, ctrls.iputNDir, ctrls.iputNLook);
        
        if (ctrls.hasAct)
        {
            _o.CheckAndUseEquip(Time.time);
            uis.ingam.RefreshWeaponNum(_o.wp);
        }
        else
            _o.model.Ani.SetInteger(model.AniId_nAct, 0);
    }
}
