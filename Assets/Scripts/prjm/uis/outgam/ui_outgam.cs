using UnityEngine;

public class ui_outgam : MonoBehaviour
{
    public enum eState
    {
        none = 0,
        menu,
        multiEntry,
        multiList,
        room
    }

    [SerializeField] public pn_menu menu;
    [SerializeField] public pn_custom custom;
    [SerializeField] public pn_multiEntry multiEntry;
    [SerializeField] public pn_multiList multiList;
    
    public void Active(eState state_, ui_cover.State coverState = ui_cover.State.None)
    {
        uis.cover.SetState(coverState);
        gameObject.SetActive(state_ != eState.none);
        menu.gameObject.SetActive(state_ == eState.menu);
        custom.gameObject.SetActive(state_ == eState.menu);
        multiEntry.gameObject.SetActive(state_ == eState.multiEntry);
        multiList.gameObject.SetActive(state_ == eState.multiList);
    }

    public void Inactive(ui_cover.State coverState = ui_cover.State.None)
    {
        uis.cover.SetState(coverState);
        gameObject.SetActive(false);
    }

}