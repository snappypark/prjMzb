using System.Collections.GenericEx;
using UnityEngine;

public partial class unitClones : nj.ObjsPool2<unitClones, unit>
{
    protected override short Capacity { get { return 256; } }
    public unit GetUnit(short cdx) { return cdx < _num ? _cObj[cdx] : null; }

    public const byte ally0 = 0, ally1 = 1, ally2 = 2, ally3 = 3, ally4 = 4, ally5 = 5, allyMax = 6;
    public ally_ ally = new ally_();
    
    protected override void _awake()
    {
        base._awake();
        _beginCdx = 1; _num = 1;
        _cObj[0] = transform.GetChild(0).GetComponent<unit0>();
        _awakePulse();
        _awakeMsgr();
    }
    
    public override void Clear()
    {
        pulses.ClearExceptOne(0);
        clearMsgs();
        ally.Clear();
        base.Clear();
    }

    public npc PreLoadNpc(zone zn, Vector3 pos, Vector3 dir, 
        model.eType model, byte matUnit, model.Equip equip,
        byte ally, byte iHead, byte iBody, byte iMelee, byte aniCtrlIdx, unit.SubType fsmType)
    {
        npc nc = core.units.CloneRootTranModel(transform, units.eNpc,
            model, matUnit, equip, iHead, iBody, iMelee, aniCtrlIdx, unit.TranType.Default) as npc;
        
        float scale = Random.Range(1.1f, 1.65f);
        nc.tran.localScale = new Vector3(scale,scale + Random.Range(0.0f, 0.2f),scale);

        nc.cdx = _num;
        zn.waitNpcs.Enqueue(_num);
        _cObj[_num++] = nc;

        nc.attb.Set(ally, nc.cdx, true);
        nc.SetEquip(equip);

        nc.Init(unit.Type.Npc, fsmType);
        //fsm_npc.Factory(ref nc, fsmType);
        nc.SetPosDir(pos, dir);
        nc.gameObject.SetActive(false);
        return nc;
    }

    #region plyr
    unit preLoadNetPlayers(byte type, byte ally, bool isNpc)
    {
        unit u = core.units.CloneRoot(transform, type);
        u.tran = null;
        u.cdx = _num;
        _cObj[_num++] = u;

        u.attb.Set(ally, u.cdx, isNpc);
        u.FillHpAndWeapon(0, 0, 0);

        u.gameObject.SetActive(true);
        return u;
    }

    public void PreLoadUnits_Escape()
    {
        for (int i = 1; i < 9; ++i)
            preLoadNetPlayers(units.eUpc, 0, false);
        for (int i = 9; i < 14; ++i)
            preLoadNetPlayers(units.eNpc, 2, true);
    }

    public void PreLoadUnits_Battle()
    {
        for (int i = 1; i < 5; ++i)
            preLoadNetPlayers(units.eUpc, 0, false);
        for (int i = 5; i < 9; ++i)
            preLoadNetPlayers(units.eUpc, 1, false);
        for (int i = 9; i < 10; ++i)
            preLoadNetPlayers(units.eNpc, 2, true);
    }

    public unit LinkBoltUnit(byte ally, Transform tran)
    {
        for (int i = 1; i < 9; ++i)
        {
            unit unit = _cObj[i];
            if (unit.attb.ally == ally && unit.tran == null)
            {
                unit.tran = tran;
                tran.SetParent(unit.transform);
                return unit;
            }
        }
        return null;
    }
    public unit LinkBoltMob(Transform tran, int end = 14)
    {
        for (int i = 9; i < end; ++i)
        {
            unit unit = _cObj[i];
            if (unit.tran == null)
            {
                unit.tran = tran;
                tran.SetParent(unit.transform);
                return unit;
            }
        }
        return null;
    }
    #endregion
}
