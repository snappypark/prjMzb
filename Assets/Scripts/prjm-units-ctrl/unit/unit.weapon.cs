using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngineEx;

public partial class unit
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetEquip(model.Equip equip_)
    {
        //if (wp.state == equip_) /*----*/ return;
        wp.state = equip_;
        model.OnSetEquip(equip_);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CheckAndUseEquip(float curTime)
    {
        switch (wp.state)
        {
            case model.Equip.Melee:
                if (wp.dlyMelee.IsEndAndReset(curTime) &&
                    !model.Ani.GetCurrentAnimatorStateInfo(0).IsName("melee") )
                {
                    model.Ani.SetInteger(model.AniId_nAct, 1);
                    abjs.slugs.HitByUnit(this);
                }
                break;
            case model.Equip.Pistol:
                if (wp.EnablePistol(curTime))
                {
                    wp.AmmoPistol.dec();
                    abjs.slugs.FirePistol(this);
                }
                break;
            case model.Equip.Rifle:
                if (wp.EnableRifle(curTime))
                {
                    wp.AmmoRifle.dec();
                    abjs.slugs.FireRifle(this);
                }
                break;
            case model.Equip.Bomb:
                if (wp.EnableBomb(curTime))
                {
                    wp.Bomb.dec();
                    model.Ani.SetInteger(model.AniId_nAct, 4);
                    abjs.boms.Throw(tran.localPosition.WithGapY(0.47f));

                    audios.Inst.PlaySound(audios.eSoundType.grab);
                }
                break;
        }
    }

    public bool CheckEquip(model.Equip equip, float curTime)
    {
        switch (equip) {
            case model.Equip.Melee:
                if (wp.dlyMelee.IsEndAndReset(curTime) &&
                    !model.Ani.GetCurrentAnimatorStateInfo(0).IsName("melee"))
                    return true;
                break;
            case model.Equip.Pistol:
                if (wp.EnablePistol(curTime))
                    return true;
                break;
            case model.Equip.Rifle:
                if (wp.EnableRifle(curTime))
                    return true;
                break;
            case model.Equip.Bomb:
                if (wp.EnableBomb(curTime))
                    return true;
                break;
        }
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void UseEquip(model.Equip equip, float curTime)
    {
        switch (equip)
        {
            case model.Equip.Melee:
                model.Ani.SetInteger(model.AniId_nAct, 1);
                abjs.slugs.HitByUnit(this);
                break;
            case model.Equip.Pistol:
                wp.AmmoPistol.dec();
                abjs.slugs.FirePistol(this);
                break;
            case model.Equip.Rifle:
                wp.AmmoRifle.dec();
                abjs.slugs.FireRifle(this);
                break;
            case model.Equip.Bomb:
                wp.Bomb.dec();
                model.Ani.SetInteger(model.AniId_nAct, 4);
                abjs.boms.Throw(tran.localPosition.WithGapY(0.47f));

                audios.Inst.PlaySound(audios.eSoundType.grab);
                break;
        }
    }

    public bool TryMeleeAtack(unit target, float spdAttackScale)
    {
        if (null == target)
            return false;
        switch (wp.state)
        {
            case model.Equip.Melee:
                //set close dist
                if (target.IsDetected( tran.localPosition + tran.forward * 0.5f, dist.sqr0_74))
                {
                    wp.dlyMelee.Reset(spdAttackScale);
                    return true;
                }
                return false;
            case model.Equip.Bomb:
                //set close dist
                // detect condition
                return false;
            case model.Equip.Rifle:
            case model.Equip.Pistol:
                //set close dist
                // detect condition
                return false;
        }
        return false;
    }

    public class weapon
    {
        public model.Equip state = model.Equip.None;

        public iM AmmoPistol = new iM(910250);
        public iM AmmoRifle = new iM(910500);
        public iM Bomb = new iM(91020);

        public delay dlyMelee = new delay(0.888666666f);
        public delay dlyPistol = new delay(0.39f);
        public delay dlyRifle = new delay(0.19f);
        public delay dlyBomb = new delay(1.4f);
        public delay dlyAttEff = new delay(0.11f);

        public const float speedPistol = 32.0f;
        public const float speedRifle = 38.0f;
        public const int dmgPistol = 8;
        public const int dmgRifle = 13;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fill(int amPistol, int amRifle, int nBomb)
        {
            AmmoPistol.Set(amPistol);
            AmmoRifle.Set(amRifle);
            Bomb.Set(nBomb);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EnablePistol(float curTime)
        {
            return AmmoPistol.isNotZero() && dlyPistol.IsEndAndReset(curTime);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EnableRifle(float curTime)
        {
            return AmmoRifle.isNotZero() && dlyRifle.IsEndAndReset(curTime);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool EnableBomb(float curTime)
        {
            return Bomb.isNotZero() && dlyBomb.IsEndAndReset(curTime);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ResetShield()
    {
        attb.dlyShield.Reset();
        hud.SetShield(true);
        core.unitClones.EnqMsg(msgType.Shield, attb.dlyShield.duration, cdx);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ResetHitEffect_ByWeapon()
    {
        attb.dlyDmgMat.Reset();
        model.SetSkin(matUnits.OnDmg);
        core.unitClones.EnqMsg(msgType.DmgMat, attb.dlyDmgMat.duration, cdx);
    }
}
