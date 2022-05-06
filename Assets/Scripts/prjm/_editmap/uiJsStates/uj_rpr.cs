using UnityEngine;

public class uj_rpr : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.Prop; } }

    [SerializeField] js.rpr_ _jsPrp;
    js.zone_ _jsZn = null;

    private void LateUpdate()
    {
        uis.editmap.SetSelectMode(uj_editmap.eSelect.SingleCel1l);
    }

    public override void Create(i5 b1d)
    {
        _jsPrp.x = b1d.X0;
        _jsPrp.z = b1d.Z0;
        cel1l cell = core.zells[b1d.X0, b1d.Z0];
        if (cell.type != cel1l.Type.Tile)
            return;

        if (js.Inst.FindZoneByArea(b1d, out _jsZn))
        {
            js.Inst.AddRpr(_jsZn, b1d.X0, b1d.Z0, (byte)_jsPrp.type, _jsPrp.cellType, _jsPrp.opt, js.SeriType.WithLoad);
            zjs.rprs.roller.OnUpdate(new Pt(0, 2, 0));

            _createdFlagForEdit = true;
        }
    }

    public override void Delete(i5 b1d, i5 b5d)
    {
        if (js.Inst.FindZoneByArea(b1d, out _jsZn))
        {
            for (int x = b1d.X0; x <= b1d.X1; ++x)
                for (int z = b1d.Z0; z <= b1d.Z1; ++z)
                {
                    js.Inst.RemoveRpr(_jsZn, x, z);
                    core.zells[x, z].Clear();
                }
            zjs.rprs.roller.OnUpdate(new Pt(0, 2, 0));
            Debug.Log("d");
        }

    }

    public void ApplyPreData()
    {
        cel1l cell = core.zells[_jsPrp.x, _jsPrp.z];
    }

    public rprs.Type OnGUI_GetPropType()
    {
        return _jsPrp.type;
    }
    public void OnGUI_SetCellType(cel1l.Type type_)
    {
        _jsPrp.cellType = type_;
    }
    public void OnGUI_SetOptions(f4 options_)
    {
        _jsPrp.opt = options_;
    }
}
