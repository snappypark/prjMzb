using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

public partial class ctrls
{
    public spawn_ GetSpawn(int idx) { return _spawns[idx]; }
    List<spawn_> _spawns = new List<spawn_>();

    public void AddSpawn(byte ally_, float x_, float y_, float z_,
            float rx_, float ry_, float rz_,
            int numAmmoPistol_, int numAmmoRifle_, int numAmmoBomb_)
    {
        _spawns.Add(new spawn_(ally_, x_, y_, z_, rx_, ry_, rz_, numAmmoPistol_, numAmmoRifle_, numAmmoBomb_));
    }

    public void ClearSpawns()
    {
        _spawns.Clear();
    }

    public void SpawnOnSolo()
    {
        spawnUnit0(State.Stage, unit.Type.Solo, unit.hud_.Type.HpBar, tranCam.OffsetPlay, Vector3.zero, _spawns[0]);
    }

    public void SpawnOnMenu(Vector3 offset1, Vector3 offset2)
    {
        spawnUnit0(State.Menu, unit.Type.None, unit.hud_.Type.None, offset1, offset2,
            new spawn_(0, 116.0f, 0, 131.0f, 0.24253f, 0, -0.97014f, 0, 0, 0));
    }

    public void SpawnOnEdit(float x, float z)
    {
        spawnUnit0(State.Edit, unit.Type.Solo, unit.hud_.Type.None, new Vector3(0, 15.1f, 0), Vector3.zero,
            new spawn_(0, x, 0, z, 0, 0, 1, 0, 0, 0, 300));
        unit0.inst.tran.GetComponent<CapsuleCollider>().enabled = false;
    }

    void spawnUnit0(State state, unit.Type unitType, unit.hud_.Type hudType,
        Vector3 camOffset1, Vector3 camOffset2, spawn_ info)
    {
        unit u = unit0.inst;
        core.units.CloneTranModel(u, (model.eType)dCust.Model, dCust.SkinMat, model.Equip.Rifle,
            dCust.Head, dCust.Body, dCust.Hand, units.eAniCtrlPlayor, unit.TranType.Rigid);

        u.attb.Set(info.ally, 0, false);
        u.SetEquip(model.Equip.Pistol);
        u.Init(unitType);
        setSpawnOptions(state, u, hudType, info);
        cams.Inst.mainTran.SetTarget(u.tran, camOffset1, camOffset2);
    }

    public void SpawnOnMulti(unit u)
    {
        State state = core.multis.GetStateByMultiMode();
        spawn_ info = _spawns[u.attb.ally];
        switch (state)
        {
            case State.Multi_Escape:
                //info.Set(0, 0, 0, new Vector3(0, 11.28f, -5.14f));
                info.Set(0, 0, 0, tranCam.OffsetPlay);
                break;
            case State.Multi_Battle:
                info.Set(50, 100, 3, tranCam.OffsetPlay);
                break;
            default:
                return;
        }

        setSpawnOptions(state, u, unit.hud_.Type.HpBar, info);
        cams.Inst.mainTran.SetTarget(u.tran, info.offsetCam);
        core.zones.OnUpdate_NextZone(u.cell.zn, false);
        core.collis.Clear();
    }

    void setSpawnOptions(State state, unit u, unit.hud_.Type hudType, spawn_ info)
    {
        SetState(state, u);
        u.attb.moveSpd = info.MoveSpd;
        u.FillHpAndWeapon(info.numAmmoPistol, info.numAmmoRifle, info.numBomb);
        u.SetPosDir(info.pos, info.dir.IfZeroReturnForward());
        u.SetCell();
        u.hud.Set(hudType);
        u.gameObject.SetActive(true);

        uis.ingam.RefreshAllWeaponNum(u.wp);
    }

    public class spawn_
    {
        [SerializeField] public byte ally = 0;
        [HideInInspector] public Vector3 pos;
        [SerializeField] public Vector3 dir = new Vector3(0, 0, 1);

        [SerializeField] public int numAmmoPistol = 10;
        [SerializeField] public int numAmmoRifle = 30;
        [SerializeField] public int numBomb = 1;

        [HideInInspector] public float MoveSpd;
        [HideInInspector] public int ZoneIdx;

        public spawn_(byte ally_, float x_, float y_, float z_,
            float rx_, float ry_, float rz_,
            int numAmmoPistol_, int numAmmoRifle_, int numAmmoBomb_,
            float moveSpd_ = unit.DefaultMoveSpd)
        {
            ally = ally_;
            pos = new Vector3(x_, y_, z_);
            dir = new Vector3(rx_, ry_, rz_);
            numAmmoPistol = numAmmoPistol_;
            numAmmoRifle = numAmmoRifle_;
            numBomb = numAmmoBomb_;

            MoveSpd = moveSpd_;
            ZoneIdx = core.zells[cel1l.Pt(pos)].zn.idx;
        }

        public void Set(int nPistol, int nRifle, int nBomb, Vector3 offsetCam_)
        {
            numAmmoPistol = nPistol;
            numAmmoRifle = nRifle;
            numBomb = nBomb;
            offsetCam = offsetCam_;
        }

        [HideInInspector] public Vector3 offsetCam;
    }
    
    public void Release()
    {
        SetState(State.None, _o);
    }
}
