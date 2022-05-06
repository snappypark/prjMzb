using UnityEngine;
using UnityEngine.UI;

using Mosframe;

public class pn_multiList : MonoBehaviour
{
    [SerializeField] Text _lb;
    [SerializeField] DynamicVScrollView _scroll;

    void OnEnable()
    {
        _lb.text = core.multis.GetModeName_InLobby();
    }

    public void Refresh(int numItem)
    {
        _scroll.totalItemCount = numItem;
        _scroll.refresh();
    }

    #region UI Action
    public void OnBtn_Close()
    {
        core.multis.EndMulti();
    }
    #endregion

}