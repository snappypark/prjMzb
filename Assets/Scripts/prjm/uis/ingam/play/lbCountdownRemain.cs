using UnityEngine;
using UnityEngine.UI;

public class lbCountdownRemain : MonoBehaviour
{
    public enum State
    {
        None,
        SoloStage,
        Multi,
    }

    [SerializeField] Text _lb;

    State _state = State.None;

    delay _dly = new delay(0);
    public void Set(State state, float duration = 0)
    {
        _state = state;
        switch (state)
        {
            case State.None:
                _lb.enabled = false;
                break;
            case State.SoloStage:
            case State.Multi:
                _lb.enabled = true;
                _dly.Reset(duration);
                OnUpdate();
                break;
        }
    }

    const float _over60 = 0.016666666666666667f;

    public void OnUpdate()
    {
        switch (_state)
        {
            case State.None:

                break;
            case State.SoloStage:
            case State.Multi:
                if (_dly.InTime())
                    refresh(_dly.fRemain());
                else
                {
                    _lb.enabled = false;
                    _state = State.None;
                }
                break;
        }

    }
    // float sec = core.stages.remain.fRemain();
    void refresh(float sec)
    {
        int min = (int)(sec * _over60);
        _lb.text = string.Format("{0}:{1:00}", min, (int)sec % 60);

    }
}
