using UnityEngine;

public partial class js
{
    public npc AddNpc(zone_ zn, 
        Vector3 pos, Vector3 dir, 
        model.eType model, byte matUnit, model.Equip equip,
        byte ally, byte iHead, byte iBody, byte iMelee, byte aniCtrlIdx, unit.SubType subType, SeriType seriType)
    {
        zn.npcs.Add(new js.npc_(pos, dir.normalized,
            model, (matUnits.Type)matUnit, equip,
            ally, iHead, iBody, iMelee, aniCtrlIdx, subType));

        if (seriType == SeriType.WithLoad)
        {
            npc npc = core.unitClones.PreLoadNpc(zn.zn, pos, dir.normalized,
                model, matUnit, equip,
                ally, iHead, iBody, iMelee, aniCtrlIdx, unit.SubType.None);
            npc.gameObject.SetActive(true);

            zn.npcs[zn.npcs.Count - 1].tmpCdx = npc.cdx;
            return npc;
        }
        return null;
    }

    public void RemoveNpc(zone_ zn, Vector3 pos)
    {
        int idx = findIdxNpc(zn, pos);
        if (idx > -1)
        {
            core.unitClones[zn.npcs[idx].tmpCdx].gameObject.SetActive(false);
            zn.npcs.RemoveAt(idx);
        }
    }

    int findIdxNpc(zone_ zn, Vector3 pos)
    {
        for (int i = 0; i < zn.npcs.Count; ++i)
        {
            if ((zn.npcs[i].pos - pos).sqrMagnitude < 0.25f)
                return i;
        }
        return -1;
    }

    [System.Serializable]
    public class npc_
    {
        [HideInInspector] public Vector3 pos;
        [SerializeField] public byte ally = 1;

        // rand
        [SerializeField] public Vector3 dir = Vector3.forward;
        [HideInInspector] public model.eType modelType;
        [HideInInspector] public matUnits.Type matType = matUnits.Type.Zomb0;
        [HideInInspector] public model.Equip equipType = model.Equip.Melee;
        [HideInInspector] public byte headIdx, bodyIdx, meleeIdx;

        [SerializeField] public byte aniCtrlIdx = units.eAniCtrlNpcZombie;
        [HideInInspector] public unit.SubType subType;
        
        public npc_(Vector3 pos_, Vector3 dir_, 
            model.eType model_, matUnits.Type matType_, model.Equip equip_,
        byte ally_, byte iHead_, byte iBody_, byte iMelee_, byte aniCtrlIdx_, unit.SubType subType_)
        {
            pos = pos_;
            dir = dir_;
            modelType = model_;
            matType = matType_;
            equipType = equip_;
            ally = ally_;
            headIdx = iHead_;
            bodyIdx = iBody_;
            meleeIdx = iMelee_;
            aniCtrlIdx = aniCtrlIdx_;
            subType = subType_;
        }

        public short tmpCdx;
    }

}
