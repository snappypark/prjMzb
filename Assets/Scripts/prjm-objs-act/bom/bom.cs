using System.Runtime.CompilerServices;
using UnityEngine;

public class bom : nj.qObj
{
    enum State
    {
        None,
        WaitForPreEnd,
        WaitForEnd,
    }

    [SerializeField] public ParticleSystem ps;
    [SerializeField] Transform _tranLine;

    [HideInInspector] public Vector3 nor;
    [HideInInspector] public float _endTime;
    [HideInInspector] public float overDuration;
    [HideInInspector] public const float speed = 26.0f;

    short _oCdx = 19999;
    cel1l _cell = null;
    bool _prePath = false;
    float _endPathTime;
    public void Assign(short oCdx = 19999, float delay = 2.0f)
    {
        _oCdx = oCdx;
        _cell = core.zells[cel1l.Pt(transform.localPosition)];

        _tranLine.localPosition = _cell.ct - transform.localPosition;
        _endTime = Time.time + delay;
        ps.Play();
        _endPathTime = _endTime - _cell.zn.durationOfJsp;
        _prePath = true;

    }

    void Update()
    {
        if (_prePath && _endPathTime < Time.time )
        {
            for (int i = 0; i < nj.idx.MaxNum3x3; ++i)
                _cell.C3X3(i).SetPrePath();
            _prePath = false;
        }

        if (_endTime > Time.time)
            return;

        for (int i = 0; i < nj.idx.MaxNum3x3; ++i)
        {
            cel1l c = _cell.C3X3(i);

            BumpOffEvir(c);
            int num = c.units.Count;
            for (int r = 0; r < num; ++r)
                c.RollUnit.DecHp(300, unit.DecHpType.Default, core.unitClones.GetUnit(_oCdx));
        }

        audios.Inst.bomb.PlaySound();
        gjs.effs.Play(effs.bomb, transform.localPosition);
        gjs.smudges.Replace(smudges.eExplosionBomb, 
            new Vector3(transform.localPosition.x, _cell.ct.y+0.05f, transform.localPosition.z));
        abjs.boms.Unactive(type, cdx);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void BumpOffEvir(cel1l c)
    {
        switch (c.type)
        {
            case cel1l.Type.Wall:
                gjs.effs.Play(effs.effWall, c.ct, Color.gray);
                zjs.walls.Remove_SkipMetal(c);
                break;
            case cel1l.Type.Box:
            case cel1l.Type.Tube: // case cel1l.Type.LambOn: // case cel1l.Type.LambOff:
            case cel1l.Type.BotFire:
            case cel1l.Type.BotGun:
            case cel1l.Type.BotZ1:
            case cel1l.Type.BotZ2:
            case cel1l.Type.BotZ3:
            case cel1l.Type.BotZ4:
            case cel1l.Type.BotZ5:
            case cel1l.Type.BotZ6:
            case cel1l.Type.PushUp:
            case cel1l.Type.PushSide_X:
            case cel1l.Type.PushSideX:
            case cel1l.Type.PushSide_Z:
            case cel1l.Type.PushSideZ:
            case cel1l.Type.PushZA1:
            case cel1l.Type.PushZA2:
            case cel1l.Type.PushZA3:
            case cel1l.Type.PushZA4:
            case cel1l.Type.PushZA5:
            case cel1l.Type.PushZB1:
            case cel1l.Type.PushZB2:
            case cel1l.Type.PushZB3:
            case cel1l.Type.PushZB4:
            case cel1l.Type.PushZB5:
            case cel1l.Type.PushZC1:
            case cel1l.Type.PushZC2:
            case cel1l.Type.PushZC3:
            case cel1l.Type.PushZC4:
            case cel1l.Type.PushZC5:
            case cel1l.Type.Tree1A_Blue:
            case cel1l.Type.Tree1B_Pink:
            case cel1l.Type.Tree2A_Blue:
            case cel1l.Type.Tree2B_Pink:
            case cel1l.Type.Tree3A_Red:
            case cel1l.Type.Tree3B_Green:
            case cel1l.Type.Tree4A_Red:
            case cel1l.Type.Tree4B_Green:
            case cel1l.Type.Tnt:
                gjs.effs.Play(effs.effWall, c.ct, Color.gray);
                zjs.rprs.RemoveWithCheck(c, true);
                break;
            case cel1l.Type.Bush:
            case cel1l.Type.Bush1:
            case cel1l.Type.Bush2:
            case cel1l.Type.Bush3:
                zjs.rprs.ReplaceWithFire(c);
                break;

        }
    }
}
