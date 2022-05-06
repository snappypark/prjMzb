using UnityEngine;
using UnityEngine.UI;

public class popOption : MonoBehaviour
{
    public enum Type
    {
        InStage,
        InMenu,
    }

    [SerializeField] Image _rootImg;
    [SerializeField] GameObject _root;

    [SerializeField] GameObject _btnHome;
    [SerializeField] GameObject _btnRetry;
    [SerializeField] GameObject _btnRate;
    [SerializeField] GameObject _btnAds;

    [SerializeField] GameObject _btnPreStage;
    [SerializeField] GameObject _btnCredit;
    [SerializeField] GameObject _btnLanguage;

    [SerializeField] Image _imgSound;
    [SerializeField] Sprite[] _sprsSounds;

    [SerializeField] Text[] _lbs;

    public void Active(Type type)
    {
        _rootImg.enabled = true;
        _imgSound.sprite = _sprsSounds[dOption.SoundIdx];

        _btnHome.SetActive(type == Type.InStage);
        _btnRetry.SetActive(type == Type.InStage);

        _btnPreStage.SetActive(type == Type.InMenu);
        _btnCredit.SetActive(type == Type.InMenu);
        _btnRate.SetActive(type == Type.InMenu);
        _btnAds.SetActive(type == Type.InMenu);
       // _btnLanguage.SetActive(type == Type.InMenu);

        _root.SetActive(true);
        gameObject.SetActive(true);

        _lbs[0].text = langs.stagePre();
        _lbs[1].text = langs.Option_credit();
        _lbs[2].text = langs.Option_language();
        _lbs[3].text = langs.rateUs();
        _lbs[4].text = langs.tryNow();
    }

    #region UI Action
    public void OnBtn_Rate()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ninetyjay.MazeZombieBreak"); 
    }
    public void OnBtn_Ads()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ninetyjay.lineshot"); 
    }

    public void OnBtn_Close()
    {
        gameObject.SetActive(false);
        _root.SetActive(false);
    }

    public void OnBtn_Sound()
    {
        dOption.ChangeAndSave_SoundIdx();
        _imgSound.sprite = _sprsSounds[dOption.SoundIdx];
    }

    public void OnBtn_Home()
    {
        gameObject.SetActive(false);
        _root.SetActive(false);
        uis.cover.SetState(ui_cover.State.Loading);
        core.Inst.flowMgr.Change<Flow_Menu>();
    }

    public void OnBtn_Retry()
    {
        gameObject.SetActive(false);
        _root.SetActive(false);
        uis.cover.SetState(ui_cover.State.LoadStage);
        core.Inst.flowMgr.Change<Flow_SoloPlay>();
    }

    public void OnBtn_PreStage()
    {
        gameObject.SetActive(false);
        uis.pops.result.preStage.Active();

    }

    public void OnBtn_Credit()
    {
        _root.SetActive(false);
        gameObject.SetActive(false);
        uis.pops.Show_Ok("develop&design: 90j studio  \nsound&music: freesfx.co.uk\nsound&music: zapsplat.com");
    }

    public void OnBtn_Language()
    {
    }
    #endregion
}
