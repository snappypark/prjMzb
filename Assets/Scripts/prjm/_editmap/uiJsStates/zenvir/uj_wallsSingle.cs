using UnityEngine;

public class uj_wallsSingle : uj_wallsAbs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.Walls; } }

    [SerializeField] int _stageIdx;
    [SerializeField] js.wallSingle_ _jsWall;
    js.zone_ _jsZn = null;

    void OnEnable()
    {
        _jsWall = new js.wallSingle_();
        _jsWall.matMini = uj_abs.matWall;
    }

    void LateUpdate()
    {
        uis.editmap.SetSelectMode(uj_editmap.eSelect.SingleCel1l);
    }

    public override void Create(i5 b1d)
    {
        if (js.Inst.FindZoneByArea(b1d, out _jsZn))
        {
            mats mats = getMats(_stageIdx);

            _jsWall.matMini = (matWalls.Type)mats.wall;
            _jsWall.matTop = (matWalls.Type)mats.wall;
            _jsWall.matSideUp = (matWalls.Type)mats.wall;
            _jsWall.matSideDown = (matWalls.Type)mats.wall;
            setMats(_jsWall.matMini, _jsWall.matTop, _jsWall.matSideUp, _jsWall.matSideDown);
            js.Inst.AddWallsSingle(_jsZn, b1d.X0, b1d.Z0, _mats, 
                _jsWall.cType, js.SeriType.WithLoad);

            _jsZn.zn.Refresh_WithRelatedZn_ByJs();
        }
    }

    public override void Delete(i5 b1d, i5 b5d)
    {
        Debug.Log("should delete the zone.");
    }

    public void OnGUI_SetWallInfo(cel1l.Type type_)
    {
        _jsWall.cType = type_;
    }


    
    mats getMats(int stageIdx)
    {
        int by100 = stageIdx % 100;
        int tileBy18 = Mathf.Clamp(1 + (int)(by100 / 24), 0, 4); // 4*5
        int by3 = Mathf.Clamp((int)(by100 / 4), 0, 28); 

        switch (by100)
        {
            case 97: case 98: case 99:
                return new mats(matTiles.Gray, matWalls.DarkDark);
        }

        //if(by100 % 6 == 3)
        //    return new mats(matTiles.Gray, (byte)by3);
        return new mats((byte)tileBy18, (byte)by3);
    }

    struct mats
    {
        public byte tile, wall;
        public mats(byte tile_, byte wall_)
        {
            tile = tile_; wall = wall_;
        }
    }

}
