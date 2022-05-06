using System.Runtime.CompilerServices;
using UnityEngine;

public partial class sights : MonoBehaviour
{
    public enum eState : byte { None = 0, ByCtrlUnit, ByCtrl, Black  }
    eState _curState = eState.None;
    eState _nextState = eState.None;


    public const float alpha = 0.51f; // 36
    public const float editLr = 0.67f;
    static float _hiddenLightRatio = alpha; // light values --> 0.0 = black, 1.0 = white

    public delay timerUpdate = new delay(0.160f); //0.16556f 0.07556f 0.14556f 0.17556f
    //public textureIgnore igrTexs = new textureIgnore();
    public textureRoller textRoller = new textureRoller();

    private void Awake()
    {
        core.sights = this;
        awake_sightsInfo();
    }

    public void SetState(eState state)
    {
        _nextState = state;
    }

    public void OnEnable()
    {
        textRoller.Init(_hiddenLightRatio);
        //igrTexs.Init(_hiddenLightRatio);
    }
    
    public void SetAlpha(float v)
    {
        _hiddenLightRatio = v;
        textRoller.Init(_hiddenLightRatio);
    }

    static Pt _ct = Pt.Huge;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetForNextState()
    {
        _curState = _nextState;
        //_curState = eState.ByCtrl;
        _ct = ctrls.Unit.cell.pt;
        textRoller.SetNextPixel(_ct.x, _ct.z, Color.white);
    }



}
