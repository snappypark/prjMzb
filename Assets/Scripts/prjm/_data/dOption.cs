using CodeStage.AntiCheat.ObscuredTypes;

public static class dOption
{
    public static int SoundIdx { get { return _soundIdx; } }
    static int _soundIdx = 3;

    public static void Load()
    {
        _soundIdx = ObscuredPrefs.GetInt("optsoundidx", 3);
        audios.Inst.SetVolume(_soundIdx * 0.333333f);
    }

    public static void ChangeAndSave_SoundIdx()
    {
        _soundIdx = (_soundIdx + 1) % 4;
        audios.Inst.SetVolume(_soundIdx * 0.333333f);
        ObscuredPrefs.SetInt("optsoundidx", _soundIdx);
        ObscuredPrefs.Save();
    }
}
