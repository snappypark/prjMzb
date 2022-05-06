using UnityEngine;

public partial class unit
{
    public enum Type { None, Solo, SoloLadder, Npc, MultiPlyor, MultiNpc, MultiBos }
    Type _type;

    public enum SubType { None,
        Default,
        Tmp1,
        ZombieLv1, ZombieLv2, ZombieLv3, ZombieLv4, ZombieLv5,
        MeleeLv1, MeleeLv2, MeleeLv3, MeleeLv4,
        PistolLv1, PistolLv2, PistolLv3, PistolLv4,
    }

    public void Init(Type type, SubType sub = SubType.None)
    {
        _type = type;
        if (entity != null)
            GameObject.DestroyImmediate(entity);

        switch (type)  {
            case Type.Npc: initNpc(sub); break;
            case Type.Solo:       entity = gameObject.AddComponent<unitEntitySolo>(); break;
            case Type.SoloLadder: entity = gameObject.AddComponent<unitEntitySoloLadder>(); break;
            case Type.MultiPlyor: entity = gameObject.AddComponent<unitEntityMultiPlyor>(); break;
            case Type.MultiNpc:   entity = gameObject.AddComponent<unitEntityMultiNpc>(); break;
            case Type.MultiBos:   entity = gameObject.AddComponent<unitEntityMultiBos>(); break;
            default:              entity = gameObject.AddComponent<unitEntity>(); break; }

        entity.Init(this, type, sub);
    }

    void initNpc(SubType sub)
    {
        switch (sub) {
            case SubType.ZombieLv1: case SubType.ZombieLv2: case SubType.ZombieLv3: case SubType.ZombieLv4: case SubType.ZombieLv5:
                entity = gameObject.AddComponent<unitEntityNpc>(); break;
            case SubType.MeleeLv1: case SubType.MeleeLv2:  case SubType.MeleeLv3: case SubType.MeleeLv4:
                entity = gameObject.AddComponent<unitEntityNpc2>(); break;
            case SubType.PistolLv1: case SubType.PistolLv2: case SubType.PistolLv3: case SubType.PistolLv4:
                entity = gameObject.AddComponent<unitEntityNpc3>(); break;
            default:
                entity = gameObject.AddComponent<unitEntity>(); break;
        }
    }
}
