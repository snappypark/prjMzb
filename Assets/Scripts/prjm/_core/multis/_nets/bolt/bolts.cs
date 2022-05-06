using UnityEngine;
using System;
using UdpKit;
using System.Collections;
using Bolt.Matchmaking;
using UdpKit.Platform;
using UnityEngine.SceneManagement;
using Bolt;
using Bolt.Photon;

public partial class bolts : Bolt.GlobalEventListener
{
    void Awake() { BoltLauncher.SetUdpPlatform(new PhotonPlatform()); }
    public new void OnEnable() { base.OnEnable(); }

    State _state = State.Shutdown;
    
    [SerializeField] bolts_serverlist _servers;

    public bool IsRunning{get{ return BoltNetwork.IsRunning && _state != State.Shutdown; }}
    
    #region lobby
    public void EnterLobbyAsClient() {
        if (_state == State.Shutdown) { _state = State.ConnectingLobby;
            uis.cover.SetState(ui_cover.State.Connecting);
            BoltLauncher.StartClient();
        }
    }
    public void RefreshServerList() {
        if (_state == State.LobbyAsClient && multis.isLoadedUnits) {
            uis.outgam.Active(ui_outgam.eState.multiList);
            _servers.Active(true);
        }
    }
    #endregion

    public override void BoltStartBegin()  {
        BoltNetwork.RegisterTokenClass<PhotonRoomProperties>();
        BoltNetwork.RegisterTokenClass<boltBos.token>();
        BoltNetwork.RegisterTokenClass<boltMob.token>();
        BoltNetwork.RegisterTokenClass<boltPlyor.token>();
        BoltNetwork.RegisterTokenClass<boltPlyor.tokenConnect>();
        
    }
    public override void BoltStartDone()
    {
        switch (_state) {
            case State.ConnectingLobby: _state = State.LobbyAsClient;
                RefreshServerList();
                break;
            case State.CreatingServer: _state = State.IngameAsServer;
                core.multis.CreateRoom();
                break;
            case State.IngameAsClient:
                Debug.Log("IngameAsClient");
                break;
        }
    }

    public override void BoltStartFailed(UdpConnectionDisconnectReason disconnectReason)
    {
        BoltLog.Error("BoltStartFailed");
    }

    public override void ConnectRequest(UdpEndPoint endpoint, IProtocolToken token)
    {
        //if (BoltNetwork.IsServer)
        //    connectRequest_OnServer(endpoint);
    }

    public override void Connected(BoltConnection cnnt)
    {
        if (BoltNetwork.IsClient)
            _state = State.IngameAsClient;
        else if (BoltNetwork.IsServer)
        {
            Debug.Log("Connected" + cnnt);
            boltPlyor.tokenConnect tk = cnnt.ConnectToken as boltPlyor.tokenConnect;
            BoltEntity enty = core.multis.InstantiatePlyor(tk.name, tk.modxes);
            enty.AssignControl(cnnt);
        }
    }

    public override void ConnectFailed(UdpEndPoint endpoint, IProtocolToken token)
    {
        Debug.Log("ConnectFailed ");
        //uis.It.pop.warning.Active("[ConnectFailed]" + token.ToString());
        // gotoMenu();
        base.ConnectFailed(endpoint, token);
    }

    public override void Disconnected(BoltConnection cnnt)
    {
        core.multis.RemovePlayer(cnnt);
        if (BoltNetwork.IsServer)
        {
         //   uis.It.pop.warning.Active("Disconnected: " + cnnt.ToString());
        }
        else
        {
            /*
            if (full == true)
            {
                full = false;

                PhotonRoomProperties roomProperties = new PhotonRoomProperties
                {
                    IsOpen = true,
                    IsVisible = true
                };
                float t = BoltNetwork.Server.PingNetwork;
                //   BoltMatchmaking.CurrentSession.ConnectionsCurrent
                BoltMatchmaking.UpdateSession(roomProperties);
            }*/
            //uis.It.pop.warning.Active("Disconnected: " + cnnt.ToString());
            //core.Inst.flowMgr.Change<Flow_Menu>();
        }
    }

    public override void EntityAttached(BoltEntity entity)
    {
        if (entity.PrefabId.Value == BoltPrefabs.boltPlyor.Value)
            core.multis.AddPlayer(entity.GetComponent<boltPlyor>());
    }

    public override void EntityDetached(BoltEntity entity)
    {
        if (entity.PrefabId.Value == BoltPrefabs.boltPlyor.Value)
            core.multis.RemovePlayer(entity.GetComponent<boltPlyor>());
    }

    //public override void SessionCreated(UdpSession session) { }
    public enum State {
        Shutdown, Shutdowning,
        ConnectingLobby,
        LobbyAsClient,

        CreatingServer,
        IngameAsClient,
        IngameAsServer, }
}
