using UnityEngine;

public class uj_wallsRect : uj_wallsAbs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.WallsRect; } }
    
    [SerializeField] js.wallsRect_ _jsWalls;
    js.zone_ _jsZn = null;

    private void OnEnable()
    {
        _jsWalls = new js.wallsMaze_();
        _jsWalls.matMini = matWall;
    }

    void LateUpdate()
    {
        uis.editmap.SetSelectMode(uj_editmap.eSelect.RangeCel1l);
    }

    public override void Create(i5 b1d)
    {
        if (js.Inst.FindZoneByArea(b1d, out _jsZn))
        {
            setMats(_jsWalls.matMini, _jsWalls.matTop, _jsWalls.matSideUp, _jsWalls.matSideDown);
            
            js.Inst.AddWallsRect(_jsZn, b1d.i4, _mats, _jsWalls.type, js.SeriType.WithLoad);

            _jsZn.zn.Refresh_WithRelatedZn_ByJs();
        }
    }

    public override void Delete(i5 b1d, i5 b5d)
    {
    }
    
    public void OnGUI_SetWallInfo(cel1l.Type type_)
    {
        _jsWalls.type = type_;
    }
}
