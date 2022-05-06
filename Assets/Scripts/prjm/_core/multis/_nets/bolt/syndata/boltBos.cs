using UnityEngine;

public partial class boltBos : Bolt.EntityEventListener<IBosState>
{
    public static Vector3 SpawnPos;

    public void Spawn()
    {
        state.ani = 0;
        transform.localPosition = SpawnPos;
        _o.entity.OnSpawn();
    }
    
    public override void SimulateController()
    {
        _o.entity.Command_1by1();
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
