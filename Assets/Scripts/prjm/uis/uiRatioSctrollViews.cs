using UnityEngine;

public class uiRatioSctrollViews : MonoBehaviour
{
    [SerializeField] RectTransform _rtItemRegion;
    [SerializeField] RectTransform _rtItemRoom;
    private void Start()
    {
        _rtItemRegion.sizeDelta = new Vector2(0, nj.App.Scr.Height * 0.07f);
        _rtItemRoom.sizeDelta = new Vector2(0, nj.App.Scr.Height * 0.12f);
    }

    
}
