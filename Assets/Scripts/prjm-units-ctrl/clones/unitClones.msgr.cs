using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine;

public partial class unitClones
{
    Queue<msg> _msgr = new Queue<msg>();

    void _awakeMsgr()
    {
        _msgr.Enqueue(new msg(msgType.Basic, -1, 0));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnUpdate_Msgr()
    {
        msg msg = _msgr.Dequeue();
        if (Time.time < msg.time)
        {
            switch (msg.type)
            {
                case msgType.SoloEndTime:
                    smudges.SetFootPrint();
                    break;
                case msgType.SpdX:
                    pPlat.AddForce(Vector3.right, _cObj[msg.dx]);
                    break;
                case msgType.Spd_X:
                    pPlat.AddForce(Vector3.left, _cObj[msg.dx]);
                    break;
                case msgType.SpdZ:
                    pPlat.AddForce(Vector3.forward, _cObj[msg.dx]);
                    break;
                case msgType.Spd_Z:
                    pPlat.AddForce(Vector3.back, _cObj[msg.dx]);
                    break;
            }

            _msgr.Enqueue(msg);
        }
        else
        {
            switch (msg.type)
            {
                case msgType.SoloEndTime:
                    ctrls.Inst.OnEvent(ctrls.evTimeOut);
                    break;

                case msgType.DmgMat:
                    _cObj[msg.dx].SetSkinBack();
                    break;

                case msgType.DmgFire:
                    _cObj[msg.dx].DecHp(14, unit.DecHpType.Fire);
                    break;

                case msgType.Shield:
                    _cObj[msg.dx].hud.SetShield(false);
                    break;

                case msgType.SpdEnd:
                    _cObj[msg.dx].attb.moveSpd = unit.DefaultMoveSpd;
                    break;

                case msgType.SpawnNpc:
                    if (core.zones[msg.dx].SpawnNpc())
                        _msgr.Enqueue(msg);
                    break;
                case msgType.TrigA:
                    zTrigger.Bomb_ByTrig(msg.cell, msg.dx);
                    break;

                case msgType.Meteor:
                    if(sMeteor.ShootMeteors() && zones.Inst.IsCurIdx(msg.dx) && unit0.inst.attb.hp.isNotZero())
                        core.unitClones.EnqMsg(msgType.Meteor, sMeteor.timeGap, msg.dx);
                    break;

                case msgType.Tnt:
                    pTnt.OnExplosion(msg.cell);
                    break;

                case msgType.Basic:
                    _msgr.Enqueue(msg);
                    break;
            }
        }
        
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnqMsg(msgType type, float timeGap, short dx_ = 0, cel1l c_ = null)
    {
        _msgr.Enqueue(new msg(type, Time.time + timeGap, dx_, c_));
    }

    void clearMsgs()
    {
        _msgr.Clear();
        _msgr.Enqueue(new msg(msgType.Basic, -1, 0));
    }

    public struct msg
    {
        public msgType type;
        public float time;
        public short dx;
        public cel1l cell;
        public msg(msgType type_, float time_, short dx_, cel1l c_ = null)
        {
            type = type_;
            time = time_;
            dx = dx_;
            cell = c_;
        }
    }
}

public enum msgType : byte
{
    Basic = 0, SoloEndTime, DmgMat, DmgFire, Shield,
    SpdEnd, SpdX, Spd_X, SpdZ, Spd_Z,
    SpawnNpc,
    TrigA,

    Meteor,
    Tnt,
}
