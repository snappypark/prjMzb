using UnityEngine;

public class uj_wallsMaze : uj_wallsAbs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.WallsMaze; } }
    
    [SerializeField] js.wallsMaze_ _jsWalls;
    js.zone_ _jsZn = null;

    private void OnEnable()
    {
        _jsWalls = new js.wallsMaze_();
        _jsWalls.matMini = (matWalls.Type)UnityEngine.Random.Range(0, 28);
        _jsWalls.rand0 = UnityEngine.Random.Range(10, 10000000);
        _jsWalls.option0 = _jsWalls.rand0;
        matWall = _jsWalls.matMini;
    }

    void LateUpdate()
    {
        uis.editmap.SetSelectMode(uj_editmap.eSelect.RangeCel5l);
    }

    public override void Create(i5 b1d)
    {
        if (js.Inst.FindZoneByArea(b1d, out _jsZn))
        {
            setMats(_jsWalls.matMini, _jsWalls.matTop, _jsWalls.matSideUp, _jsWalls.matSideDown);

            i2 szRC = new i2(5, 5);
            i2 numRC = new i2(10, 10);

            js.Inst.AddWallsMaze(_jsZn, b1d.i4, szRC, numRC, _mats,
                _jsWalls.type, _jsWalls.rand0, _jsWalls.option0, js.SeriType.WithLoad);

            _jsZn.zn.Refresh_WithRelatedZn_ByJs();
        }
        _jsWalls.rand0 = UnityEngine.Random.Range(10, 10000000);
        _jsWalls.option0 = _jsWalls.rand0;
    }

    public override void Delete(i5 b1d, i5 b5d)
    {
        Debug.Log("should delete the zone.");
    }

    public void OnGUI_SetWallInfo(cel1l.Type type_)
    {
        _jsWalls.type = type_;
    }
}
