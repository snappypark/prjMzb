using UnityEngine;

public partial class boltMob : Bolt.EntityEventListener<IMobState>
{
    public void Spawn(Vector3 pos)
    {
        transform.localPosition = pos;
        _o.entity.OnSpawn();
    }

    public int i;
    int i_1by1 = 0;
    public override void SimulateController()
    {
        if (i_1by1 == i)
            _o.entity.Command_1by1();
        ++i_1by1;
        if (i_1by1 >= 5)
            i_1by1 = 0;
        _o.entity.OnUpdate();
    }

    void Update()
    {
        if (_o == null)
            return;
        _o.hud.SetPos(transform.localPosition);
        _o.SetCell();
    }

    void onHp0()
    {
        if (_o != null)
        {
            _o.OutCell();
            _o.hud.Set(unit.hud_.Type.None);
            _o.model.Ani.SetInteger(model.AniId_State, UnityEngine.Random.Range(4, 6));
        }
    }

    void onUseEquip()
    {
        if (_o != null)
            _o.UseEquip(model.Equip.Melee, BoltNetwork.ServerTime);
    }

    void onAni(int aniState)
    {
        if (_o != null)
            _o.model.Ani.SetInteger(model.AniId_State, aniState);
    }
}
