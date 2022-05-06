using UnityEngine;
using UnityEngine.UI;

public class lstItemMsg : MonoBehaviour
{
    [SerializeField] public Text lb;
    [SerializeField] uiRatio _ur;
    [SerializeField] int _idx;

    public bool RefreshCount(bool toggled, int max)
    {
        _idx = (_idx + 1) % max;
        Refresh(toggled, max);
        return _idx == 0;
    }

    public void Refresh(bool toggled, int max)
    {
        switch (toggled)
        {
            case true:
                lb.enabled = true;
                _ur.Refresh(0.2f, -0.1f - 0.044f * _idx,  0.39f, 0.044f);
                break;
            case false:
                lb.enabled = (_idx < 3);
                _ur.Refresh(0.2f, -0.022f - 0.044f * _idx, 0.39f, 0.044f);
                break;
        }
    }
}
