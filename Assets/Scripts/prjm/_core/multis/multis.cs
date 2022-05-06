using System.Collections;
using UnityEngine;
using UdpKit.Platform.Photon;
using UdpKit.Platform;
using Bolt.Matchmaking;

public partial class multis : nj.MonoSingleton<multis>
{
    [SerializeField] txts_[] _txts;

    public static bool isLoadedUnits = false;
    public static bool isLoadedMaps = false;
    Mode _mode = Mode.None;
    public enum Mode : byte { Escap = 0, Battle, None }

    public void StartMulti(Mode mode_)
    {
        this.enabled = true;
        synRegion.UpdateRegion();
        _mode = mode_;
        isLoadedUnits = false; isLoadedMaps = false;
        nets.Inst.bolts.EnterLobbyAsClient();
        core.Inst.flowMgr.Change<Flow_MultiLobby>();
    }

    public void EndMulti()
    {
        this.enabled = false;
        isLoadedUnits = false; isLoadedMaps = false;
        nets.Inst.bolts.LeaveLobby();
    }

    PhotonSession _sss = null;
    delay _dlyUpdate = new delay(1.717f);
    delay _dlyNextState = new delay(0.0f, false);
    void Update()
    {
        if (_dlyUpdate.IsEndAndReset() && BoltNetwork.IsRunning)
        {
            _sss = BoltMatchmaking.CurrentSession as PhotonSession;
            if (_sss == null)
                return;

            changeState chgState = (changeState)_sss.Properties[k_chgState];
            if (_chgState != chgState)
            {
                if (_chgState == changeState.None)
                    setState_OnEscape(state.Load);
                else {
                    switch (_mode){
                        case Mode.Escap: onRoomState_OnEscape(chgState);  break;
                        case Mode.Battle: onRoomState_OnBattle(chgState); break;  }
                }
                _chgState = chgState;
            }

            switch (_state)
            {
                case state.PlayCnt:
                    if (_dlyNextState.afterOnceEnd())
                        setState(state.Play);
                    break;
                case state.Play:
                    switch (_mode)  {
                        case Mode.Escap: onPlay_OnEscape(); break;
                        case Mode.Battle: onPlay_OnBattle(); break; }
                    if (_dlyNextState.afterOnceEnd() && BoltNetwork.IsServer)
                        BoltMatchmaking.UpdateSession(getRoomPrp_endRoom());
                    break;
                case state.EndCnt:
                    if (_dlyNextState.afterOnceEnd())
                        setState(state.Result);
                    break;
                case state.Result:
                    if (_dlyNextState.afterOnceEnd())
                        setState(state.Load);
                    break;
                case state.Load:
                    if (_dlyNextState.afterOnceEnd())
                        setState(state.Wait);
                    break;
            }
        }

    }

    void setState(state state_) {
        switch (_mode) {
            case Mode.Escap: setState_OnEscape(state_); break;
            case Mode.Battle: setState_OnBattle(state_); break; }
    }

    public IEnumerator StartMultiLobby_()
    {
        core.loads.ResetBgType();
        switch (_mode) {
            case Mode.Escap:  core.unitClones.PreLoadUnits_Escape(); break;
            case Mode.Battle: core.unitClones.PreLoadUnits_Battle(); break; }

        unit0.Inactive();
        isLoadedUnits = true;
        yield return null;
        nets.Inst.bolts.RefreshServerList();
    }

    public IEnumerator StartMultiSession_(bool first)
    {
        int mapIdx = _rmSeed % 4;
        core.loads.SetRandSeed(_rmSeed);
        loads.TypeBg bgtype = _isDark ? loads.TypeBg.Dark2 : loads.TypeBg.ByJs;
        yield return core.loads.FromJson_(_txts[(int)_mode].GetJson(mapIdx), bgtype, false);
        switch (_mode)
        {
            case Mode.Escap:
                for (int i = 1; i < core.zones.Num; ++i)
                    for (int j = 0; j < core.zones[i].mzs.Count; ++j)
                        yield return core.loads.FromRandMaze_Escape_(i, j, mapIdx);
                //i4 bd = core.zones[0].bd.i4; // tmp
                //core.zones[0].AddPrp(bd.X0 + 1, bd.Z1 - 1, zprps.eArea, cel1l.Type.AreaWin, f4.O);

                break;
            case Mode.Battle:
                for (int i = 1; i < core.zones.Num - 2; ++i)
                    for (int j = 0; j < core.zones[i].mzs.Count; ++j)
                        yield return core.loads.FromRandMaze_Battle_(i, j);
                break;
        }

        if (BoltNetwork.IsServer)
        {
            switch (_mode)
            {
                case Mode.Escap:
                    for (int i = 1; i < core.zones.Num; ++i)
                        for (int j = 0; j < core.zones[i].mzs.Count; ++j)
                            yield return core.loads.Mob_MultiEscape_(i, j, first);
                    break;
                case Mode.Battle:
                    core.loads.Bos_MultiBattle(core.zones[2]);
                    break;
            }
            if (first)
            {
                BoltEntity enty = core.multis.InstantiatePlyor(dUser.Name, dCust.CurMdxes_h());
                enty.TakeControl();
                core.multis.InstantiateNpcs();
            }
            else
                ctrls.Inst.SpawnOnMulti(ctrls.Unit);

        }

        core.collis.Clear();

        core.loads.SetBgsFromJson();

        yield return core.zones.InitJsps_();
        isLoadedMaps = true;
    }
    

    public ctrls.State GetStateByMultiMode()  {
        switch (_mode) {
            case Mode.Escap:  return ctrls.State.Multi_Escape;
            case Mode.Battle: return ctrls.State.Multi_Battle;
            default:          return ctrls.State.None; } }
}


//https://doc.photonengine.com/en-US/bolt/current/connection-and-authentication/regions/
public class synRegion
{
    public static int CurIdx = 0;
    public static string GetCurName() { return names[CurIdx]; }

    public static string[] names = new string[] {
        "Best Region",

        "Asia",
        "Australia",
        "Europe",
        "India",
        "Japan",

        "Russia",
        "South America",
        "South Korea",
        "USA,East",
        "USA,West",
    };

    public static PhotonRegion.Regions[] regions = new PhotonRegion.Regions[]
    {
        PhotonRegion.Regions.BEST_REGION,

        PhotonRegion.Regions.ASIA,
        PhotonRegion.Regions.AU,
        PhotonRegion.Regions.EU,
        PhotonRegion.Regions.IN,
        PhotonRegion.Regions.JP,

        PhotonRegion.Regions.RU,
        PhotonRegion.Regions.SA,
        PhotonRegion.Regions.KR,
        PhotonRegion.Regions.US,
        PhotonRegion.Regions.USW,
    };

    public static void UpdateRegion()
    {
        if (BoltNetwork.IsRunning == false)
        {
            // Get the current Region based on the index
            PhotonRegion targetRegion = PhotonRegion.GetRegion(regions[CurIdx]);

            // Update the target region
            BoltRuntimeSettings.instance.UpdateBestRegion(targetRegion);

            // Log the update
         //   Debug.LogFormat("Update region to {0}", targetRegion.Name);

            // IMPORTANT
            // Initialize the Photon Platform again
            // this will update the internal cached region
            BoltLauncher.SetUdpPlatform(new PhotonPlatform());
        }
        else
        {
            BoltLog.Error("Bolt is running, you can't change region while runnning");
        }
    }
}
