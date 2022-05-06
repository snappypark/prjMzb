using UnityEngine;
using UnityEngine.UI;


public class popEndSolo : MonoBehaviour
{
    public enum State { None, RetryGame, NextGame, LastStage, }
    enum rewardType {none, x1, x10}

    [SerializeField] Image _root;
    [SerializeField] Text _lbCenter;
    [SerializeField] Text _lbRight;
    
    [SerializeField] uiRatio _rtHome;
    [SerializeField] uiRatio _rtRetry;
    [SerializeField] uiRatio _rtNext;
    [SerializeField] uiRatio _rtGift;
    
    [SerializeField] Text _lbNumReward;

    rewardType _rewardType = rewardType.none;

    public void Active(int playingIdx, State state, string str)
    {
        switch(state)
        {
            case State.RetryGame:
                _lbRight.text = langs.stageSharp(playingIdx, dStage.LastIdx);
                _rtGift.gameObject.SetActive(false);
            break;
            case State.NextGame:
                settingAds(playingIdx);
                dStage.SaveNextLevel();
                _lbRight.text = langs.stageNext(playingIdx+1, dStage.LastIdx);
            break;
            case State.LastStage:
                _lbRight.text = langs.stageSharp(playingIdx, dStage.LastIdx);
                _rtGift.gameObject.SetActive(false);
            break;
        }

        _lbCenter.text = str;
        
        _root.gameObject.SetActive(true);
        _root.enabled = true;
        gameObject.SetActive(true);
        
        _rtRetry.gameObject.SetActive(state == State.RetryGame || state == State.LastStage);
        _rtNext.gameObject.SetActive(state == State.NextGame);
    }

    void settingAds(int playingIdx)
    {
        switch(playingIdx%5)
        {
            case 2:
            if(ads.Inst.IsLoadRewardX1())
                _rewardType = rewardType.x1;
            _lbNumReward.text = "";
            break;
            case 4:
            if(ads.Inst.IsLoadRewardX10())
                _rewardType = rewardType.x10;
            _lbNumReward.text = "x10";
            break; 
        }
        _rtGift.gameObject.SetActive(_rewardType != rewardType.none);

        if(_rewardType == rewardType.none) {
            _rtHome.Refresh(0.11f, 0.4f, 0.18f, 0.18f);
            _rtNext.Refresh(0.33f, 0.4f, 0.18f, 0.18f);
        }
        else{
            _rtHome.Refresh(0.1f, 0.4f,  0.18f, 0.18f);
            _rtNext.Refresh(0.23f, 0.4f, 0.18f, 0.18f);
            _rtGift.Refresh(0.36f, 0.4f, 0.18f, 0.18f); }

    }
    
    #region UI Action
    public void OnBtn_Home()
    {
        if (uis.IsEnableBtnTime(1.7f))
        {
            uis.cover.SetState(ui_cover.State.Loading);
            core.Inst.flowMgr.Change<Flow_Menu>();
                
            _root.gameObject.SetActive(false);
            _root.enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void OnBtn_Retry()
    {
        if (uis.IsEnableBtnTime(1.7f))
        {
            audios.Inst.SetNeedToPlayDefaultMusic();
            uis.cover.SetState(ui_cover.State.LoadStage);
            core.Inst.flowMgr.Change<Flow_SoloPlay>();
                
            _root.gameObject.SetActive(false);
            _root.enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void OnBtn_Next()
    {
        if (uis.IsEnableBtnTime(1.7f))
        {
            dStage.SetPlayingNextIdx();
            uis.cover.SetState(ui_cover.State.LoadStage);
            core.Inst.flowMgr.Change<Flow_SoloPlay>();
            _root.gameObject.SetActive(false);
            _root.enabled = true;
            gameObject.SetActive(false);
        }
    }
    
    public void OnBtn_Reward()
    {
        if (uis.IsEnableBtnTime(1.7f))
        {
            switch(_rewardType)
            {
                case rewardType.x1:
                ads.Inst.ShowRewardX1();
                break;
                case rewardType.x10:
                ads.Inst.ShowRewardX10();
                break;
            }
            _rtGift.gameObject.SetActive(false);
        }
    }
    #endregion
}
