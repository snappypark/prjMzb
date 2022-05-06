using UnityEngine;

public class zbGrass : zbg
{
    const float size = 0.8f;
    //rot, size0.8~1.0, size 0.6 ~ 0.8
    public override void Assign(ref zone.zbg_ info)
    {
        transform.localEulerAngles = new Vector3(0, info.opts.F1, 0);
        transform.localScale = new Vector3(info.opts.F2 * size, info.opts.F3 * size, info.opts.F2 * size);
        //    
    }
}
