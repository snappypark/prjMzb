using UnityEngine;
using UnityEngineEx;
using UnityEngine.UI;

public class ui_cover : MonoBehaviour
{
    public enum State { None, Menu, LoadStage, Loading, Connecting, Disconnecting, }

    [SerializeField] public AniCurveEx_UIGraphics Img;

    [SerializeField] Image _block;
    [SerializeField] Text _lbLeft;
    [SerializeField] Text _lbRight;
    
    public void SetState(State state)
    {
        _lbLeft.gameObject.SetActive(true);
        _lbRight.gameObject.SetActive(true);
        switch (state)
        {
            case State.None:
                _block.color = new Color(0, 0, 0, 0); 
                _block.raycastTarget = false;
                _lbLeft.text = _lbRight.text = string.Empty;
                break;
            case State.Menu:
                _block.raycastTarget = false;
                _lbLeft.text = string.Empty; _lbRight.text = string.Format("# {0}", dStage.Idx + 1);
                break;
                
            case State.LoadStage:
                _block.raycastTarget = true;
                _lbLeft.text = langs.stageSharp(dStage.PlayingIdx, dStage.LastIdx); 
                _lbRight.text = string.Empty;
                break;
            case State.Loading:
                _block.raycastTarget = true;
                _lbLeft.text = " Loading..."; _lbRight.text = langs.stageSharp(dStage.PlayingIdx, dStage.LastIdx);
                break;
            case State.Connecting:
                _block.color = new Color(0, 0, 0, 0.6f); _block.raycastTarget = true;
                _lbLeft.text = " Connecting..."; _lbRight.text = string.Empty;
                break;
            case State.Disconnecting:
                _block.color = new Color(0, 0, 0, 0.6f); _block.raycastTarget = true;
                _lbLeft.text = " Disconnecting..."; _lbRight.text = string.Empty;
                break;
        }
    }

    /*
    float deltaTime = 0.0f;

    delay d = new delay(1.7f);
    void LateUpdate()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        if(d.IsEndAndReset())
            _lbTopLeft.text = ((int)(deltaTime * 1000.0f)).ToString();
    }//*/

    //coroutine.

}