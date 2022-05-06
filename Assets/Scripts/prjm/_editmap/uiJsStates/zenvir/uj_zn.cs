using UnityEngine;

public class uj_zn : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.Zone; } }

    [SerializeField] int _tmp = 0;
    [SerializeField] int _tmpX = 0;
    [SerializeField] int _tmpZ = 0;
    [SerializeField] bool _WithTile = true;
    [SerializeField] matTiles.Type _matTile = matTiles.Type.Gray;
    [SerializeField] js.zone_ _jsZn;

    void OnEnable()
    {
        _WithTile = true;
        _jsZn = new js.zone_();
        _jsZn.eSight = sights.eState.ByCtrlUnit;
        _tmp = UnityEngine.Random.Range(1, 24);
        _tmpX = UnityEngine.Random.Range(4, 8);
        _tmpZ = UnityEngine.Random.Range(4, 8);
    }

    void LateUpdate()
    {
        uis.editmap.SetSelectMode(uj_editmap.eSelect.RangeCel1l);
        uj_abs.matTile = _matTile;
    }

    public override void Create(i5 b1d)
    {
        _tmp = UnityEngine.Random.Range(1, 28);
        _tmpX = UnityEngine.Random.Range(4, 8);
        _tmpZ = UnityEngine.Random.Range(4, 8);
        if (js.Inst.IsCollided(b1d))
            Debug.Log("[ui_js_zone] if (jsData.Inst.IsCollided(b5d))");
        else
        {
            js.zone_ jsZn = js.Inst.AddZone(b1d.X0, b1d.Z0, b1d.X1, b1d.Z1, b1d.Y, (int)_jsZn.eSight, js.SeriType.WithLoad);
            reset_LinkZones();
            if (_WithTile)
            {
                js.Inst.AddTileT(jsZn, b1d.X0, b1d.Z0, b1d.X1, b1d.Z1, (byte)_matTile, js.SeriType.WithLoad);

                jsZn.zn.Refresh_WithRelatedZn_ByJs();
                core.collis.Clear();
            }
        }
    }

    void reset_LinkZones()
    {
        for (int i = 0; i < js.Inst.zones.Count; ++i)
            js.Inst.zones[i].zn.adjx.Clear();
        for (int i = 0; i < js.Inst.zones.Count; ++i)
        {
            for (int j = i + 1; j < js.Inst.zones.Count; ++j)
            {
                if (js.Inst.zones[i].bd.IsContacted(js.Inst.zones[j].bd))
                {
                    js.Inst.zones[i].zn.adjx.Add((byte)j);
                    js.Inst.zones[j].zn.adjx.Add((byte)i);
                }
            }
        }
    }

    public override void Delete(i5 b1d, i5 b5d)
    {
        // check target
        // delete hud
        // delete has compo
        // delete zones
        // link all again
    }
}
