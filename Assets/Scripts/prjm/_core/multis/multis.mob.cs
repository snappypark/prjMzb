using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

public partial class multis
{
    List<boltMob> _mobs = new List<boltMob>();
    boltBos _bos;

    public void InstantiateNpcs()
    {
        _mobs.Clear();
        switch (_mode)
        {
            case Mode.Escap:
                for (int i = 0; i < 5; ++i)
                {
                    BoltEntity enty = BoltNetwork.Instantiate(BoltPrefabs.boltMob, new boltMob.token()  {
                        modxes = dCust.GetMobMdxes_h(), }, Vector3.zero, Quaternion.identity);
                    boltMob mob = enty.GetComponent<boltMob>();
                    _mobs.Add(mob);
                    mob.i = i;
                }
                break;
            case Mode.Battle:
                BoltEntity enty2 = BoltNetwork.Instantiate(BoltPrefabs.boltBos, new boltBos.token() {
                    modxes = dCust.GetMobMdxes_h(), }, Vector3.zero, Quaternion.identity);
                enty2.TakeControl();
                _bos = enty2.GetComponent<boltBos>();
                break;
        }


    }

    public void RespawnMobs_ByServer(int zdx)
    {
        if (!BoltNetwork.IsServer || _mode != Mode.Escap)
            return;
        zone zn = core.zones[zdx];
        while (zn.Mobs.Count > 0)
        {
            iff info = zn.Mobs.Dequeue();
            boltMob mob = _mobs[info.i];
            mob.Spawn(new Vector3(info.x, 0, info.z));
            if(!mob.entity.IsControlled)
                mob.entity.TakeControl();
        }
    }

    void respawnBos_ByServer()
    {
        if (!BoltNetwork.IsServer || _mode != Mode.Battle)
            return;
        _bos.Spawn();
        if (!_bos.entity.IsControlled)
            _bos.entity.TakeControl();
    }

    void lostCtrlOfMobs()
    {
        if(BoltNetwork.IsServer)
            for (int i = 0; i < _mobs.Count; ++i)
                _mobs[i].ControlLost();
    }

    void lostCtrlOfBos()
    {
        if (BoltNetwork.IsServer)
        {
            _bos.Spawn();
            _bos.ControlLost();
        }
    }
}
