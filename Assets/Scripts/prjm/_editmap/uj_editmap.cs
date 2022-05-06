using UnityEngine;
using UnityEngine.UI;
using UnityEngineEx;
using UnityEngine.EventSystems;

public partial class uj_editmap : MonoBehaviour
{
    [SerializeField] Text _lbPos;
    //[Header("Edit")]
    [SerializeField] eMode _mode = eMode.Create;
    [SerializeField] eState _state = eState.Zone;
    [SerializeField] eSelect _select = eSelect.SingleCel1l;
    [HideInInspector] public int _idxY = 0;
    [HideInInspector] uj_abs _uiJs = null;
    public i5 Getbd1 { get { return _bd1; } }
    public i5 Getbd5 { get { return _bd5; } }
    i5 _bd1, _bd5;
    Vector3 _pos1, _pos2;

    private void OnEnable()
    {
        _uiJs = GetComponent<uj_zn>();
    }

    [HideInInspector] public int _idxCurY = -1;
    public void LateUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 _posOnXZ;
        if (cams.PickingPlan(out _posOnXZ))
        {
            _posOnXZ = _posOnXZ.WithGapY(0.5f);
            if (_select == eSelect.SingleCel1l || _select == eSelect.RangeCel1l)
                core.huds.editmap.DrawCel1l(_posOnXZ);
            else
                core.huds.editmap.DrawCel5l(_posOnXZ);
        }

        if (cams.Plan_End(ref _pos1, cams.MOUSE_LEFT) ||
            cams.Plan_End(ref _pos2, cams.MOUSE_RIGHT) )
        {
            _pos1 = _pos1.WithGapY(0.5f);
            _pos2 = _pos2.WithGapY(0.5f);
            if (_select == eSelect.SingleCel1l || _select == eSelect.SingleCel5l)
                _pos2 = _pos1;
            _bd1 = new i5(cel1l.Pt(Vector3.Min(_pos1, _pos2)), cel1l.Pt(Vector3.Max(_pos1, _pos2)), (int)(_posOnXZ.y * 0.333333333333f));
            _bd5 = new i5(cel5l.Pt(Vector3.Min(_pos1, _pos2)), cel5l.Pt(Vector3.Max(_pos1, _pos2)), (int)(_posOnXZ.y * 0.333333333333f));
            if (_select == eSelect.SingleCel1l || _select == eSelect.RangeCel1l)
            {
                core.huds.editmap.DrawCel1lSelect(_pos1, _pos2);
                core.huds.editmap.DrawCel5lSelect(Vector3.zero, Vector3.zero);
                _lbPos.text = string.Format(
                    "[c1{0}] {1},{2} ~ {3},{4} ({5},{6})", (int)(_posOnXZ.y * 0.333333333333f),
                        _bd1.X0, _bd1.Z0, _bd1.X1, _bd1.Z1, _bd1.SizeX, _bd1.SizeZ);
            }
            else
            {
                core.huds.editmap.DrawCel1lSelect(Vector3.zero, Vector3.zero);
                core.huds.editmap.DrawCel5lSelect(_pos1, _pos2);
                _lbPos.text = string.Format(
                    "c5[{0}] {1},{2} ~ {3},{4} ({5},{6})", (int)(_posOnXZ.y * 0.333333333333f),
                        _bd5.X0, _bd5.Z0, _bd5.X1, _bd5.Z1, _bd5.SizeX, _bd5.SizeZ);
            }

            if(_uiJs != null)
                _uiJs.Select(_bd1, _bd5);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            switch (_mode)
            {
                case eMode.Create:
                    _uiJs.Create(_bd1);
                    break;
                case eMode.Delete:
                    _uiJs.Delete(_bd1, _bd5);
                    break;
            }
        }
    }

    #region select 
    public enum eSelect
    {
        SingleCel1l,
        SingleCel5l,
        RangeCel1l,
        RangeCel5l,
    }
    public void SetSelectMode(eSelect select_)
    {
        _select = select_;
    }
    #endregion

    #region state
    public enum eState
    {
        Zone = 0,
        Tile,
        TileR,
        WallsMaze,
        WallsRect,
        Walls,
        Walls_Del,

        zProp,
        Npc,
        zBg,

        Prop,

        Spawn,

        _AutoAllChange,
        _AutoAllStages,
        _AllMultiEscape,
        _AllMultiBattle,
    }

    public void OnGUI_StateComponent()
    {
        if (_uiJs == null)
            _uiJs = add_state_component();
        else if (_state != _uiJs.State){
            GameObject.DestroyImmediate(_uiJs);
            _uiJs = add_state_component();
        }
    }

    uj_abs add_state_component() {
        switch (_state)
        {
            default: return null;
            case eState.Zone: return gameObject.AddComp_NoDupliated<uj_zn>();
            case eState.Tile: return gameObject.AddComp_NoDupliated<uj_tile>();
            case eState.TileR: return gameObject.AddComp_NoDupliated<uj_tileR>();
            case eState.WallsMaze: return gameObject.AddComp_NoDupliated<uj_wallsMaze>();
            case eState.WallsRect: return gameObject.AddComp_NoDupliated<uj_wallsRect>();
            case eState.Walls: return gameObject.AddComp_NoDupliated<uj_wallsSingle>();
            case eState.Walls_Del: return gameObject.AddComp_NoDupliated<uj_walls_Del>();

            case eState.zProp: return gameObject.AddComp_NoDupliated<uj_zprp>();
            case eState.Npc: return gameObject.AddComp_NoDupliated<uj_npc>();
            case eState.zBg: return gameObject.AddComp_NoDupliated<uj_zbg>();

            case eState.Prop: return gameObject.AddComp_NoDupliated<uj_rpr>();
            case eState.Spawn: return gameObject.AddComp_NoDupliated<uj_spawn>();

            case eState._AutoAllChange: return gameObject.AddComp_NoDupliated<uj_AutoAllChange>();
            case eState._AutoAllStages: return gameObject.AddComp_NoDupliated<uj_AllStgs>();
            case eState._AllMultiEscape: return gameObject.AddComp_NoDupliated<uj_mulEscape>();
            case eState._AllMultiBattle: return gameObject.AddComp_NoDupliated<uj_mulBattle>();
                
        }
    }
    #endregion

    #region mode
    enum eMode
    {
        Create,
        Delete,
    }

    public void OnGUI_Mode()
    {
        switch (_mode)
        {
            case eMode.Create:
                if (GUILayout.Button("Create Mode: Create!!"))
                    _uiJs.Create(_bd1);
                break;
            case eMode.Delete:
                if (GUILayout.Button("Delete Mode: Delete!!"))
                    _uiJs.Delete(_bd1, _bd5);
                break;
        }
    }
    #endregion
}
