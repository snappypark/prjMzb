using UnityEngine;
using UnityEngine.UI;

public class popRate : MonoBehaviour
{
    [SerializeField] GameObject _root;

    [SerializeField] Text _lb;
    
    [SerializeField] Text _lbOk;

    public void Active()
    {
        _lb.text = langs.Rate();
        _lbOk.text = langs.Ok();
        gameObject.SetActive(true);
    }

    #region UI Action

    public void OnBtn_Ok()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ninetyjay.MazeZombieBreak");
    }

    public void OnBtn_Cancel()
    {
        gameObject.SetActive(false);
        _root.SetActive(false);
    }
    #endregion
}
