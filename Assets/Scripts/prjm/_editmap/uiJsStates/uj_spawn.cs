using UnityEngine;

public class uj_spawn : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.Spawn; } }

    [SerializeField] js.spawn_ _spawn;

    void OnEnable()
    {
        _spawn = new js.spawn_(0, 0, 0, 0, 0, 0, 0, 10, 10, 0);
    }

    void LateUpdate()
    {
        uis.editmap.SetSelectMode(uj_editmap.eSelect.SingleCel1l);
    }

    public override void Create(i5 b1d)
    {
        Vector3 pos = cel1l.Center(new Pt(b1d.X0, b1d.Y, b1d.Z0));
        Vector3 dir = _spawn.dir.normalized;
        if (js.Inst.CheckHasSpawn(pos))
            js.Inst.RemoveSpawn(pos);
        else
        {
            js.Inst.AddSpawn(_spawn.ally, 
                pos.x, pos.y, pos.z,
                dir.x, dir.y, dir.z,
                _spawn.numAmmoPistol, _spawn.numAmmoRifle, _spawn.numBomb,
                js.SeriType.WithLoad );
        }
    }
}
