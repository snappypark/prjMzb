//#define _test_90j_

using CodeStage.AntiCheat.ObscuredTypes;

public static class dStage
{
    public const int LastIdx = 99;
    public static int Idx { get { return _idx.GetValue(); } }
    public static int PlayingIdx { get { return _playingIdx.GetValue(); } }

    static nj.scInt _idx = 0;
    static nj.scInt _playingIdx = 0;
    
    public static void Load()
    {
        _idx.SetValue(ObscuredPrefs.GetInt("xdiegats", 0));

#if _test_90j_
        //_idx.SetValue(99);
        //_idx.SetValue(2);
#endif
    }

    public static void SaveNextLevel()
    {
        if (_playingIdx.GetValue() < _idx.GetValue())
            return;

        if(_idx.GetValue() < LastIdx)
            ++_idx;
        ObscuredPrefs.SetInt("xdiegats", _idx);
        ObscuredPrefs.Save();
    }

    public static void SetPlayingNextIdx()
    {
        if (_playingIdx.GetValue() < LastIdx)
            _playingIdx++;
    }

    public static void SetPlayingIdx()
    {
        _playingIdx.SetValue(_idx.GetValue());
    }

    public static void SetPlayingIdx(int idx)
    {
        _playingIdx.SetValue(idx);
    }
}
