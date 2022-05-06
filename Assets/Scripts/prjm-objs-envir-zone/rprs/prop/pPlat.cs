using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngineEx;

public class pPlat : rpr
{
    [SerializeField] SpriteRenderer _spIcon;
    [SerializeField] SpriteRenderer _spRound;
    [SerializeField] SpriteRenderer _spFrame;

    cel1l _cell;

    //opts: nil, nil, nil, nil
    public override void Assign(cel1l cell)
    {
        _cell = cell;

        switch (_cell.type)
        {
            case cel1l.Type.Plat_X:
                _spIcon.sprite = zjs.rprs.sps[spriteProps.left];
                _spIcon.color = spriteProps.mvCol0;
                _spRound.color = spriteProps.mvCol1; _spFrame.color = spriteProps.mvCol2;
                break;
            case cel1l.Type.PlatX:
                _spIcon.sprite = zjs.rprs.sps[spriteProps.right];
                _spIcon.color = spriteProps.mvCol0;
                _spRound.color = spriteProps.mvCol1; _spFrame.color = spriteProps.mvCol2;
                break;
            case cel1l.Type.Plat_Z:
                _spIcon.sprite = zjs.rprs.sps[spriteProps.down];
                _spIcon.color = spriteProps.mvCol0;
                _spRound.color = spriteProps.mvCol1; _spFrame.color = spriteProps.mvCol2;
                break;
            case cel1l.Type.PlatZ:
                _spIcon.sprite = zjs.rprs.sps[spriteProps.up];
                _spIcon.color = spriteProps.mvCol0;
                _spRound.color = spriteProps.mvCol1; _spFrame.color = spriteProps.mvCol2;
                break;
            case cel1l.Type.PlatSpd:
                _spIcon.sprite = zjs.rprs.sps[spriteProps.star];
                _spIcon.color = spriteProps.mvCol0;
                _spRound.color = spriteProps.mvCol1; _spFrame.color = spriteProps.mvCol2;
                break;
            case cel1l.Type.PlatHeal:
                _spIcon.sprite = zjs.rprs.sps[spriteProps.heart];
                _spIcon.color = spriteProps.hlCol0;
                _spRound.color = spriteProps.hlCol1; _spFrame.color = spriteProps.hlCol2;
                break;
            case cel1l.Type.PlatOil:
                _spIcon.sprite = zjs.rprs.sps[spriteProps.oil];
                _spIcon.color = spriteProps.oiCol0;
                _spRound.color = spriteProps.oiCol1; _spFrame.color = spriteProps.oiCol2;
                break;
        }
        gameObject.SetActive(true);
    }

    public override void UnAssign()
    {
        gameObject.SetActive(false);
    }

    nj.idxArr _sfx = new nj.idxArr(2);
    float f = 0;
    void Update()
    {
        switch (_sfx.GetAfterRoll())
        {
            case 0:
                _spFrame.transform.localPosition = new Vector3(0, 0.05f + f*0.3f, 0);
                break;
            case 1:
                f = Quadratic.Zigzag01((Time.time - core.stages.startTime), 2.0f);
                break;
        }
    }

    public const float duration = 1.0f;
    public const float moveForce0 = 390.0f;
    public const float moveForce = 120.0f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OnPlatMove(unit u, msgType msgType_)
    {
        zjs.rprs.Remove(u.cell);
        if (u.attb.dlySpd.IsEndAndReset())
        {
            audios.Inst.PlaySound(audios.eSoundType.wind);
            effs.Inst.Play(effs.spd, u.tran);
            core.unitClones.EnqMsg(msgType_, duration, u.cdx);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OnEvent_Spd(unit u)
    {
        zjs.rprs.Remove(u.cell);
        if (u.attb.dlySpd.IsEndAndReset())
        {
            audios.Inst.PlaySound(audios.eSoundType.wind);
            u.attb.moveSpd = unit.FastMoveSpd;
            core.unitClones.EnqMsg(msgType.SpdEnd, 5, u.cdx);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddForce(Vector3 dir, unit u)
    {
        float s = u.attb.dlySpd.Ratio10() * moveForce0;
        ctrls.Inst.AddForce(dir, moveForce + s);
    }
}
