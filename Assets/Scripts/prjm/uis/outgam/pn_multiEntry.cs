using UnityEngine;
using UnityEngineEx;
using UnityEngine.UI;

using Mosframe;

public class pn_multiEntry : MonoBehaviour
{
    [SerializeField] AniCurveEx _AniBg;
    [SerializeField] AniCurveEx_TranScale _AniPanel;
    [SerializeField] Image _bg;
    [SerializeField] GameObject _scrollObj;
    [SerializeField] DynamicVScrollView _scroll;
    [SerializeField] Text _lb;
    [SerializeField] Text _lb1;
    [SerializeField] Text _lb2;

    private void OnEnable()
    {
        _lb1.text = langs.Maze_Escape();
        _lb2.text = langs.Team_Battle();
        _lb.text = string.Format("{0}", synRegion.GetCurName());
        _AniBg.ResetTime(0.4f);
        _AniPanel.SetScale(0.2f, 0.3f);
        _bg.color = _bg.color.Alpha(0.0f);
    }

    public void Refresh()
    {
        _scroll.refresh();
        _lb.text = string.Format("{0}", synRegion.GetCurName());
    }

    void Update()//
    {
        if (_AniBg.UpdateUntilTime())
            _bg.color = _bg.color.Alpha(Mathf.Clamp01(_AniBg.Evaluate() - 0.2f));
        if (_AniPanel.UpdateUntilTime())
            _AniPanel.SetScale(_AniPanel.Evaluate());
    }

    #region Action
    public void OnBtnStart_MultiMaze()
    {
        gotoMultiLobby(multis.Mode.Escap);
    }

    public void OnBtnStart_TeamMatch()
    {
        gotoMultiLobby(multis.Mode.Battle);
    }

    public void OnBtn_Region()
    {
        _scrollObj.SetActive(!_scrollObj.activeSelf);
        _scroll.refresh();
    }

    public void OnBtnClose()
    {
        endUI();
        uis.outgam.Active(ui_outgam.eState.menu, ui_cover.State.Menu);
    }

    #endregion

    void gotoMultiLobby(multis.Mode mode)
    {
        endUI();
        if (nets.checkNotReachable())
        {
            uis.outgam.Active(ui_outgam.eState.menu, ui_cover.State.Menu);
            uis.pops.Show_Warning("check network, can't connect.");
        }
        else
        {
            uis.outgam.Active(ui_outgam.eState.none, ui_cover.State.Connecting);
            core.multis.StartMulti(mode);
        }
    }

    void endUI()
    {
        _scrollObj.SetActive(false);
        _AniPanel.SetScale(0.4f, 0.3f);
        _bg.color = _bg.color.Alpha(0.0f);
    }
}
