using UnityEngine;

public class zArrow : zprp
{
    [SerializeField] Transform _tr;

    // opt: rotate
    public override void Assign(zone.prp_ info)
    {
        cel1ls.NearZnType type = (cel1ls.NearZnType)(info.opts.F1+0.1f);
        switch (type)
        {
            case cel1ls.NearZnType.East:
                _tr.localEulerAngles = new Vector3(90, 90,0);
                break;
            case cel1ls.NearZnType.West:
                _tr.localEulerAngles = new Vector3(90, -90, 0);
                break;
            case cel1ls.NearZnType.North:
                _tr.localEulerAngles = new Vector3(90, 0, 0);
                break;
            case cel1ls.NearZnType.South:
                _tr.localEulerAngles = new Vector3(90, 180, 0);
                break;
        }
    }
}
