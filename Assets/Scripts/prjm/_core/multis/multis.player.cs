using System.Collections.Generic;
using UnityEngine;
using Bolt;

public partial class multis
{
    List<boltPlyor> _plyors = new List<boltPlyor>();

    public BoltEntity InstantiatePlyor(string name_, int modxes_)
    {
        BoltEntity enty = BoltNetwork.Instantiate(BoltPrefabs.boltPlyor, new boltPlyor.token(){
            ally = getNextAlly(), modxes = modxes_, name = name_ }, Vector3.zero, Quaternion.identity);
        return enty;
    }

    
    public void AddPlayer(boltPlyor plyr)
    {
        if (!_plyors.Contains(plyr))
            _plyors.Add(plyr);
        sortPlayers();
        uis.ingam.multi.Refresh_RelatedPlayers(ref _plyors, _mode);
    }

    public void RemovePlayer(BoltConnection cnnt)
    {
        for (int i = 0; i < _plyors.Count; ++i)
        {
            if (_plyors[i].entity.Controller == cnnt)
            {
                _plyors.RemoveAt(i);
                break;
            }
        }
        sortPlayers();
        uis.ingam.multi.Refresh_RelatedPlayers(ref _plyors, _mode);
    }

    public void RemovePlayer(boltPlyor plyr)
    {
        if (_plyors.Contains(plyr))
            _plyors.Remove(plyr);
        sortPlayers();
        uis.ingam.multi.Refresh_RelatedPlayers(ref _plyors, _mode);
    }

    public void UpdatePlayers()
    {
        sortPlayers();
        uis.ingam.multi.Refresh_RelatedPlayers(ref _plyors, _mode);
    }

    public void ClearPlayers()
    {
        _plyors.Clear();
    }


    void sortPlayers()
    {
        switch (_mode) {
            case Mode.Escap:
                _plyors.Sort((p1, p2) => {
                    int i1 = p1.state.zdx;
                    int i2 = p2.state.zdx;
                    if (i1 == i2)
                    {
                        i1 = -(int)(p1.state.endTime * 100);
                        i2 = -(int)(p2.state.endTime * 100);
                    }
                    return i2 - i1;      } );
                break;
            case Mode.Battle:
                _plyors.Sort((p1, p2) => {
                    int i1 = p1.state.score;
                    int i2 = p2.state.score;
                    return i2 - i1;      } );
                break;
        }
    }
    
    byte getNextAlly()
    {
        switch (_mode)
        {
            case Mode.Battle:
                int ally0s = 0; int ally1s = 0;
                for (int i = 1; i < 9; ++i)
                {
                    unit unit = core.unitClones[i];
                    if (unit.tran == null)
                        continue;
                    switch (unit.attb.ally)
                    {
                        case 0:
                            ++ally0s;
                            break;
                        case 1:
                            ++ally1s;
                            break;
                    }
                }
                return ally0s > ally1s ? (byte)1 : (byte)0;
            default: // stages.eMode.EscapMode:
                return 0;
        }
    }

    //foreach (var entity in BoltNetwork.Entities)
    //if (entity.StateIs<ILobbyPlayerInfoState>() == false || entity.IsController(connection) == false) continue;
    //entity.GetComponent<LobbyPlayer>();
    //.RemovePlayer();





    public int GetBestZn_afterSort()
    {
        return _plyors.Count == 0 ? 0 : _plyors[0].state.zdx;
    }
}
