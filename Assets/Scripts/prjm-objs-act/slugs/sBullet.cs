using UnityEngine;
using UnityEngineEx;

public class sBullet : slug
{
    Vector3 _sV;
    float _speed;

    public override void OnActive(unit u, Vector3 nV, float speed, int dmg_)
    {
        _oCdx = u.cdx;
        _ally = u.attb.ally;
        transform.forward = nV;
        _sV = nV * speed;
        _speed = speed;
        _dmg = dmg_;
    }

    public override void OnActive(byte ally_, Vector3 nV, float speed, int dmg_)
    {
        _oCdx = 19999;
        _ally = ally_;
        transform.forward = nV;
        _sV = nV * speed;
        _speed = speed;
        _dmg = dmg_;
    }

    Pt _pt;
    void FixedUpdate()
    {
        transform.localPosition += _sV * Time.fixedDeltaTime;//Time.fixedDeltaTime;
        _pt = cel1l.Pt(transform.localPosition);

        Pt pt11 = cel1l.Pt11(transform.localPosition);
        cel1l c11 = core.zells[pt11];

        for (int i = 0; i < nj.idx.MaxNum2x2; ++i)
        {
            cel1l c = c11.C2X2(i);
            switch (c.colliType)
            {
                case cel1l.ColliType.Null:
                    if (c.pt == _pt)
                    {
                        abjs.slugs.Unactive(type, cdx);
                        return;
                    }
                    break;
                case cel1l.ColliType.Cube:
                    onCollisionCube(c);
                    break;
                case cel1l.ColliType.Capsule:
                    if (VectorEx.SqrMagnitudeXZ(c.ct, transform.localPosition) < dist.sqr0_5)
                    {
                        abjs.slugs.Unactive(type, cdx);
                        return;
                    }
                    break;
                case cel1l.ColliType.None:
                    int num = c.units.Count;
                    for (int r = 0; r < num; ++r)
                    {
                        unit unit = c.RollUnit;
                        if (unit.checkHp0_SameAlly(_ally))
                            continue;

                        f2 v = f2.VecXZ(unit.tran, transform);
                        float size = unit.tran.localScale.x*0.42f;
                        if (v.SqrMagnitude() < size*size)
                        {
                            unit.DecHp(_dmg, unit.DecHpType.Default , core.unitClones.GetUnit(_oCdx));
                            gjs.effs.Play_HitBullet(transform.localPosition, v);
                            abjs.slugs.Unactive(type, cdx);
                            return;
                        }
                    }
                    break;
            }
        }
    }

    void onCollisionCube(cel1l c)
    {
        switch (c.type)
        {
            case cel1l.Type.Tnt:
                if(c.pt == _pt)
                {
                    abjs.slugs.Unactive(type, cdx);
                    if(c.IsState0)
                        pTnt.OnActive(c);
                }
                break;
            case cel1l.Type.PushUp:
            case cel1l.Type.PushSide_X:
            case cel1l.Type.PushSideX:
            case cel1l.Type.PushSide_Z:
            case cel1l.Type.PushSideZ:
                if(c.IsInsideCubeColli(transform.localPosition)) 
                    abjs.slugs.Unactive(type, cdx);
                break;
            default:
                if(c.pt == _pt)
                    abjs.slugs.Unactive(type, cdx);
                break;
        }
    }
}
