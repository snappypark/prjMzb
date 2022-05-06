using UnityEngine;

public class zbCloud : zbg
{
    //4~6, 20~120
    public override void Assign(ref zone.zbg_ info)
    {
        transform.localScale =
               new Vector3(info.opts.F1, info.opts.F1, info.opts.F1);
        transform.localEulerAngles = new Vector3(info.opts.F2, 0, 0);
        
    }
}
