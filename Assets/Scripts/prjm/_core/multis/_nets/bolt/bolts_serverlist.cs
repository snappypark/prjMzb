using UnityEngine;
using System;
using UdpKit;
using System.Collections.Generic;
using Bolt.Matchmaking;

using UdpKit.Platform.Photon;
public class bolts_serverlist : Bolt.GlobalEventListener
{
    public int Num() { return _rooms.Count; }
    List<UdpSession> _rooms = new List<UdpSession>();
    List<UdpSession> _privates = new List<UdpSession>();

    private new void OnEnable()
    {
        base.OnEnable();
    }

    public void Active(bool active)
    {
        gameObject.SetActive(active);
    }

    public UdpSession GetRoom(int idx)
    {
        return (idx < 0 || idx >= _rooms.Count) ? null : _rooms[idx];
    }

    public string GetSessionName(int idx)
    {
        return idx < _rooms.Count ? _rooms[idx].HostName : "(null)";
    }

    public void RemoveSession(UdpSession session)
    {
        for (int i = 0; i < _rooms.Count; ++i)
            if (_rooms[i].Id == session.Id) {
                _rooms.RemoveAt(i);
                return;
            }
        uis.outgam.multiList.Refresh(_rooms.Count);
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        Debug.Log("Received session list update");

        _rooms.Clear();
        _privates.Clear();
        foreach (var info in sessionList)
        {
            PhotonSession sss = info.Value as PhotonSession;
            if (core.multis.IsNotSameModeOrClosed(sss))
                continue;
            if (sss.IsVisible) _rooms.Add(sss);
            else               _privates.Add(sss);
        }
        uis.outgam.multiList.Refresh(_rooms.Count);
    }


    public UdpSession FindPrivate(string code)
    {
        for(int i=0; i< _privates.Count; ++i)
            if(_privates[i].HostName == code)
                return _privates[i];
        return null;
    }
}
