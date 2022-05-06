using UnityEngine;
using UnityEngineEx;
using UdpKit;
using Bolt.Matchmaking;
using Bolt.Photon;

public partial class bolts
{
    public void CreateServer(bool isPublic, bool isDark)
    {
        if (_state == State.LobbyAsClient) {
            core.multis.ClearPlayers();
            core.multis.SetRoomInfo(makeSessionName(isPublic), isPublic, isDark, RandEx.GetN(10, 999999));
            _servers.Active(false);
            shutdown(State.CreatingServer, ui_cover.State.Connecting);
        }
    }

    public override void SessionCreationFailed(UdpSession session, UdpSessionError errorReason)
    {
        Debug.Log("SessionCreationFailed");
        core.Inst.flowMgr.Change<Flow_MultiLobby>();
        shutdown(State.ConnectingLobby, ui_cover.State.Connecting);

        if (errorReason == UdpSessionError.GameIdAlreadyExists)
            uis.pops.Show_Warning("Server error 6472");
        else
            uis.pops.Show_Warning(errorReason.ToString());
    }
    
    //
    void connectRequest_OnServer(UdpEndPoint endpoint)
    {
        if (full == false)
        {
            full = true;
            PhotonRoomProperties prps = new PhotonRoomProperties {
                IsOpen = false,
                IsVisible = true  };

            BoltMatchmaking.UpdateSession(prps);

            BoltNetwork.Accept(endpoint);
        }
        else
        {
            BoltNetwork.Refuse(endpoint);
        }
    }

    bool full;
    
    string makeSessionName(bool isPublic)
    {
        if(isPublic)
            return string.Format("{0}{1}", dUser.Name, genCode.Alphabet5());
        return genCode.Alphabet5();
    }
}
