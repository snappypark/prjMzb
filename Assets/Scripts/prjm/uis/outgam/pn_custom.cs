using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pn_custom : MonoBehaviour
{
    [SerializeField] Text _lbCharacter;
    [SerializeField] InputField _fieldName;
    [SerializeField] Text _lbType;
    [SerializeField] Text _lbHead;
    [SerializeField] Text _lbBody;
    [SerializeField] Text _lbHand;

    void OnEnable()
    {
        _lbCharacter.text = langs.Character();
        _fieldName.text = dUser.Name;
        refreshType(); refreshHead(); refreshBody(); refreshHand();
    }
    
    public void Refresh()
    {
        refreshType(); refreshHead(); refreshBody(); refreshHand();
    }

    void refreshType() {
        _lbType.text = langs.CustomType(dCust.Type, dCust.NumType);
    }
    void refreshHead() {
        _lbHead.text = langs.CustomHead(dCust.Head, dCust.HeadMax);
    }

    void refreshBody() {
        _lbBody.text = langs.CustomBody(dCust.Body, dCust.BodyMax);
    }

    void refreshHand() {
    }

    #region UI Action

    public void OnInputField_Name(string str)
    {
        if (string.IsNullOrEmpty(_fieldName.text))
            _fieldName.text = dUser.Name;
        else
            dUser.SaveName(_fieldName.text);
    }

    public void OnBtn_Type_L(){  if (!uis.IsEnableBtnTime()) /*---*/ return;
        dCust.RollType(-1);
        refreshChange(true, true, true, true);
    }

    public void OnBtn_Type_R(){  if (!uis.IsEnableBtnTime()) /*---*/ return;
        dCust.RollType(1);
        refreshChange(true, true, true, true);
    }

    public void OnBtn_Head_L() {  if (!uis.IsEnableBtnTime()) /*---*/ return;
        dCust.RollHead(-1);
        refreshChange(false, true, false, false);
    }

    public void OnBtn_Head_R() {  if (!uis.IsEnableBtnTime()) /*---*/ return;
        dCust.RollHead(1);
        refreshChange(false, true, false, false);
    }

    public void OnBtn_Body_L() {  if (!uis.IsEnableBtnTime()) /*---*/ return;
        dCust.RollBody(-1);
        refreshChange(false, false, true, false);
    }

    public void OnBtn_Body_R() { if (!uis.IsEnableBtnTime()) /*---*/ return;
        dCust.RollBody(1);
        refreshChange(false, false, true, false);
    }

    public void OnBtn_Hand_L() {
        if (!uis.IsEnableBtnTime()) /*---*/ return;
        dCust.RollHand(-1);
        refreshChange(false, false, false, true);
    }

    public void OnBtn_Hand_R() {
        if (!uis.IsEnableBtnTime())  /*---*/ return;
        dCust.RollHand(1);
        refreshChange(false, false, false, true);
    }

    #endregion

    void refreshChange(bool isType, bool isHaed, bool isBody, bool isHand)
    {
        if (isType) refreshType();
        if (isHaed) refreshHead();
        if (isBody) refreshBody();
        if (isHand) refreshHand();
        core.units.ChangeModel(ctrls.Unit, dCust.Model, dCust.SkinMat, 
                                    dCust.Head, dCust.Body, dCust.Hand, model.Equip.Pistol);
    }

    byte RollClamp(int value_, int max_, int gap)
    {
        int value = value_ + gap;
        if (value < 0)
            return (byte)max_;
        else if (value > max_)
            return 0;
        return (byte)value;
    }
}
