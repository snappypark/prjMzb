using UnityEngine;
using UnityEngine.UI;

public class pn_menu : MonoBehaviour
{
    [SerializeField] Text _lbTitle;

    [SerializeField] Text _lbSingle;
    [SerializeField] Text _lbMulti;

    [SerializeField] GameObject _goNoAdBtn;
    [SerializeField] GameObject _goAdBtn;

    void OnEnable()
    {
        _lbTitle.text = langs.GameName();
        _lbSingle.text = langs.StartGame();
        _lbMulti.text = langs.MultiPlayer();

        _goNoAdBtn.SetActive(!dUser.NoAds);
    }

    public void Refresh()
    {
        _goNoAdBtn.SetActive(!dUser.NoAds);
    }
    
    #region Action
    public void OnBtn_SinglePlay()
    {
        if (uis.IsEnableBtnTime(1.7f))
        {
            dStage.SetPlayingIdx();
            uis.outgam.Inactive(ui_cover.State.LoadStage);
            core.Inst.flowMgr.Change<Flow_SoloPlay>();
        }
    }

    public void OnBtn_MultiPlay()
    {
        if (nets.checkNotReachable())
        {
            uis.outgam.Active(ui_outgam.eState.menu, ui_cover.State.Menu);
            uis.pops.Show_Warning("check network, can't connect.");
        }
        else if (uis.IsEnableBtnTime(1.7f))
            uis.outgam.Active(ui_outgam.eState.multiEntry);
    }

    public void OnBtn_Option()
    {
        if (uis.IsEnableBtnTime(1.7f))
        {
            uis.pops.result.option.Active(popOption.Type.InMenu);
        }
    }

    public void OnBtn_NoAds()
    {
        if (uis.IsEnableBtnTime(1.7f))
        {
            if(InApp.Inst != null)
             InApp.Inst.BuyProductID();
        }
    }

    public void OnBtn_Ads()
    {
        if (uis.IsEnableBtnTime(1.7f))
        {
            _goAdBtn.SetActive(false);
            ads.Inst.ShowRewardX10();
        }
    }

    public void ActiveAdBtn(bool value)
    {
        _goAdBtn.SetActive(value);
    }
    
    #endregion
}
