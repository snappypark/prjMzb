using UnityEngine;

public class uj_zprp : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.zProp; } }

    [SerializeField] js.zprp_ _jsZprp;
    js.zone_ _jsZn = null;

    void LateUpdate()
    {
        switch (_jsZprp.oType)
        {
            case zprps.Type.Area:
            case zprps.Type.Door://
                uis.editmap.SetSelectMode(uj_editmap.eSelect.RangeCel1l);
                break;
            default:
                uis.editmap.SetSelectMode(uj_editmap.eSelect.SingleCel1l);
                break;
        }
    }

    public override void Create(i5 b1d)
    {
        _jsZprp.bd = b1d.i4;
        if (js.Inst.FindZoneByArea(b1d, out _jsZn))
        {
            js.Inst.AddZprop(_jsZn,
                b1d.X0, b1d.Z0, b1d.X1, b1d.Z1,
                (byte)_jsZprp.oType, _jsZprp.cellType, _jsZprp.opts, js.SeriType.WithLoad);

            _jsZn.zn.Refresh_WithRelatedZn_ByJs();
            _createdFlagForEdit = true;
        }
    }
    
    public void ApplyPreData()
    {
        if (_jsZprp != null && _jsZn.prps.Count > 0)
        {
            int last = _jsZn.prps.Count - 1;
            _jsZn.prps[last].opts = _jsZprp.opts;

            _jsZn.zn.Refresh_WithRelatedZn_ByJs();
        }

    }

    public zprps.Type OnGUI_GetZpropType()
    {
        return _jsZprp.oType;
    }
    public void OnGUI_SynCellType(cel1l.Type type_)
    {
        _jsZprp.cellType = type_;
    }
    public void OnGUI_SynOptions(f4 options_)
    {
        _jsZprp.opts = options_;
    }
}
