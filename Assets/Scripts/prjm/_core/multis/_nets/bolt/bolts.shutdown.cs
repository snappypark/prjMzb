using UnityEngine;
using System;
using UdpKit;
using System.Collections;
using Bolt.Matchmaking;
using UdpKit.Platform;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Bolt;
using Bolt.Photon;

public partial class bolts
{
    public void LeaveLobby()
    {
        if (_state == State.LobbyAsClient) {
            core.Inst.flowMgr.Change<Flow_Menu>();
            shutdown(State.Shutdowning, ui_cover.State.Disconnecting);
        }
    }
    
    public void LeaveGame()
    {
        if (_state == State.IngameAsClient || _state == State.IngameAsServer) {
            core.Inst.flowMgr.Change<Flow_MultiLobby>();
            shutdown(State.ConnectingLobby, ui_cover.State.Connecting);
        }
    }

    void shutdown(State state, ui_cover.State uiState)
    {
        _state = state;
        uis.outgam.Active(ui_outgam.eState.none, uiState);
        BoltLauncher.Shutdown();
    }

    public override void BoltShutdownBegin(Bolt.AddCallback doneCallback, UdpConnectionDisconnectReason disconnectReason)
    {   //Debug.Log("BoltShutdownBegin: " + disconnectReason +", " + _state);
        unit0.Inactive();
        switch (_state) {
            case State.ConnectingLobby:
                doneCallback(() => { StartCoroutine(enterClient_()); });
                break;
            case State.CreatingServer:
                doneCallback(() => { StartCoroutine(enterServer_()); });
                break;
            case State.Shutdowning:
                doneCallback(() => {
                    _state = State.Shutdown;
                    uis.outgam.Active(ui_outgam.eState.menu, ui_cover.State.Menu);
                });
                break;
            default:
                switch (disconnectReason)
                {
                    case UdpConnectionDisconnectReason.Disconnected:
                        _state = State.ConnectingLobby;
                        doneCallback(() => { StartCoroutine(enterClient_()); });
                        core.Inst.flowMgr.Change<Flow_MultiLobby>(() => {
                            uis.pops.Show_Warning("disconncected from server.");
                        });
                        break;
                    case UdpConnectionDisconnectReason.Unknown:
                    case UdpConnectionDisconnectReason.Timeout:
                    case UdpConnectionDisconnectReason.Error:
                    case UdpConnectionDisconnectReason.MaxCCUReached:
                        _state = State.Shutdown;
                        core.Inst.flowMgr.Change<Flow_Menu>(() => {
                            uis.outgam.Active(ui_outgam.eState.menu, ui_cover.State.Menu);
                            uis.pops.Show_Warning("Network " + disconnectReason.ToString());
                        });
                        break;
                }
                break;
        }
    }

    IEnumerator enterClient_()
    {
        WaitForSeconds wait = new WaitForSeconds(1.7f);
        yield return wait;
        yield return null; yield return null;
        uis.cover.SetState(ui_cover.State.Connecting);
        BoltLauncher.StartClient();
    }

    IEnumerator enterServer_()
    {
        yield return null; yield return null; yield return null; yield return null;
        yield return null; yield return null; yield return null; yield return null;
        BoltLauncher.StartServer();
    }
}
