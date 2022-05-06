using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine;

public partial class unit
{
    public enum DecHpType { Default, Fire }

    public const float DefaultMoveSpd = 58;
    public const float FastMoveSpd = 80;


    public void DecHp(int amount, DecHpType dectype = DecHpType.Default ,unit causer = null)
    {
        if (attb.hp.isZero())return;

        switch (_type) {
            case Type.None:
            case Type.MultiBos:
                return;
            case Type.Solo:
            case Type.MultiPlyor:
                attb.hp.decClamp(amount);
                hud.SetHpBar(attb.hp.Ratio01());
                break;
            default:
                attb.hp.decClamp(amount);
                break;
        }

        ResetHitEffect_ByWeapon();

        switch (dectype) {
            case DecHpType.Fire:
                gjs.effs.Play(effs.fire, tran);
                break;
            default:
                audios.Inst.PlaySound(audios.eSoundType.lifeImpact);
                break;
        }


        if (attb.hp.isNotZero())return;
        //if (causer != null)/*---*/ causer.fsm.OnHp0ing();

        entity.OnHp0();
    }

    public void IncHp(int amount, unit causer = null)
    {
        if (attb.hp.isFull())return;
        attb.hp.incClamp(amount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool checkHp0_SameAlly(byte ally)
    {
        return attb.ally == ally || attb.hp.isZero();
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool checkHp0_SameCdx(short cdx_)
    {
        return cdx == cdx_ || attb.hp.isZero();
    }

    public class attrib_
    {
        public string name;
        public byte ally;
        public float moveSpd;
        public bool isNpc;
        public Queue<short> tdxs = new Queue<short>(); // targets
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(byte ally_, short cdx, bool isNpc_)
        {
            core.unitClones.ally[ally_].Enqueue(cdx);
            ally = ally_;
            isNpc = isNpc_;

            spawnDelay.Reset();

            nj.idxArr _mdx = new nj.idxArr(ally, 6);
            for (int i = 0; i < 5; ++i)
            {
                _mdx.Roll();

                int num = core.unitClones.ally[_mdx.idx].Count;
                for (int t = 0; t < num; ++t)
                {
                    unit unit = core.unitClones.ally.RollTryUnit(_mdx.idx);
                    if (unit.attb.isNpc)
                        unit.attb.tdxs.Enqueue(cdx);
                    if (isNpc_)
                        tdxs.Enqueue(unit.cdx);
                }
            }
        }

        public byte modelIdx, skinIdx, headIdx, bodyIdx, meleeIdx, aniCtrlIdx;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetModelIdx(byte modelIdx_, byte skinIdx_, byte headIdx_, byte bodyIdx_, byte meleeIdx_, byte aniCtrlIdx_)
        {
            modelIdx = modelIdx_;
            skinIdx = skinIdx_;
            headIdx = headIdx_;
            bodyIdx = bodyIdx_;
            meleeIdx = meleeIdx_;
            aniCtrlIdx = aniCtrlIdx_;
        }

        // Passive
        public unit RollTryUnit { get {
                short tdx = tdxs.Dequeue();
                tdxs.Enqueue(tdx);
                return core.unitClones[tdx];
                } }

        public delay spawnDelay = new delay(0.5f);
        public iM hp = new iM(0, 100);

        public delay dlyDmgMat = new delay(0.11f);
        public delay dlyShield = new delay(8.4f);

        public delay dlyDmgFire = new delay(0.6f);

        public delay dlySpd = new delay(pPlat.duration);
    }
}
