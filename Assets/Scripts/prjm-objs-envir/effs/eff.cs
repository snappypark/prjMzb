using System.Runtime.CompilerServices;
using UnityEngine;

public class eff : nj.qObj
{
    [SerializeField] public ParticleSystem ps;

    [HideInInspector] public float endTime;

    [HideInInspector] public Transform target;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Play(Transform target_, float endTime_)
    {
        ps.Play();
        endTime = endTime_;
        target = target_;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Play(float endTime_)
    {
        ps.Play();
        endTime = endTime_;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Play(float endTime_, Color color_)
    {
        var mainModule = ps.main;
        mainModule.startColor = color_;
        ps.Play();
        endTime = endTime_;
    }

    void Update()
    {
        //transform.localPosition += nor * speed * Time.smoothDeltaTime;
        if (endTime < Time.time)
            gjs.effs.Unactive(type, cdx);
    }
}
