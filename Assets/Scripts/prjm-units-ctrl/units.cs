using UnityEngine;
using UnityEngineEx;

public partial class units : nj.Objs<units, unit>
{
    [SerializeField] public matUnits mats;
    public const byte eNpc = 0, eUpc = 1;
    [SerializeField] heads _heads;
    [SerializeField] equips _equips;
    [SerializeField] aniControllers _aniControllers;

    protected override void _awake()
    {
        base._awake();
        model.InitAniIDs();
    }

    public unit CloneRoot(Transform root, byte type)
    {
        unit u = CloneObj(type, root);
        u.transform.localPosition = Vector3.zero;
        return u;
    }

    public unit CloneRootTranModel(Transform root, byte type, 
        model.eType bodyType, byte mat, model.Equip equipType,
        byte headIdx, byte bodyIdx, byte meleeIdx, byte aniCtrlIdx, unit.TranType tranType)
    {
        unit u = CloneObj(type, root);
        u.transform.localPosition = Vector3.zero;
        return CloneTranModel(u, bodyType, mat, equipType, headIdx, bodyIdx, meleeIdx, aniCtrlIdx, tranType);
    }
    
    public unit CloneTranModel(unit u, model.eType modelType, byte mat, model.Equip equipType,
        byte headIdx, byte bodyIdx, byte meleeIdx, byte aniCtrlIdx, unit.TranType tranType)
    {
        if(u.tran != null)
            GameObject.DestroyImmediate(u.tran.gameObject);
        u.tran = createTrans(tranType, u.transform);
        CloneModel(u, modelType, mat, equipType, headIdx, bodyIdx, meleeIdx, aniCtrlIdx);
        return u;
    }

    public void CloneModel(unit u, model.eType modelType, byte iSkinMat, model.Equip equipType,
        byte headIdx, byte bodyIdx, byte meleeIdx, byte aniCtrlIdx)
    {
        model md = createBody(u.tran, modelType, bodyIdx);
        createHead(md, modelType, headIdx);
        createHand(md, equipType, meleeIdx);

        md.SetSkin(iSkinMat);
        md.Ani.runtimeAnimatorController = _aniControllers[aniCtrlIdx];

        u.model = md;
        u.attb.SetModelIdx((byte)modelType, iSkinMat, headIdx, bodyIdx, meleeIdx, aniCtrlIdx);
    }

    public void ChangeModel(unit unit, byte iModel, byte iSkinMat, byte iHead, byte iBody, byte iMeleeIdx, model.Equip equip)
    {
        if (unit.attb.modelIdx != iModel || unit.attb.bodyIdx != iBody)
        {
            GameObject.DestroyImmediate(unit.model.gameObject);
            unit.model = createBody(unit.tran, (model.eType)iModel, iBody);
            createHead(unit.model, (model.eType)iModel, iHead);
            createHand(unit.model, global::model.Equip.Rifle, iMeleeIdx);
        }
        else
        {
            if (unit.attb.headIdx != iHead)
            {
                GameObject.DestroyImmediate(unit.model.head.gameObject);
                createHead(unit.model, (model.eType)iModel, iHead);
            }
            if (unit.attb.meleeIdx != iMeleeIdx)
            {
                GameObject.DestroyImmediate(unit.model.melee.gameObject);
                createMelee(unit.model, iMeleeIdx);
            }
        }

        unit.model.SetSkin(iSkinMat);
        unit.SetEquip(equip);
        unit.attb.SetModelIdx(iModel, iSkinMat, iHead, iBody, iMeleeIdx, unit.attb.aniCtrlIdx);
        unit.model.Ani.runtimeAnimatorController = _aniControllers[unit.attb.aniCtrlIdx];

    }

    Transform createTrans(unit.TranType tranType, Transform parent)
    {
        switch (tranType)
        {
            case unit.TranType.Default:
                return CreateGameObject.TranRes("prefabs/bodys/tran", parent);
            case unit.TranType.Rigid:
                return CreateGameObject.TranRes("prefabs/bodys/rigid", parent);
        }
        return null;
    }

    model createBody(Transform tran, model.eType modelType, byte bodyIdx )
    {
        switch (modelType) {
            case model.eType.Female:
                return CreateGameObject.WithRes<model>(
                    string.Format("prefabs/bodys/females/{0}", bodyIdx), tran);
            case model.eType.Male:
                return CreateGameObject.WithRes<model>(
                    string.Format("prefabs/bodys/males/{0}", bodyIdx), tran);
            case model.eType.Zombie_Female:
                return CreateGameObject.WithRes<model>(
                    string.Format("prefabs/bodys/zombie_females/{0}", bodyIdx), tran);
            case model.eType.Zombie_Male:
                return CreateGameObject.WithRes<model>(
                    string.Format("prefabs/bodys/zombie_males/{0}", bodyIdx), tran);
        }
        return null;
    }

    void createHead(model md, model.eType modelType, byte headIdx)
    {
        switch (modelType)
        {
            case model.eType.Female:
                md.head = _heads.famels.Clone(headIdx, md.tranHead);
                return;
            case model.eType.Male:
                md.head = _heads.mans.Clone(headIdx, md.tranHead);
                return;
            case model.eType.Zombie_Female:
                md.head = _heads.zombie_famels.Clone(headIdx, md.tranHead);
                return;
            case model.eType.Zombie_Male:
                md.head = _heads.zombie_mans.Clone(headIdx, md.tranHead);
                return;
        }
    }

    void createHand(model md, model.Equip equipType, byte meleeIdx)
    {
        md.melee = _equips.Melees.Clone(meleeIdx, md.tranHand).gameObject;
        if (equipType == global::model.Equip.Pistol || equipType == global::model.Equip.Rifle)
        {
            md.pistol = _equips.Guns.CloneWithInactive(0, md.tranHand).GetComponent<tranGun>();
            md.rifle = _equips.Guns.CloneWithInactive(1, md.tranHand).GetComponent<tranGun>();
            md.bomb = _equips.Guns.CloneWithInactive(2, md.tranHand).gameObject;
        }
    }

    void createMelee(model md, byte meleeIdx)
    {
        md.melee = _equips.Melees.Clone(meleeIdx, md.tranHand).gameObject;
    }
}
