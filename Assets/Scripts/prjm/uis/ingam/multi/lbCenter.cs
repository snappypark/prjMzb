using UnityEngine;
using UnityEngine.UI;

public class lbCenter : MonoBehaviour
{
    public enum Type
    {
        None,
        CountDownToStart,
        Victory,
        Defeat,
        Tie,
        Timeout,
        Winner,
        GameEnd,
    }

    [SerializeField] Text _lb;

    Type _type;
    delay _dly = new delay(1);
    public void Active(Type type, float duration = 2.7f)
    {
        _type = type;
        _dly.Reset(duration);
        switch (type)
        {
            case Type.CountDownToStart:
                _lb.color = new Color(1.0f, 1.0f, 1.0f, 0.9f);
                _lb.text = string.Format("{0}", (int)_dly.fRemain());
                break;
            case Type.Victory:
                _lb.color = new Color(7.0f, 7.0f, 1.0f, 0.8f);
                _lb.text = "Victory";
                break;
            case Type.Defeat:
                _lb.color = new Color(1.0f, 7.0f, 7.0f, 0.8f);
                _lb.text = "Defeat";
                break;
            case Type.Tie:
                _lb.color = new Color(1.0f, 1.0f, 1.0f, 0.8f);
                _lb.text = "Tie";
                break;
            case Type.Timeout:
                _lb.color = new Color(1.0f, 7.0f, 7.0f, 0.8f);
                _lb.text = "Time Out";
                break;
            case Type.GameEnd:
                _lb.color = new Color(1.0f, 7.0f, 7.0f, 0.8f);
                _lb.text = "Game End";
                break;
        }

        Debug.Log("_isEscaped" + type);
        gameObject.SetActive(true);
    }

    void Update()//
    {
        switch (_type)
        {
            case Type.CountDownToStart:
                _lb.text = string.Format("{0}", (int)_dly.fRemain());
                break;
        }
        
        if (_dly.IsEnd(-0.654311f))
            gameObject.SetActive(false);
    }
}
