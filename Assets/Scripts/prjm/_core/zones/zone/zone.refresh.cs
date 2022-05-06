using UnityEngine;

public partial class zone
{
    static public int CntTask = 5;
    public bool having = false;

    public bool HasTask_OnOff()
    {
        bool bw = hasTask_OnOff_Walls();
        bool b0 = hasTask_OnOff_Tiles();
        bool b1 = hasTask_OnOff_Bgs();
        bool b2 = hasTask_OnOff_Prps();
        return bw || b0 || b1 || b2;
    }

    public bool HasTask_OnOn()
    {
        bool bw = hasTask_OnOn_Walls();
        bool b0 = hasTask_OnOn_Tiles();
        bool b1 = hasTask_OnOn_Bgs();
        bool b2 = hasTask_OnOn_Prps();
        return bw || b0 || b1 || b2;
    }

    public void Refresh_WithRelatedZn_ByJs()
    {
        Refresh_ByJs();
        for (int t = 0; t < adjx.Count; ++t)
            core.zones[adjx[t]].Refresh_ByJs();
    }

    public void Refresh_ByJs()
    {
        refreshWalls_ByJs();
        refreshTiles_ByJs();
        refreshZProps_ByJs();
        refreshBgs_ByJs();
    }
}
