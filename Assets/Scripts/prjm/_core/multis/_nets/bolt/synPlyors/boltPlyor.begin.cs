using UnityEngine;
using UdpKit;

public partial class boltPlyor
{
    public byte ally;
    unit _o = null;
    Rigidbody _rigid;
    public override void Attached()
    {
        state.SetTransforms(state.tran, transform);
        token tk = entity.AttachToken as token;
        ally = tk.ally;
        name = tk.name;

        int idesx_h = tk.modxes;
        byte ihand = (byte)(idesx_h % 100); idesx_h = (int)(idesx_h * 0.01f + 0.5f);
        byte ibody = (byte)(idesx_h % 100); idesx_h = (int)(idesx_h * 0.01f + 0.5f);
        byte ihead = (byte)(idesx_h % 100); idesx_h = (int)(idesx_h * 0.01f + 0.5f);
        byte iskin = (byte)(idesx_h % 100); idesx_h = (int)(idesx_h * 0.01f + 0.5f);
        byte imodel = (byte)(idesx_h % 100);

        _o = core.unitClones.LinkBoltUnit(tk.ally, transform);
        core.units.CloneModel(_o, (model.eType)(imodel),
            matUnits.ByType(iskin), model.Equip.Rifle, ihead, ibody, ihand, units.eAniCtrlPlayor);
            
        _rigid = this.GetComponent<Rigidbody>();

        _o.SetEquip(model.Equip.Rifle);

        _o.Init(unit.Type.None);
        
        _o.hud.SetName(name);
        setMinimapColor(entity.IsOwner, ally);

        state.AddCallback("wp", onWeaponChanged);
        state.AddCallback("chat", callback: () => uis.ingam.multi.chat.RefreshChat_WithNewMsg(string.Format("{0}: {1}", name, state.chat))); // <color=#ff0000ff>colorfully: </color> amused
        state.AddCallback("score", callback: () => onScore(state.score));
        state.AddCallback("zdx", callback: () => onZdx(state.zdx));
        state.OnUseEquip = onUseEquip;
    }


    public override void Detached()
    {
        if (_o != null)
        {
            _o.hud.SetPos(Vector3.zero);
            _o.tran = null;
            _o = null;
        }
        DestroyImmediate(this);
    }
    
    void setMinimapColor(bool isOwner, byte ally)
    {
        switch (ally)
        {
            case 0:
                _o.hud.SetMiniColor(isOwner ? pnn_plyors.sky : pnn_plyors.blue);
                break;
            case 1:
                _o.hud.SetMiniColor(isOwner ? pnn_plyors.org : pnn_plyors.red);
                break;
        }
    }

    public class token : Bolt.IProtocolToken
    {
        public byte ally;
        public string name;
        public int modxes;

        public token() { }
        void Bolt.IProtocolToken.Read(UdpPacket packet) {
            ally = packet.ReadByte();
            name = packet.ReadString();
            modxes = packet.ReadInt();
        }

        void Bolt.IProtocolToken.Write(UdpPacket packet) {
            packet.WriteByte(ally);
            packet.WriteString(name);
            packet.WriteInt(modxes);
        }
    }

    public class tokenConnect : Bolt.IProtocolToken
    {
        public string name;
        public int modxes;

        public void Read(UdpPacket packet)
        {
            name   = packet.ReadString();
            modxes = packet.ReadInt();
        }

        public void Write(UdpPacket packet)
        {
            packet.WriteString(name);
            packet.WriteInt(modxes);
        }
    }
}

