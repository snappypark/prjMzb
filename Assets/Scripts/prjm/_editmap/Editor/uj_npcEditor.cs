using UnityEngine;
using UnityEngineEx;
using UnityEditor;

[CustomEditor(typeof(uj_npc))]
public class uj_npcEditor : Editor
{
    unit.SubType _subType = unit.SubType.ZombieLv1;
    
    model.eType _modelType;
    matUnits.Type _matType = matUnits.Type.Zomb0;
    model.Equip _equipType = model.Equip.Melee;

    Melee _melee = Melee.BaseBallBat;

    F_Head _headF = F_Head.Style1;
    M_Head _headM = M_Head.Style1;
    ZF_Head _headZF = ZF_Head.Style1;
    ZM_Head _headZM = ZM_Head.Style1;

    F_Body _bodyF = F_Body.Black;
    M_Body _bodyM = M_Body.BanditA;
    ZF_Body _bodyZF = ZF_Body.BlackDress;
    ZM_Body _bodyZM = ZM_Body.BlackSuit;

    public override void OnInspectorGUI()
    {
        uj_npc ui = (uj_npc)target;

        if (GUILayout.Button("Apply pre data"))
            ui.ApplyPreData();

        base.OnInspectorGUI();

        _subType = (unit.SubType)EditorGUILayout.EnumPopup("subType", _subType);

        if (_subType != ui.OnGUI_GetFsm() || ui.checkCreatedFlagOnce())
        {
            if (ui.autoRandomValue)
            {
                SetDataByFsm_Randomly();
                ui.OnGUI_randDir_Syn();
            }
        }

        _modelType = (model.eType)EditorGUILayout.EnumPopup("modelType", _modelType);
        _matType = (matUnits.Type)EditorGUILayout.EnumPopup("matUnits", _matType);
        _equipType = (model.Equip)EditorGUILayout.EnumPopup("equipType", _equipType);

        _melee = (Melee)EditorGUILayout.EnumPopup("melee", _melee);

        switch (_modelType)
        {
            case model.eType.Female:
                _headF = (F_Head)EditorGUILayout.EnumPopup("head", _headF);
                _bodyF = (F_Body)EditorGUILayout.EnumPopup("body", _bodyF);
                ui.OnGUI_Syn((byte)_headF, (byte)_bodyF);
                break;
            case model.eType.Male:
                _headM = (M_Head)EditorGUILayout.EnumPopup("head", _headM);
                _bodyM = (M_Body)EditorGUILayout.EnumPopup("body", _bodyM);
                ui.OnGUI_Syn((byte)_headM, (byte)_bodyM);
                break;
            case model.eType.Zombie_Female:
                _headZF = (ZF_Head)EditorGUILayout.EnumPopup("head", _headZF);
                _bodyZF = (ZF_Body)EditorGUILayout.EnumPopup("body", _bodyZF);
                ui.OnGUI_Syn((byte)_headZF, (byte)_bodyZF);
                break;
            case model.eType.Zombie_Male:
                _headZM = (ZM_Head)EditorGUILayout.EnumPopup("head", _headZM);
                _bodyZM = (ZM_Body)EditorGUILayout.EnumPopup("body", _bodyZM);
                ui.OnGUI_Syn((byte)_headZM, (byte)_bodyZM);
                break;
        }

        ui.OnGUI_Syn(_subType, _modelType, _matType, _equipType, _melee);
    }


