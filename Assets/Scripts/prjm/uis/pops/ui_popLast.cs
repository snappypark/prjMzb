using System;
using UnityEngine;
using UnityEngine.UI;

public class ui_popLast : MonoBehaviour
{
    [SerializeField] Text _lb;
    [SerializeField] Text _lbPositive;
    [SerializeField] Text _lbNegative;
    [SerializeField] uiRatio _uiPositive;
    [SerializeField] uiRatio _uiNegative;
    [SerializeField] GameObject _goLock;

    ui_pops._type _PopType;
    Action _okFunc;

    public void Show(ui_pops._type type, string str, Action okFunc = null)
    {
        _lb.text = str;
        _PopType = type;
        _okFunc = okFunc;
        gameObject.SetActive(true);
        _goLock.SetActive(type == ui_pops._type.Warning);
        switch (type)
        {
            case ui_pops._type.Ok:
                _uiPositive.Active(0,0.1f);         _uiNegative.Inactive();
                _lbPositive.text = langs.Ok();
                break;
            case ui_pops._type.YesOrNo:
                _uiPositive.Active(-0.12f,0.1f);     _uiNegative.Active(0.12f,0.1f); 
                _lbPositive.text = langs.Yes();     _lbNegative.text = langs.No();
                break;
            case ui_pops._type.Warning:
                _uiPositive.Active(0.2f,0.1f);      _uiNegative.Inactive();
                _lbPositive.text = langs.Ok();
                break;
        }
    }

    public void uBtn_Positive()
    {
        gameObject.SetActive(false);
        if(_okFunc != null)
            _okFunc();
    }

    public void uBtn_Negative()
    {
        gameObject.SetActive(false);
    }
}
