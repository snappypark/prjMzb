using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abjs : MonoBehaviour
{
    static public abjs inst;

    public static boms boms;
    public static slugs slugs;

    void Awake()
    {
        inst = this;

        boms = boms.Inst;
        slugs = slugs.Inst;
    }

    public void Clear()
    {
        boms.UnactiveAll();
        slugs.UnactiveAll();
    }
}
