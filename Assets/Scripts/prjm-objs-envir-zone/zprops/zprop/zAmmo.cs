using System.Runtime.CompilerServices;
using UnityEngine;

public class zAmmo : zprp
{
    enum state
    {
        inMenu,
        reloaded,
        reloading,
        none,
    }

    [SerializeField] GameObject _fx;
    [SerializeField] Transform _trGap;
    [SerializeField] SpriteRenderer _spRdMini;

    zone.prp_ _info;
    state _state;
    // in menu option: x, y, z
    // opt: count, delay, amount, amount
    public override void Assign(zone.prp_ info)
    {
        AssignZprp(info);
        _info = info;

        int num = (int)(info.opts.F1+0.1f);

        if (num > 0)
            _state = info.timin.IsEnd() ? state.reloaded : state.reloading;
        else if (num == 0)
            _state = state.none;
        else
            _state = state.inMenu;

        refresh();
    }

    void Update()
    {
        if (_state == state.reloading && _info.timin.IsEnd())
        {
            _state = state.reloaded;
            refresh();
        }
    }

    public override bool OnUsed()
    {
        if (_state == state.reloaded)
        {
            gjs.effs.Play(effs.getKey, _trGap.position);
            audios.Inst.PlaySound(audios.eSoundType.reload);

            _info.timin.SetEnd(_info.opts.F2);
            _info.opts = new f4(_info.opts.F1-1, _info.opts.F2, _info.opts.F3, _info.opts.F4);

            int num = (int)(_info.opts.F1 + 0.1f);
            _state = num > 0 ? state.reloading : state.none;

            gjs.ctrlHuds.ShowWord(langs.PlusAmmo());
            ctrls.Unit.wp.AmmoPistol.incClamp((int)_info.opts.F3);
            ctrls.Unit.wp.AmmoRifle.incClamp((int)_info.opts.F4);
            uis.ingam.RefreshAllWeaponNum(ctrls.Unit.wp);

            refresh();
            if (_state == state.none)
                return true;
        }
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void refresh()
    {
        bool isReloaded = _state == state.reloaded;
        _spRdMini.gameObject.SetActive(isReloaded);
        _fx.SetActive(isReloaded);
        switch (_state)
        {
            case state.inMenu:
                _trGap.localEulerAngles = new Vector3(0, Random.Range(0, 180), 0);
                _trGap.localPosition = new Vector3(_info.opts.F2, _info.opts.F3, _info.opts.F4);
                _trGap.gameObject.SetActive(true);
                break;
            case state.reloaded:
                _trGap.localPosition = new Vector3(0, 0.15f, 0);
                _trGap.gameObject.SetActive(true);
                break;
            default:
                _trGap.gameObject.SetActive(false);
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OnAmmo(unit u)
    {
        if (u.cell.zprp != null && zjs.zprps[u.cell.zprp.cdx].OnUsed())
            u.cell.SetTile();
    }
}
