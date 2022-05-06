using System.Runtime.CompilerServices;
using UnityEngine;

public class audios : MonoBehaviour
{
    public static audios Inst;
    public enum eMusicType
    {
        outgame = 0,
        maze,
        rock1,
        dada,
    }

    public enum eSoundType
    {
        victory = 0,
        lose,
        grab,
        key,
        lifeImpact,
        reload,
        burned,
        wind,
        elect,
        brek,
    }

    [SerializeField] AudioClip[] _musics;
    [SerializeField] AudioSource _musicSource;

    [SerializeField] AudioClip[] _sounds;
    [SerializeField] AudioSource[] _soundSources;

    [SerializeField] public audio pistol;
    [SerializeField] public audio rifle;
    [SerializeField] public audio bomb;
    [SerializeField] public audio step;

    bool _mute = false;

    void Awake()
    {
        Inst = this;
    }

    public bool IsMute { get { return _mute; } }
    public void Mute()
    {
        _mute = true;
        for (int i = 0; i < _soundSources.Length; ++i)
            _soundSources[i].mute = true;
    }
    public void UnMute()
    {
        _mute = false;
        for (int i = 0; i < _soundSources.Length; ++i)
            _soundSources[i].mute = false;
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        _musicSource.UnPause();
    }

    public void PlayMusic(eMusicType type)
    {
        _musicSource.clip = _musics[(int)type];
        _musicSource.loop = false;
        _musicSource.Play();
    }

    public void SetNeedToPlayDefaultMusic()
    {
        _needToPlayMusic = true;
    }

    bool _needToPlayMusic = true;

    public void PlayMazeMusic(eMusicType music)
    {
        if (_needToPlayMusic)
        {
            PlayMusic(music);
            _needToPlayMusic = false;
        }
    }

    const float de = 0.043f;

    delay _delaySound0 = new delay(de);
    delay[] _delaySound = new delay[] {
    new delay(de), new delay(de), new delay(de), new delay(de),
    new delay(de), new delay(de), new delay(de), new delay(de),
    new delay(de), new delay(de), new delay(de), new delay(de),
};
    int _soundIdx = 0;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void PlaySound(eSoundType type, float soundPitch = 1.0f)
    {
        if (!_delaySound[_soundIdx].IsEnd())
            return;
        _delaySound[_soundIdx].Reset();
        _soundSources[_soundIdx].clip = _sounds[(int)type];
        _soundSources[_soundIdx].pitch = soundPitch;
        _soundSources[_soundIdx].Play();

        ++_soundIdx;
        _soundIdx = _soundIdx % _soundSources.Length;
    }
    // TODO: fade in out, mute

    public void SetVolume(float ratio)
    {
        _musicSource.volume = ratio;
        for (int i = 0; i < _soundSources.Length; ++i)
            _soundSources[i].volume = ratio;
        pistol.GetSource().volume = ratio;
        rifle.GetSource().volume = ratio;
        bomb.GetSource().volume = ratio;
        step.GetSource().volume = ratio;
    }
}
