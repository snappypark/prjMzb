using UnityEngine;

public class ctrlHud_word : ctrlHud
{
    void Update()
    {
        transform.localPosition +=
            new Vector3(0, 0, 0.5f * Time.smoothDeltaTime);

        if (endTime < Time.time)
            gjs.ctrlHuds.Unactive(ctrlHuds.eWord, cdx);
    }
}
