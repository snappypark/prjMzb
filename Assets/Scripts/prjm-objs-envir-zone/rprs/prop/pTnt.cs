using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pTnt : rpr
{
    const float timeGap = 2.5f;
    const byte eActived = 1, eActived2 = 2;

    [SerializeField] GameObject _line;
    [SerializeField] MeshRenderer _render;

    cel1l _cell;
    public override void Assign(cel1l cell)
    {
        _cell = cell;
        _line.SetActive(!_cell.IsState0);
        _render.material = zjs.rprs.mats[matProps.Tnt];
        gameObject.SetActive(true);
    }

    public override void UnAssign()
    {
        gameObject.SetActive(false);
    }

    bool bb = true;
    delay _delay = new delay(0.133f);
    void Update()
    {
        switch(_cell.state)
        {
            case 0:
            break;
            case eActived:
                if(!_line.activeSelf)
                    _line.SetActive(true);
                _cell.state = eActived2;
            break;
            case eActived2:
                if(_delay.IsEndAndReset())
                {
                    bb = !bb;
                    _render.material = bb ? zjs.rprs.mats[matProps.Tnt] : zjs.rprs.mats[matProps.Red];
                }
                _cell.state = eActived;
            break;
        }

    }

    public static void OnActive(cel1l c)
    {
        core.unitClones.EnqMsg(msgType.Tnt, timeGap, 0, c);
        c.state = eActived;
    }

    public static void OnExplosion(cel1l cell)
    {
        gjs.effs.Play(effs.bomb, cell.ct);
        audios.Inst.bomb.PlaySound();
        for (int i = 0; i < nj.idx.MaxNum3x3; ++i)
        {
            cel1l c = cell.C3X3(i);

            bom.BumpOffEvir(c);
            int num = c.units.Count;
            for (int r = 0; r < num; ++r)
                c.RollUnit.DecHp(300, unit.DecHpType.Default);
        }
    }
}
