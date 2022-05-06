using UnityEngine;

public class uj_zbg : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.zBg; } }

    [SerializeField] int _ZnIdx = -1;
    [SerializeField] js.zbg_ _jsZbg;

    void LateUpdate()
    {
        uis.editmap.SetSelectMode(uj_editmap.eSelect.SingleCel1l);
    }

    public override void Select(i5 b1d, i5 b5d)
    {
        js.zone_ jsZn = null;
        if (js.Inst.FindZoneByArea(b1d, out jsZn))
        {
            _ZnIdx = jsZn.zn.idx;
            Debug.Log("[zbg]selected zone: " + _ZnIdx);
        }
    }

    public override void Create(i5 b1d)
    {
        js.zone_ jsZn = null;
        if (js.Inst.FindZoneByIdx(_ZnIdx, out jsZn))
        {
            Debug.Log("[zbg]create zone: " + _ZnIdx);
            switch (_jsZbg.oType)
            {
                case zbgs.Type.Cloud:
                    _jsZbg.opts = new f4(Random.Range(2.1f,4), Random.Range(20, 120), 0);
                    _jsZbg.ps = new Vector3(b1d.X0+0.5f, jsZn.bd.Y - (_jsZbg.opts.F1+ Random.Range(2.5f, 6)), b1d.Z0 + 0.5f);
                    break;
                case zbgs.Type.Grass:
                case zbgs.Type.Flower:
                    _jsZbg.opts = new f4(Random.Range(0, 360), Random.Range(0.8f, 1.0f), Random.Range(0.6f, 0.8f));
                    _jsZbg.ps = new Vector3(b1d.X0 , jsZn.bd.Y+0.5f, b1d.Z0);
                    break;
                case zbgs.Type.Word:
                    _jsZbg.ps = new Vector3(b1d.X0 + 0.5f, jsZn.bd.Y+0.5f, b1d.Z0 + 0.5f);
                    break;
                default:
                    _jsZbg.ps = new Vector3(b1d.X0 + 0.5f, jsZn.bd.Y+0.5f, b1d.Z0 + 0.5f);
                    break;
            }

            js.Inst.AddZbg(jsZn, _jsZbg.oType, _jsZbg.ps, _jsZbg.opts, js.SeriType.WithLoad);

            jsZn.zn.Refresh_WithRelatedZn_ByJs();

            _createdFlagForEdit = true;
        }
    }

    public override void Delete(i5 b1d, i5 b5d)
    {
        Debug.Log("should delete the zone.");
    }

    public zbgs.Type OnGUI_GetObjType()
    {
        return _jsZbg.oType;
    }

    public void OnGUI_SynOptions(f4 options_)
    {
        _jsZbg.opts = options_;
    }

    enum flowerType
    {
        Flower1 = cel1l.Type.Flower1,
        Flower2,
        Flower3,
        Flower4,
    }

    enum wordType
    {
        Word_Stage = cel1l.Type.Word_Stage,
        Word_ThisWayLeft,
        Word_ThisWayRight,
        Word_Welcome,
    }
    /*
     * 
            case zprps.Type.Word: // (botton or top), colorMat
                _wordType = (wordType)EditorGUILayout.EnumPopup("wordType", _wordType);
                _opt1 = EditorGUILayout.FloatField("top or botton (1 or -1):", _opt1);
                _matWall = (matWalls.Type)EditorGUILayout.EnumPopup("matWall", _matWall);
                break;

                case zprps.Type.Word:
                    _opt1 = -1;
                    _matWall = ui_js_abs.matWall;
                    break;
                    */
}
