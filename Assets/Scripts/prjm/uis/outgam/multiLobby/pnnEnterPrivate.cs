using UnityEngine;
using UnityEngine.UI;

public class pnnEnterPrivate : MonoBehaviour
{
    [SerializeField] Text _lbField;
    [SerializeField] Text _lbBtn;

    void OnEnable()
    {
        _lbField.text = langs.EnterCode();
        _lbBtn.text = langs.JoinPrivateRoom();
    }

    #region UI Action

    public void OnBtn_EnterPrivate()
    {
        nets.Inst.bolts.JoinSessionPrivate(_lbField.text);
    }

    #endregion
}
