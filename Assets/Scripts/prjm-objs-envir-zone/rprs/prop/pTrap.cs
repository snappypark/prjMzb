using System.Runtime.CompilerServices;
using UnityEngine;

public class pTrap : rpr
{
    enum state {
        check,
        off,
        on,

        check_fire,
        off_fire,
        on_fire,

        check_slow,
        off_slow,
        on_slow,

        on_firwall,
    }

    [SerializeField] ParticleSystem _ps1;
    [SerializeField] ParticleSystem _ps2;
    [SerializeField] SpriteRenderer _sr1;
    [SerializeField] SpriteRenderer _sr2;
    cel1l _cell;
    cel1l.Type _ctype = cel1l.Type.None;

    state _state = state.check;

    //opts: null, speed, time gap, {detect value}
    public override void Assign(cel1l cell)
    {
        _cell = cell;
        _ctype = _cell.type;
        switch (_ctype)
        {
            case cel1l.Type.Trap_Fire:
                _sr1.color = new Color(0.8f, 0.3f, 0.21f);
                _sr2.color = new Color(0.915f, 0.337f, 0.237f);
                _state = state.check_fire;
                break;
            case cel1l.Type.Trap_Slow:
                _sr1.color = new Color(0.8f, 0.785f, 0.21f);
                _sr2.color = new Color(0.915f, 0.885f, 0.237f);
                _state = state.check_slow;
                break;
            case cel1l.Type.Trap_FireWall:
                _sr1.color = new Color(0.8f, 0.3f, 0.21f);
                _sr2.color = new Color(0.915f, 0.337f, 0.237f);
                _sr1.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                _state = state.on_firwall;
                _cell.opts = f4.One;
                break;
        }
        gameObject.SetActive(true);
    }

    public override void UnAssign()
    {
        switch (_ctype)
        {
            case cel1l.Type.Trap_Fire:
            case cel1l.Type.Trap_FireWall:
                _ps1.Stop();
                break;
            case cel1l.Type.Trap_Slow:
                _ps2.Stop();
                break;

        }
        gameObject.SetActive(false);
    }

    public const float WaitRatio = 0.5f;
    void Update()
    {
        switch (_state)
        {
            case state.check_fire:
                setOpts_Movement();
                _state = _cell.opts.F4 < WaitRatio ? state.off_fire : state.on_fire;
                break;
            case state.off_fire:
                if (_ps1.isPlaying)
                    _ps1.Stop();
                setScale_ByOpts();
                _state = state.check_fire;
                break;
            case state.on_fire:
                if (!_ps1.isPlaying)
                    _ps1.Play();
                _sr1.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                _state = state.check_fire;
                break;


            case state.check_slow:
                setOpts_Movement();
                _state = _cell.opts.F4 < WaitRatio ? state.off_slow : state.on_slow;
                break;
            case state.off_slow:
                if (_ps2.isPlaying)
                    _ps2.Stop();
                setScale_ByOpts();
                _state = state.check_slow;
                break;
            case state.on_slow:
                if (!_ps2.isPlaying)
                    _ps2.Play();
                _sr1.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                _state = state.check_slow;
                break;

            case state.on_firwall:
                if (!_ps1.isPlaying)
                    _ps1.Play();
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void setOpts_Movement()
    {
        _cell.opts = new f4(0, _cell.opts.F2, _cell.opts.F3,
            Quadratic.Zigzag01((Time.time - core.stages.startTime) + _cell.opts.F3, _cell.opts.F2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void setScale_ByOpts()
    {
        float sc = _cell.opts.F4 * 1.35f;
        _sr1.transform.localScale = new Vector3(sc, sc, sc);
    }

    // static
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OnSlowTrap(cel1l cell, Rigidbody rigid)
    {
        if (cell.opts.F4 > WaitRatio)
            rigid.velocity *= 0.6f;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OnFireTrap(unit unit)
    {
        if (unit.cell.opts.F4 > WaitRatio &&
            unit.attb.dlyDmgFire.IsEndAndReset())
        {
            core.unitClones.EnqMsg(msgType.DmgFire, 0.8f, unit.cdx);
            audios.Inst.PlaySound(audios.eSoundType.burned);
            unit.DecHp(14, unit.DecHpType.Fire);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OnFireWallTrap(unit unit)
    {
        if (unit.attb.dlyDmgFire.IsEndAndReset())
        {
            core.unitClones.EnqMsg(msgType.DmgFire, 0.8f, unit.cdx);
            audios.Inst.PlaySound(audios.eSoundType.burned);
            unit.DecHp(14, unit.DecHpType.Fire);
        }
    }
}
