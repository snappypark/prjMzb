using System.Collections;
using UnityEngine;
using UnityEngineEx;

public partial class stages /*.EditMap*/
{
    public IEnumerator StartEditMap_()
    {
        yield return core.huds.editmap.Active_();
        ctrls.Inst.SpawnOnEdit(125, 125);
        core.sights.SetAlpha(sights.editLr);
        core.sights.trSs.Refresh();
    }

    public IEnumerator EndEditMap_()
    {
        core.huds.editmap.Clear();
        yield return core.huds.editmap.Inactive_();
        unit0.inst.tran.GetComponent<CapsuleCollider>().enabled = true;
    }
    
    public void OnUpdate_OnEdit(unit u)
    {
        core.zones.OnUpdate_NextZone(u.cell.zn);

        uis.editmap._idxY = Mathf.Clamp(uis.editmap._idxY, 0, cel1ls.MaxY - 1);
        if (uis.editmap._idxCurY != uis.editmap._idxY)
        {
            u.SetPosDir(new Vector3(
                u.tran.localPosition.x,
                uis.editmap._idxY * 3 + 0.5f,
                u.tran.localPosition.z), Vector3.forward);
            uis.editmap._idxCurY = uis.editmap._idxY;
        }
    }
}
