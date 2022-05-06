using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngineEx;

public class zBomb : zprp
{
    enum state
    {
        inMenu,
        reloaded,
        reloading,
        none,
    }

    public enum Type
    {
        Default,
        AutoExplosion = 31587,
    }

    [SerializeField] GameObject _fx;
    [SerializeField] Transform _trGap;
    [SerializeField] SpriteRenderer _spRdMini;

    zone.prp_ _info;
    state _state;
    Type _type;

    // option(menu): x, y, z
    // option: count, delay, num, type
    public override void Assign(zone.prp_ info)
    {
        AssignZprp(info);
        _info = info;

        int num = (int)(info.opts.F1 + 0.1f);

        if (num > 0)
            _state = info.timin.IsEnd() ? state.reloaded : state.reloading;
        else if (num == 0)
            _state = state.none;
        else
            _state = state.inMenu;
        
        if (Type.AutoExplosion == (Type)info.opts.F4 && core.Inst.flowMgr.CurType == Flow.iTypePlay )
        {
            zjs.zprps.PutOff(info);
            abjs.boms.Throw(transform.localPosition, info.opts.F2);
            _state = state.none;
        }

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
            audios.Inst.PlaySound(audios.eSoundType.grab);

            _info.timin.SetEnd(_info.opts.F2);
            _info.opts = new f4(_info.opts.F1 - 1, _info.opts.F2, _info.opts.F3, _info.opts.F4);

            int num = (int)(_info.opts.F1 + 0.1f);
            _state = num > 0 ? state.reloading : state.none;

            gjs.ctrlHuds.ShowWord(langs.PlusBomb());
            ctrls.Unit.wp.Bomb.incClamp((int)_info.opts.F3);
            uis.ingam.RefreshBombNum(ctrls.Unit.wp.Bomb.value);

            refresh();
            
        }
        return _state == state.none;
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
                _type = Type.Default;
                _fx.SetActive(false);
                _trGap.gameObject.SetActive(true);
                break;
            case state.reloaded:
                _trGap.localPosition = new Vector3(0, 0.3f, 0);
                _trGap.gameObject.SetActive(true);
                break;
            default:
                _trGap.gameObject.SetActive(false);
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OnBomb(unit u)
    {
        if (u.cell.zprp != null && zjs.zprps[u.cell.zprp.cdx].OnUsed())
            u.cell.SetTile();
    }
}
