using UnityEngine;
using UdpKit;
using Bolt.Matchmaking;
using Bolt;

using UdpKit.Platform.Photon;

public partial class bolts
{
    public string GetSessionName(int idx) { return _servers.GetSessionName(idx); }
    public void JoinSession(int idx)
    {
        if (_state == State.LobbyAsClient)
            joinSession(_servers.GetRoom(idx));
    }

    public void JoinSessionPrivate(string code)
    {
        joinSession(_servers.FindPrivate(code));
    }

    void joinSession(UdpSession sss)
    {
        if (sss == null) { uis.pops.Show_Warning("Not Found"); return; }
        BoltMatchmaking.JoinSession(sss, new boltPlyor.tokenConnect { name = dUser.Name, modxes = dCust.CurMdxes_h() });
        uis.outgam.Active(ui_outgam.eState.none, ui_cover.State.Connecting);
        core.multis.ClearPlayers();
        _servers.Active(false);
    }

    public override void SessionConnected(UdpSession sss_, IProtocolToken token)
    {
        Debug.Log("SessionConnected");
        core.multis.SetRoomInfo(sss_ as PhotonSession);
    }

    public override void SessionConnectFailed(UdpSession session, IProtocolToken token, UdpSessionError errorReason)
    {
        _servers.RemoveSession(session);
        switch (errorReason)
        {
            case UdpSessionError.GameFull:
                uis.pops.Show_Warning("TheRoomIsFull");
                break;
            default:
                uis.pops.Show_Warning("GameDoesNotExist");
                break;
        }
        uis.outgam.Active(ui_outgam.eState.multiList, ui_cover.State.None);
    }

    public override void ConnectRefused(UdpEndPoint endpoint, IProtocolToken token)
    {
        Debug.Log("ConnectRefused ");
        base.ConnectRefused(endpoint, token);
    }

    /*
    void onCennected_OnClient()
    {
        // Debug.Log("MaxConnections" + BoltNetwork.MaxConnections);
        Debug.Log("Connected IsClient");
        core.Inst.flowMgr.Change<Flow_MultiSession>();
        uis.It.ingam.Active(ui_ingam.State.multi_wait);
    }
    */
}
