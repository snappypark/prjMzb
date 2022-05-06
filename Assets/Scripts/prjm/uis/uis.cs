using UnityEngine;

[ExecuteInEditMode]
public partial class uis : MonoBehaviour
{
    [SerializeField] RectTransform _rt;
    
    public Vector2 WorldToCanvas(Vector3 pos)
    {
        Vector2 ViewportPosition = cams.Inst.ViewportPos(pos);
        return new Vector2((ViewportPosition.x - 0.5f) * _rt.sizeDelta.x,
                            (ViewportPosition.y - 0.5f) * _rt.sizeDelta.y);
    }
    /*
    //now you can set the position of the ui element
    UI_Element.anchoredPosition = WorldObject_ScreenPosition;
    */

    static float _btnDelayTime = 0;
    public static bool IsEnableBtnTime(float nexTtile = 0.4166f)
    {
        if (!core.Inst.flowMgr.IsFlowLoop)
            return false;

        if (Time.time > _btnDelayTime)
        {
            _btnDelayTime = Time.time + nexTtile;
            return true;
        }
        return false;
    }
}