using UnityEngine;

public class sHit : slug
{
    delay _endTime = new delay(0.31f);

    public override void OnActive(unit u)
    {
        _dmg = 30;
        _oCdx = u.cdx;
        _endTime.Reset();
    }

    void Update()
    {
        if (_endTime.InTime())
            return;

        unit o = core.unitClones.GetUnit(_oCdx);
        if(o != null && o.tran != null)
        {
            Vector3 hitPt = o.model.melee.transform.position + o.tran.forward * 0.44f;
            cel1l cc = core.zells[cel1l.Pt(hitPt)];
            for (int i = 0; i < nj.idx.MaxNum3x3; ++i)
            {
                cel1l c = cc.C2X2(i);
                int num = c.units.Count;
                for (int r = 0; r < num; ++r)
                {
                    unit unit = c.RollUnit;
                    if (unit.checkHp0_SameAlly(o.attb.ally))
                        continue;

                    if (f2.VecXZ(unit.tran.localPosition, hitPt).SqrMagnitude() > dist.sqr1_0)
                        continue;
                    
                    unit.DecHp(_dmg, unit.DecHpType.Default, o);
                    gjs.effs.Play(effs.hitMelee, hitPt, Color.white);
                }
            }
        }
        slugs.Inst.Unactive(type, cdx);
    }
}
