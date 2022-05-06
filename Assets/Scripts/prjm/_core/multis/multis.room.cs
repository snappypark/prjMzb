using UnityEngine;
using Bolt.Matchmaking;
using Bolt.Photon;
using UdpKit.Platform.Photon;
using UdpKit.Platform;

public partial class multis //https://doc.photonengine.com/en-US/bolt/current/lobby-and-matchmaking/bolt-matchmaking/
{
    public bool IsPlaying { get { return _state == state.Play; } }
    enum state : byte { Wait, PlayCnt, Play, EndCnt, Result, Load }
    state _state;
    
    enum changeState : byte { None, Begin, Play, End }
    changeState _chgState = changeState.None;
    bool _isPublic, _isDark;
    string _ssName;
    int _rmSeed;

    public void SetRoomInfo(PhotonSession sss)
    {
        SetRoomInfo(sss.HostName, sss.IsVisible, (bool)sss.Properties[k_dark], (int)sss.Properties[k_seed]);
    }

    public void SetRoomInfo(string roomName, bool isPublic_, bool isDark_, int randSeed_)
    {
        _ssName = roomName;
        _isPublic = isPublic_; _isDark = isDark_; _rmSeed = randSeed_;
        _chgState = changeState.None;
        _state = state.Wait;
    }

    public void CreateRoom()
    {
        BoltMatchmaking.CreateSession(sessionID: _ssName, token: getRoomPrp_createRoom());
    }

    public void StartGame()
    {
        BoltMatchmaking.UpdateSession(getRoomPrp_startRoom());
    }

    void reopenRoom()
    {
        PhotonRoomProperties prps = new PhotonRoomProperties();
        BoltMatchmaking.UpdateSession(prps);
    }

    const float cntDur = 7;/*10;*/
    const float playDur = 210;//290;
    const float endDur = 3;/*4*/
    const float scoreDur = 2;/*4*/


    #region PhotonRoomProperties
    public bool IsNotSameModeOrClosed(PhotonSession sss_)
    {
        return _mode != (Mode)sss_.Properties[k_mode] || !sss_.IsOpen;
    }

    PhotonRoomProperties getRoomPrp_createRoom()
    {
        PhotonRoomProperties prps = new PhotonRoomProperties { IsOpen = true, IsVisible = _isPublic, };
        prps[k_mode] = (byte)_mode;
        prps[k_dark] = _isDark;
        prps[k_chgState] = (byte)changeState.Begin;
        prps[k_seed] = _rmSeed;
        return prps;
    }

    PhotonRoomProperties getRoomPrp_startRoom()
    {
        _rmSeed = UnityEngine.Random.Range(10, 99999999/*9*/);
        PhotonRoomProperties prps = new PhotonRoomProperties { IsOpen = false, IsVisible = _isPublic };
        prps[k_seed] = _rmSeed;
        prps[k_chgState] = (byte)changeState.Play;
        prps[k_startTime] = BoltNetwork.ServerTime + cntDur;
        return prps;
    }

    PhotonRoomProperties getRoomPrp_endRoom()
    {
        PhotonRoomProperties prps = new PhotonRoomProperties { IsOpen = true, IsVisible = _isPublic };
        prps[k_chgState] = (byte)changeState.End;
        return prps;
    }


    const string k_mode = "m";
    const string k_dark = "d";
    const string k_seed = "r";
    const string k_chgState = "c";
    const string k_startTime = "st";

    #endregion

    #region string
    public string GetModeName() {
        switch (_mode) {
            case Mode.Escap: return langs.MazeEscape();
            case Mode.Battle: return langs.TeamBattle();  }
        return string.Empty;
    }
    public string GetRoomName()
    {
        if (_isPublic)
            return langs.WhomRoom(_ssName.Substring(0, _ssName.Length - 5));
        return langs.RoomCode(_ssName);
    }
    // Debug.Log("MaxConnections" + BoltNetwork.MaxConnections);
    public string GetModeName_InLobby() {
        switch (_mode) {
            case Mode.Escap: return string.Format("{0} - [ {1} ]", langs.MazeEscape(), synRegion.GetCurName());
            case Mode.Battle: return string.Format("{0} - [ {1} ]", langs.TeamBattle(), synRegion.GetCurName()); }
        return string.Empty;
    }
    #endregion
}
