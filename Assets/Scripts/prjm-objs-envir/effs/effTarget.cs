using UnityEngine;

public class effTarget : eff
{
    void Update()
    {
        if(target == null)
            return;
        transform.localPosition = target.localPosition + Vector3.up;
        //transform.localPosition += nor * speed * Time.smoothDeltaTime;
        if (endTime < Time.time)
            gjs.effs.Unactive(type, cdx);
    }
}
