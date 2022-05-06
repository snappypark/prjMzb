using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sMeteor : slug
{
    public static float timeGap = 0;
    public static int numGap = 0;
    public static float speed = 0;

    Vector3 _sV;

    cel1l _c;
    public override void OnActive(cel1l c)
    {
        _c = c;
        _sV = new Vector3(0, -sMeteor.speed, 0);
    }
    
    void FixedUpdate()
    {
        transform.localPosition += _sV * Time.fixedDeltaTime;//Time.fixedDeltaTime;

        if (transform.localPosition.y < 0.4f)
        {
            int num = _c.units.Count;
            for (int r = 0; r < num; ++r)
            {
                unit unit = _c.RollUnit;

                unit.DecHp(50);
                gjs.effs.Play_HitBullet(transform.localPosition, new f2(1,0));
            }

            zjs.rprs.SetWithFire(_c);
            audios.Inst.PlaySound(audios.eSoundType.brek);
            gjs.effs.Play(effs.effWall, _c.ct, Color.gray);
            abjs.slugs.Unactive(type, cdx);
        }
    }



    public static int Count = 0;
    public static bool ShootMeteors()
    {
        if(++Count > 50) 
            return false;

        cel1l cell = ctrls.Unit.cell;

        for (int i = 0; i < numGap; ++i)
        {
            int x = cell.pt.x + Random.Range(-9, 9);
            int z = cell.pt.z + Random.Range(-9, 9);
            if (cel1ls.IsOutIdx(x, z))
                continue;
            cel1l target = core.zells[x, z];

            if (target.type == cel1l.Type.Tile)
            {
                target.type = cel1l.Type.Trap_WaitForFire;
                abjs.slugs.ShootMeteor(core.zells[x, z], 11 + Random.Range(0, 5));
            }
        }
        return true;
    }
}
