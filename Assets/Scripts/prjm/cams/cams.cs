using UnityEngine;

public class cams : MonoBehaviour
{
    public static cams Inst;

    [SerializeField] Camera _cam;
    [SerializeField] public Camera hud;
    [SerializeField] public tranCam mainTran;
    [SerializeField] public MiniMapCam MiniMap;

    void Awake()
    {
        Inst = this;
    }

    public void SetClearFlag(CameraClearFlags flag)
    {
        _cam.clearFlags = flag;
    }

    public void SetBgColorSky()
    {
        _cam.backgroundColor = new Color(0.5960785f, 0.772549f, 0.8980392f);
    }

    public void SetBgColor(Color color_)
    {
        _cam.backgroundColor = color_;
    }

    public void SetOrthMode(bool value)
    {
        _cam.orthographic = value;
        if (value)
            _cam.orthographicSize = 17;
    }

    public const int MOUSE_LEFT = 0;
    public const int MOUSE_RIGHT = 1;

    public static bool Plan_Begin(ref Vector3 touchedPos, int mouseLeftOrRight = MOUSE_LEFT)
    {
        if (Input.GetMouseButtonDown(mouseLeftOrRight))
            if (PickingPlan(out touchedPos))
                return true;
        return false;
    }

    public static bool Plan_Pressed(ref Vector3 touchedPos, int mouseLeftOrRight = MOUSE_LEFT)
    {
        if (Input.GetMouseButton(mouseLeftOrRight))
            if (PickingPlan(out touchedPos))
                return true;
        return false;
    }

    public static bool Plan_End(ref Vector3 touchedPos, int mouseLeftOrRight = MOUSE_LEFT)
    {
        if (Input.GetMouseButtonUp(mouseLeftOrRight))
            if (PickingPlan(out touchedPos))
                return true;
        return false;
    }

    public static bool PickingPlan(out Vector3 touchedPos)
    {
        Ray ray = cams.Inst._cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, nj.App.TouchPlan.Mask))
        {
            touchedPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            return true;
        }
        touchedPos = Vector3.positiveInfinity;
        return false;
    }

    public Vector2 ViewportPos(Vector3 pos_)
    {
        return _cam.WorldToViewportPoint(pos_);
    }
}
