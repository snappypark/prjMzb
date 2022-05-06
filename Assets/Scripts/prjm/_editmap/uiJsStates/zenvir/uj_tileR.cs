using UnityEngine;

public class uj_tileR : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.TileR; } }
    
    [SerializeField] js.tileR_ _jsTileR;

    void OnEnable()
    {
        if (_jsTileR == null) _jsTileR = new js.tileR_(new f4(0, 0, 0, 0), (byte)uj_abs.matTile);
    }

    void LateUpdate()
    {
        uis.editmap.SetSelectMode(uj_editmap.eSelect.SingleCel1l);
    }
    public override void Create(i5 b1d)
    {
        js.zone_ jsZn = null;
        if (js.Inst.FindZoneByArea(b1d, out jsZn))
        {
            _jsTileR = new js.tileR_(
                new f4(b1d.X0 + 1, b1d.Z0 + 1, 1.111f, 1.111f), 
                (byte)_jsTileR.matIdx);

            js.Inst.AddTileR(jsZn,
                _jsTileR.opts.F1, _jsTileR.opts.F2, _jsTileR.opts.F3, _jsTileR.opts.F4,
                (byte)_jsTileR.matIdx, js.SeriType.WithLoad);

            jsZn.zn.Refresh_WithRelatedZn_ByJs();
            core.collis.Clear();
        }

    }

    public override void Delete(i5 b1d, i5 b5d)
    {
        Debug.Log("should delete the zone.");
    }

}
