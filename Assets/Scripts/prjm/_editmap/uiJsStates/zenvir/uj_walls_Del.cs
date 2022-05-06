using UnityEngine;

public class uj_walls_Del : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.Walls_Del; } }

    js.wall_Del_ _jsWall;
    js.zone_ _jsZn = null;

    void OnEnable()
    {
        _jsWall = new js.wall_Del_();
    }

    void LateUpdate()
    {
        uis.editmap.SetSelectMode(uj_editmap.eSelect.RangeCel1l);
    }


    public override void Create(i5 b1d)
    {
        if (js.Inst.FindZoneByArea(b1d, out _jsZn))
        {
            for (int x = b1d.X0; x <= b1d.X1; ++x)
                for (int z = b1d.Z0; z <= b1d.Z1; ++z)
                    zjs.walls.Remove(core.zells[x, z]);

            js.Inst.AddWallsDel(_jsZn, b1d.X0, b1d.Z0, b1d.X1, b1d.Z1, js.SeriType.WithLoad);
            _jsZn.zn.Refresh_WithRelatedZn_ByJs();
        }
    }

}
