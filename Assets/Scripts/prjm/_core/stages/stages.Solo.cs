using System.Collections;
using UnityEngine;

public partial class stages /*solo*/
{
    public IEnumerator StartSolo_()
    {
        yield return core.loads.FromJson_(GetJson(dStage.PlayingIdx), loads.TypeBg.ByJs, true);
        ctrls.Inst.SpawnOnSolo();
        yield return null;
        uis.ingam.play.lbCountdownRemain.Set(lbCountdownRemain.State.SoloStage, durationTime);
        core.unitClones.EnqMsg(msgType.SoloEndTime, durationTime);
        core.loads.SetBgsFromJson();
        ads.Inst.PreLoad(dStage.PlayingIdx);
    }
}
