using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popEndMulti : MonoBehaviour
{
    [SerializeField] Image _root;

    [SerializeField] Text _text;

    public void Active(string str)
    {
        //  _players.Sort();
        //  _text.text = _players.GetScoreStr();

        _text.text = str;

        _root.gameObject.SetActive(true);
        _root.enabled = false;
        gameObject.SetActive(true);
    }

    const float _over60 = 0.016666666666666667f;
    public void Active(ref List<boltPlyor> plyors, multis.Mode mode, float duration = 20)
    {
        string str = string.Empty;
        switch (mode)
        {
            case multis.Mode.Escap:
                for (int i = 0; i < plyors.Count; ++i)
                {
                    boltPlyor p = plyors[i];
                    int min = (int)(p.state.endTime * _over60);

                    if (p.state.endTime < duration)
                        str += string.Format("{0}. {1}  {2}:{3:00}\n", i + 1, p.name, min,(int)p.state.endTime % 60);
                    else
                        str += string.Format("{0}. {1}\n", i + 1, p.name);
                }
                break;
            case multis.Mode.Battle:
                for (int i = 0; i < plyors.Count; ++i)
                {
                    boltPlyor p = plyors[i];
                    str += string.Format("{0}. {1}  {2}\n", i + 1, p.name, p.state.score);
                }
                break;
        }
        for (int i = plyors.Count; i < 8; ++i)
            str += string.Format("\n");

        _text.text = str;

        _root.gameObject.SetActive(true);
        _root.enabled = false;
        gameObject.SetActive(true);
    }


    /*
    List<synBoltPlyor> _players = new List<synBoltPlyor>();

    public string GetScoreStr()
    {
        for (int i = 0; i < _players.Count; ++i)
        {
            synBoltPlyor p = _players[i];
            str += string.Format("{0}. {1}: {2}\n", i + 1, p.name, p.score);
        }
        for (int i = _players.Count; i < _uis.Length; ++i)
            str += string.Format("\n");
        return str;
    }
    */

    #region UI Action

    public void OnBtnOk()
    {
        _root.gameObject.SetActive(false);
        _root.enabled = true;
        gameObject.SetActive(false);
    }

    #endregion
}
