using UnityEngine;
using UnityEngine.UI;

public class pnnCreateRoom : MonoBehaviour
{
    [SerializeField] Text _lbBtn;

    [SerializeField] Text _lbToggle1;
    [SerializeField] Text _lbToggle2;

    [SerializeField] Toggle _toggle1;
    [SerializeField] Toggle _toggle2;

    void OnEnable()
    {
        _lbToggle1.text = langs.Public();
        _lbToggle2.text = langs.Dark();
        _lbBtn.text = langs.CreateRoom();
    }

    #region UI Action

    public void OnBtn_CreateRoom()
    {
        nets.Inst.bolts.CreateServer(_toggle1.isOn, _toggle2.isOn);
    }

    #endregion
}
