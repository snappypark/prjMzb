using System.Collections.GenericEx;
using UnityEngine;

public class pPush : rpr
{
    enum state
    {
        first,
        second,
    }

    [SerializeField] MeshRenderer render;
    cel1l _cell;
    Vector3 _end;

    state _state = state.first;
    //options: mat, speed, time gap,
    public override void Assign(cel1l cell)
    {
        _cell = cell;
        switch (cell.type)
        {
            case cel1l.Type.PushUp:
                _end = _cell.ct + new Vector3(0,-1,0);
                break;
            case cel1l.Type.PushSideX:
                _end = _cell.ct + new Vector3(1, 0, 0);
                break;
            case cel1l.Type.PushSide_X:
                _end = _cell.ct + new Vector3(-1, 0, 0);
                break;
            case cel1l.Type.PushSideZ:
                _end = _cell.ct + new Vector3(0, 0, 1);
                break;
            case cel1l.Type.PushSide_Z:
                _end = _cell.ct + new Vector3(0, 0, -1);
                break;
        }

        render.sharedMaterial = zjs.walls.Mats((byte)_cell.opts.F1);
        _state = state.first;
        gameObject.SetActive(true);
    }

    public override void UnAssign()
    {
        gameObject.SetActive(false);
    }

    float _a01;
    void Update()
    {
        switch (_state)
        {
            case state.first:
                float t = Time.time - core.stages.startTime;
                _a01 = Quadratic.Zigzag01(t + _cell.opts.F3, _cell.opts.F2);
                _state = state.second;
                break;
            case state.second:
                transform.localPosition = _cell.ct + (_end - _cell.ct) * _a01;
                if (_cell.collObj != null)
                    _cell.collObj.transform.localPosition = transform.localPosition;
                _state = state.first;
                break;
        }
    }
}
