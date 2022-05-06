using UnityEngine;

public class zbFlower : zbg
{
    [SerializeField] GameObject _go1;
    [SerializeField] GameObject _go2;
    [SerializeField] GameObject _go3;
    [SerializeField] GameObject _go4;


    public override void Assign(ref zone.zbg_ info)
    {
        transform.localEulerAngles = new Vector3(0, info.opts.F2, 0);
        int type = (int)info.opts.F1;
        _go1.SetActive(type == 0);
        _go2.SetActive(type == 1);
        _go3.SetActive(type == 2);
        _go4.SetActive(type == 3);
    }
}
