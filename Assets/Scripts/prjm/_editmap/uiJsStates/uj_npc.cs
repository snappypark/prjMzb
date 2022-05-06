using UnityEngine;
using UnityEngineEx;

public class uj_npc : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState.Npc; } }
    
    [SerializeField] public bool autoRandomValue = true;

    [SerializeField] js.npc_ _JsNpc;
    js.zone_ _jsZn = null;
    npc _npc = null;

    private void OnEnable()
    {
        _JsNpc = new js.npc_(Vector3.zero, Vector3.forward, model.eType.Female,
            matUnits.Type.CitizenA, model.Equip.Pistol, 
            1, //ally
            0, 0, 0, units.eAniCtrlNpcZombie, unit.SubType.ZombieLv1);
    }

    void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            _JsNpc.subType = unit.SubType.ZombieLv1;
        else if (Input.GetKeyUp(KeyCode.Alpha2))
            _JsNpc.subType = unit.SubType.ZombieLv2;
        else if (Input.GetKeyUp(KeyCode.Alpha3))
            _JsNpc.subType = unit.SubType.ZombieLv3;
        else if (Input.GetKeyUp(KeyCode.Alpha4))
            _JsNpc.subType = unit.SubType.ZombieLv4;
        else if (Input.GetKeyUp(KeyCode.Alpha5))
            _JsNpc.subType = unit.SubType.ZombieLv5;

        uis.editmap.SetSelectMode(uj_editmap.eSelect.SingleCel1l);
    }

    public override void Create(i5 b1d)
    {
        if (js.Inst.FindZoneByArea(b1d, out _jsZn))
        {
            Vector3 pos = cel1l.Center(new Pt(b1d.X0, b1d.Y, b1d.Z0));

            _npc = js.Inst.AddNpc(_jsZn, 
                pos, _JsNpc.dir, _JsNpc.modelType, (byte)_JsNpc.matType, _JsNpc.equipType, _JsNpc.ally,
                _JsNpc.headIdx, _JsNpc.bodyIdx, _JsNpc.meleeIdx, _JsNpc.aniCtrlIdx,
                _JsNpc.subType, js.SeriType.WithLoad);
            _createdFlagForEdit = true;
        }
    }

    public override void Delete(i5 b1d, i5 b5d)
    {
    }

    public void ApplyPreData()
    {
        if (_jsZn != null && _npc != null && _jsZn.npcs.Count > 0)
        {
            int last = _jsZn.npcs.Count - 1;
            _JsNpc.dir = _JsNpc.dir.normalized;
            _jsZn.npcs[last].dir = _JsNpc.dir;
            _jsZn.npcs[last].ally = _JsNpc.ally;
            _jsZn.npcs[last].modelType = _JsNpc.modelType;
            _jsZn.npcs[last].matType = _JsNpc.matType;
            _jsZn.npcs[last].equipType = _JsNpc.equipType;
            _jsZn.npcs[last].headIdx = _JsNpc.headIdx;
            _jsZn.npcs[last].bodyIdx = _JsNpc.bodyIdx;
            _jsZn.npcs[last].meleeIdx = _JsNpc.meleeIdx;
            _jsZn.npcs[last].aniCtrlIdx = _JsNpc.aniCtrlIdx;
            _jsZn.npcs[last].subType = _JsNpc.subType;

            _npc.tran.forward = _JsNpc.dir;
            _npc.attb.ally = _JsNpc.ally;
            
            core.units.ChangeModel(_npc, (byte)_JsNpc.modelType, (byte)_JsNpc.matType,
                _JsNpc.headIdx, _JsNpc.bodyIdx, _JsNpc.meleeIdx, model.Equip.Melee);
        }
    }

    public unit.SubType OnGUI_GetFsm()
    {
        return _JsNpc.subType;
    }

    public void OnGUI_Syn(unit.SubType fsm_,
        model.eType modelType_,
        matUnits.Type matUnits_,
        model.Equip equip_,
        Melee melee_ )
    {
        _JsNpc.subType = fsm_;
        _JsNpc.modelType = modelType_;
        _JsNpc.matType = matUnits_;
        _JsNpc.equipType = equip_;
        _JsNpc.meleeIdx = (byte)melee_;

        if (modelType_ == model.eType.Zombie_Female ||
            modelType_ == model.eType.Zombie_Male)
        {
            _JsNpc.aniCtrlIdx = units.eAniCtrlNpcZombie;
        }
        else
        {
            _JsNpc.aniCtrlIdx = units.eAniCtrlNpcCitizen;
        }
    }

    public void OnGUI_Syn(byte headIdx_, byte bodyIdx_)
    {
        _JsNpc.headIdx = headIdx_;
        _JsNpc.bodyIdx = bodyIdx_;
    }


    public void OnGUI_randDir_Syn()
    {
        _JsNpc.dir = RandEx.Dir8();

    }
}
