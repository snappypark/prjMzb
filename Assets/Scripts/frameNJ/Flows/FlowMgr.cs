using System;
using System.Collections;
using UnityEngine;

namespace nj
{ 
[System.Serializable]
public class FlowMgr
{
    public byte PreType = Flow_Abs.iTypeNone;
    public byte CurType = Flow_Abs.iTypeNone;
    public byte NextType = Flow_Abs.iTypeNone;

    [SerializeReference] Flow_Abs _curFlow = null;
    Flow_Abs _nextFlow = null;

    public bool HasCurFlow { get { return _curFlow != null; } }
    public bool IsFlowLoop { get { return _flowLoop; } }

    bool _updateCoLoop = false;
    bool _flowLoop = true; // 

    public void Change<T>(Action callBack = null) where T : Flow_Abs, new()
    {
      //  if (!_flowLoop)
      //      return;
        _nextFlow = new T();
        _nextFlow.callback = callBack;
        PreType = CurType;
        NextType = _nextFlow.iType;
        _flowLoop = false;
        core.skx = -1;
        core.sights.timerUpdate.Reset();

       // if (ctrl.Unit.tran != null)
        //    core.sights.SetNextPixels_Sight(-sights.heroDist8, -sights.heroDist8, sights.heroDist8, sights.heroDist8);
        core.sights.textRoller.ShiftTexture(); core.sights.textRoller.ShiftTexture();
    }


    public IEnumerator Update_()
    {
        _flowLoop = true;
        _updateCoLoop = true;
        while (_updateCoLoop)
        {
            _curFlow = _nextFlow;
            CurType = _curFlow.iType;
            yield return _curFlow.OnEnter_();
            if (_curFlow.callback != null)
                _curFlow.callback();
            core.skx = 0;
            _flowLoop = true;
            while (_flowLoop)
                yield return null;
            yield return _curFlow.OnExit_();
            GC.Collect();
        }
    }

}
}