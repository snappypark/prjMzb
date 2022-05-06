using UnityEngine;

public partial class boltBos
{
    unit _o = null;

    public override void Attached()
    {
        state.SetTransforms(state.tran, transform);
        token tk = entity.AttachToken as token;

        int idesx_h = tk.modxes;
        byte ihand = (byte)(idesx_h % 100); idesx_h = (int)(idesx_h * 0.01f + 0.5f);
        byte ibody = (byte)(idesx_h % 100); idesx_h = (int)(idesx_h * 0.01f + 0.5f);
        byte ihead = (byte)(idesx_h % 100); idesx_h = (int)(idesx_h * 0.01f + 0.5f);
        byte iskin = (byte)(idesx_h % 100); idesx_h = (int)(idesx_h * 0.01f + 0.5f);
        byte imodel = (byte)(idesx_h % 100);

        _o = core.unitClones.LinkBoltMob(transform);
        core.units.CloneModel(_o, (model.eType)(imodel),
            matUnits.ByType(iskin), model.Equip.Melee, ihead, ibody, ihand, units.eAniCtrlNpcCitizen);
        _o.SetEquip(model.Equip.Melee);
        _o.hud.Set(unit.hud_.Type.OnlyMiniMap);

        _o.Init(unit.Type.MultiBos);

        state.AddCallback("ani", callback: () => onAni(state.ani));
        state.OnUseEquip = onUseEquip;
        state.OnHp0 = onHp0;
    }

    public override void Detached()
    {
        if (_o != null)
        {
            _o.hud.SetPos(Vector3.zero);
            _o.tran = null;
            _o = null;
        }
    }

    public class token : Bolt.IProtocolToken {
        public int modxes;
        public token() { }
        void Bolt.IProtocolToken.Read(UdpKit.UdpPacket packet) {
            modxes = packet.ReadInt(); }
        void Bolt.IProtocolToken.Write(UdpKit.UdpPacket packet) {
            packet.WriteInt(modxes); }
    }
}
