using UnityEngine;

public class uj_tile : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.Tile; } }

    public enum eState
    {
        tile1s = 0,
        tile5s,
        tileTs,
    }

    [SerializeField] eState _mode = eState.tile1s;
    [SerializeField] js.tile_ _jsTile;

    private void OnEnable()
    {
        _mode = eState.tileTs;
        if (_jsTile == null) _jsTile = new js.tile_(0, 0, 0, 0, (byte)uj_abs.matTile);
    }

    void LateUpdate()
    {
        switch (_mode)
        {
            case eState.tile1s:
                uis.editmap.SetSelectMode(uj_editmap.eSelect.SingleCel1l);
                break;
            case eState.tile5s:
                uis.editmap.SetSelectMode(uj_editmap.eSelect.RangeCel5l);
                break;
            case eState.tileTs:
                uis.editmap.SetSelectMode(uj_editmap.eSelect.RangeCel1l);
                break;
        }
    }
    public override void Create(i5 b1d)
    {
        js.zone_ jsZn = null;
        if (js.Inst.FindZoneByArea(b1d, out jsZn))
        {
            switch (_mode)
            {
                case eState.tile1s:
                    js.Inst.AddTile1(jsZn, b1d.xMin, b1d.zMin, b1d.xMax, b1d.zMax, (byte)_jsTile.matIdx, js.SeriType.WithLoad);
                    break;
                case eState.tile5s:
                    break;
                case eState.tileTs:
                    js.Inst.AddTileT(jsZn, b1d.xMin, b1d.zMin, b1d.xMax, b1d.zMax, (byte)_jsTile.matIdx, js.SeriType.WithLoad);
                    break;
            }

            jsZn.zn.Refresh_WithRelatedZn_ByJs();
            core.collis.Clear();
        }

    }

    public override void Delete(i5 b1d, i5 b5d)
    {
        Debug.Log("should delete the zone.");
    }

}