    void SetDataByFsm_Randomly()
    {
        switch (_subType) { 
            case unit.SubType.Tmp1:
                _modelType = model.eType.Female;
                _matType = matUnits.Type.CitizenA;
                break;
            case unit.SubType.MeleeLv1: case unit.SubType.MeleeLv2: case unit.SubType.MeleeLv3: case unit.SubType.MeleeLv4:
            case unit.SubType.PistolLv1: case unit.SubType.PistolLv2: case unit.SubType.PistolLv3: case unit.SubType.PistolLv4:
                _modelType = model.GetRandType(model.eType.Female, model.eType.Zombie_Female);
                _matType = (matUnits.Type)UnityEngine.Random.Range((int)matUnits.Type.CitizenA, (int)matUnits.Type.Zomb0);
                break;
            case unit.SubType.ZombieLv1:
                _modelType = model.GetRandType(model.eType.Zombie_Female, model.eType.Max);
                _matType = (matUnits.Type)UnityEngine.Random.Range((int)matUnits.Type.Zomb0, (int)matUnits.Type.Zomb3);
                break;
            case unit.SubType.ZombieLv2:
                _modelType = model.GetRandType(model.eType.Zombie_Female, model.eType.Max);
                _matType = matUnits.Type.Zomb3;
                break;
            case unit.SubType.ZombieLv3:
                _modelType = model.GetRandType(model.eType.Zombie_Female, model.eType.Max);
                _matType = matUnits.Type.Zomb4;
                break;
            case unit.SubType.ZombieLv4:
                _modelType = model.GetRandType(model.eType.Zombie_Female, model.eType.Max);
                _matType = matUnits.Type.Zomb5;
                break;
            case unit.SubType.ZombieLv5:
                _modelType = model.GetRandType(model.eType.Zombie_Female, model.eType.Max);
                _matType = matUnits.Type.Zomb6;
                break;
        }
        
        switch (_subType)
        {
            case unit.SubType.Tmp1:
                _equipType = model.Equip.Melee;
                _melee = Melee.Revolver;
                break;
            case unit.SubType.MeleeLv1: case unit.SubType.MeleeLv2:  case unit.SubType.MeleeLv3: case unit.SubType.MeleeLv4:
                _equipType = model.Equip.Melee;
                _melee = model.GetRandMelee(Melee.BaseBallBat, Melee.Handgun);
                break;
            case unit.SubType.PistolLv1: case unit.SubType.PistolLv2: case unit.SubType.PistolLv3: case unit.SubType.PistolLv4:
                _equipType = model.Equip.Pistol;
                _melee = model.GetRandMelee(Melee.BaseBallBat, Melee.Handgun);
                break;

            case unit.SubType.ZombieLv1: case unit.SubType.ZombieLv2: case unit.SubType.ZombieLv3: case unit.SubType.ZombieLv4:
                _equipType = model.Equip.Melee;
                _melee = model.GetRandMelee(Melee.BaseBallBat, Melee.Handgun);
                break;
        }

        _headF = FModel.GetRandHead();
        _headM = MModel.GetRandHead();
        _headZF = ZFModel.GetRandHead();
        _headZM = ZMModel.GetRandHead();
        switch (_subType)
        {
            case unit.SubType.Tmp1:
                _bodyF = F_Body.Office;
                _bodyM = M_Body.Tuxedo;
                break;

            case unit.SubType.MeleeLv1:
                _bodyF = FModel.GetRandBody(F_Body.UnderwearWhite, F_Body.PoliceA);
                _bodyM = MModel.GetRandBody(M_Body.TShirtWhite, M_Body.LeatherJacket);
                break;
            case unit.SubType.MeleeLv2:
                _bodyF = FModel.GetRandBody(F_Body.White, F_Body.SportsSkirt);
                _bodyM = MModel.GetRandBody(M_Body.TShirtBlack, M_Body.Sports);
                break;
            case unit.SubType.MeleeLv3:
                _bodyF = FModel.GetRandBody(F_Body.SportsSkirt, F_Body.JacketBlack);
                _bodyM = MModel.GetRandBody(M_Body.Sports, M_Body.TShirtWhite);
                break;
            case unit.SubType.MeleeLv4:
                _bodyF = FModel.GetRandBody(F_Body.JacketBlack, F_Body.UnderwearWhite);
                _bodyM = MModel.GetRandBody(M_Body.LeatherJacket, M_Body.PunkA);
                break;

            case unit.SubType.PistolLv1:
                _bodyF = FModel.GetRandBody(F_Body.Nun, F_Body.Nurse);
                _bodyM = MModel.GetRandBody(M_Body.RockerA, M_Body.FarmerA);
                break;
            case unit.SubType.PistolLv2:
                _bodyF = FModel.GetRandBody(F_Body.Nurse, F_Body.PunkA);
                _bodyM = MModel.GetRandBody(M_Body.FarmerA, M_Body.PoliceA);
                break;
            case unit.SubType.PistolLv3:
                _bodyF = FModel.GetRandBody(F_Body.PunkA, F_Body.Max);
                _bodyM = MModel.GetRandBody(M_Body.Sailor, M_Body.Max);
                break;
            case unit.SubType.PistolLv4:
                _bodyF = FModel.GetRandBody(F_Body.PoliceA, F_Body.Nun);
                _bodyM = MModel.GetRandBody(M_Body.PoliceA, M_Body.Sailor);
                break;

            case unit.SubType.ZombieLv1:
            case unit.SubType.ZombieLv2:
            case unit.SubType.ZombieLv3:
            case unit.SubType.ZombieLv4:
            case unit.SubType.ZombieLv5:
                _bodyZF = ZFModel.GetRandBody();
                _bodyZM = ZMModel.GetRandBody();
                break;

        }
    }


}
