using UnityEngine;
using UnityEngine.UI;

public class ui_ad : MonoBehaviour
{
    [SerializeField] public GameObject Logo;
    [SerializeField] public GameObject Banner;
    [SerializeField] GameObject _Inter;
    [SerializeField] Sprite[] _SprsGeoBreaker;
    [SerializeField] Image _ImgGeoBreaker;

    public void ShowGeoBreaker()
    {
        audios.Inst.PauseMusic();
        _Inter.SetActive(true);
        _ImgGeoBreaker.sprite = _SprsGeoBreaker[Random.Range(0,3)];
    }

#region Action

    public void OnBtn_Banner()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ninetyjay.lineshot");
    }

    public void OnBtn_CloseGeoBreaker()
    {
        audios.Inst.UnPauseMusic();
        _Inter.SetActive(false);
    }

    public void OnBtn_LinkGeoBreaker()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ninetyjay.lineshot");
    }

#endregion 
}
