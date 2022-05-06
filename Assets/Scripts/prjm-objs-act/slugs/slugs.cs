using System.Runtime.CompilerServices;
using UnityEngine;

public class slugs : nj.ObjsQuePool<slugs, slug>
{
    public const byte eBullet = 0, eHit = 1, eMete = 2;

    short[] _numClones = new short[] { 96, 48, 24 };
    protected override short getCapacityOfType(byte type) { return _numClones[type]; }

    // cdx,
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Fire(byte ally, Vector3 pos, Vector3 nV, float speed, int dmg)
    {
        if (_pool[eBullet].IsFull)
            return;

        slug s = Reactive(eBullet, pos);
        s.OnActive(ally, nV, speed, dmg);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void FirePistol(unit u) {
        FirePistol(u, u.model.pistol.muzzle.position, u.tran.forward);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void FirePistol(unit u, Vector3 pos, Vector3 nV)
    {
        if (_pool[eBullet].IsFull)
            return;

        u.model.Ani.SetInteger(model.AniId_nAct, 2);
        u.model.pistol.ps.Play();

        slug s = Reactive(eBullet, pos);
        s.OnActive(u, nV, unit.weapon.speedPistol, unit.weapon.dmgPistol);
        audios.Inst.pistol.PlaySound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void FireRifle(unit u) {
        FireRifle(u, u.model.rifle.muzzle.position, u.tran.forward);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void FireRifle(unit u, Vector3 pos, Vector3 nV)
    {
        if (_pool[eBullet].IsFull)
            return;

        u.model.Ani.SetInteger(model.AniId_nAct, 3);
        u.model.rifle.ps.Play();

        slug s = Reactive(eBullet, pos);
        s.OnActive(u, nV, unit.weapon.speedRifle, unit.weapon.dmgRifle);
        audios.Inst.rifle.PlaySound();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HitByUnit(unit u)
    {
        if (_pool[eHit].IsFull)
            return;
        slug s = Reactive(eHit, Vector3.zero);
        s.OnActive(u);
    }


    public void ShootMeteor(cel1l c, float gapY)
    {
        if (_pool[eMete].IsFull)
            return;
        slug s = Reactive(eMete, c.ct + new Vector3(0, gapY, 0));
        s.OnActive(c);
    }
}
