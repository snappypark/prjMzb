using System.Collections;
using UnityEngine;

public partial class loads
{

    public IEnumerator Clear_()
    {
        unit0.Inactive(Vector3.zero, true);
        core.unitClones.Clear();

        yield return clearCommon_();
    }

    public IEnumerator ClearWithoutUnit_()
    {
        yield return clearCommon_();
    }

    IEnumerator clearCommon_()
    {
        uis.ingam.play.lbCountdownRemain.Set(lbCountdownRemain.State.None, 0);
        yield return null;
        for (int i = 0; i < core.zones.Num; ++i)
            core.zones[i].ResetJspState(zone.eJspState.None);
        uis.cursor.Set(uis.CursorType.OutGame);

        yield return null; abjs.inst.Clear();
        yield return null; core.zones.Clear();
        yield return null; gjs.inst.Clear();
        yield return null; core.zells.Clear();
        yield return null; zjs.inst.Clear();
        core.collis.Clear();
        zjs.rprs.roller.OnUpdate(new Pt(0, 2, 0));
    }
}
