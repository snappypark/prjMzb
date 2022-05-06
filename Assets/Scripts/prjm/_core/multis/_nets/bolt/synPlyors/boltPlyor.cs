using Bolt;
using UnityEngine;

public partial class boltPlyor : Bolt.EntityEventListener<IPlyerState>
{
    void Update()
    {
        if (_o == null)
            return;

        _o.hud.SetPos(transform.localPosition);

        if (ctrls.Inst.IsNone)
            return;
        _o.SetCell();

        bool isMoving = state.isMoved;

        _o.model.Ani.SetBool(model.AniId_bRun, isMoving);

    }

    void onWeaponChanged()
    {
        if (_o != null)
            _o.SetEquip((model.Equip)state.wp);
    }

    void onUseEquip()
    {
        if (_o != null)
        {
            _o.UseEquip((model.Equip)state.wp, BoltNetwork.ServerTime);
            if (entity.IsOwner)
                uis.ingam.RefreshWeaponNum(_o.wp);
        }
    }

    void onScore(int score)
    {
        core.multis.UpdatePlayers();
    }

    void onZdx(int zdx)
    {
        core.multis.UpdatePlayers();

        int newBestZdx = core.multis.GetBestZn_afterSort();
        if (newBestZdx > multis.BestZdx)
        {
            multis.BestZdx = newBestZdx;
            core.multis.RespawnMobs_ByServer(newBestZdx);
        }
    }
}
