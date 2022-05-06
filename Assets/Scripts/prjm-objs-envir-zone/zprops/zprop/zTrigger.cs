using System.Runtime.CompilerServices;
using UnityEngine;

public class zTrigger : zprp
{
    enum state
    {
        on,
        off
    }

    [SerializeField] Renderer _rd;
    [SerializeField] SpriteRenderer _spRd;
    [SerializeField] SpriteRenderer _spRdMini;

    zone.prp_ _info;
    // options: colorWall
    public override void Assign(zone.prp_ info)
    {
        AssignZprp(info);
        _info = info;
        
        refresh((byte)info.opts.F1, _info.timin.GetCnt() > 0);
    }

    public override bool OnUsed()
    {
        if (_info.timin.CountAndCheck())
        {
            gjs.effs.Play(effs.trig,
                transform.localPosition + Vector3.up, matWalls.Col((byte)_info.opts.F1));
            
            refresh((byte)_info.opts.F1, false);
            return true;
        }
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void refresh(byte matWallsIdx, bool active)
    {
        _rd.gameObject.SetActive(active);
        _spRd.gameObject.SetActive(active);
        _spRdMini.gameObject.SetActive(active);

        if (active)
        {
            _rd.sharedMaterial = zjs.walls.Mats(
                (byte)Mathf.Clamp((27 + matWallsIdx) % 29, 0, 28) );
            _spRdMini.color = matWalls.Col(matWallsIdx);
        }
    }

    const float _effGap = 0.05f;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OnTrig(unit u)
    {
        cel1l c = u.cell;
        if (c.zprp != null && zjs.zprps[c.zprp.cdx].OnUsed())
        {
            core.unitClones.EnqMsg(msgType.TrigA, _effGap, 1, c);

            for (int i = 1; i < nj.idx.MaxNum7x7; ++i)
            {
                cel1l c2 = c.C3X3(i);
                c2.IsPath = true;
                c2.zn.changed = true;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Bomb_ByTrig(cel1l c_, short idx_)
    {
        audios.Inst.PlaySound(audios.eSoundType.elect);
        if (idx_ < nj.idx.MaxNum7x7)
            bomb_ByTrig2(c_, idx_);

        ++idx_;
        if (idx_ < nj.idx.MaxNum7x7)
            bomb_ByTrig2(c_, idx_);

        ++idx_;
        if (idx_ < nj.idx.MaxNum7x7)
            bomb_ByTrig2(c_, idx_);

        ++idx_;
        if (idx_ < nj.idx.MaxNum7x7)
            bomb_ByTrig2(c_, idx_);

        ++idx_;
        if (idx_ < nj.idx.MaxNum7x7)
            core.unitClones.EnqMsg(msgType.TrigA, _effGap, idx_, c_);

    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void bomb_ByTrig2(cel1l c_, short idx_)
    {
        cel1l c = core.zells[c_.pt.x + nj.idx.INxN(idx_), 
                              c_.pt.z + nj.idx.JNxN(idx_)];
        gjs.effs.Play(effs.elect, c.ct);

        bom.BumpOffEvir(c);
        int num = c.units.Count;
        for (int r = 0; r < num; ++r)
        {
            unit u = c.RollUnit;
            if(ctrls.Unit.cdx != u.cdx)
                u.DecHp(120);
        }
    }
}