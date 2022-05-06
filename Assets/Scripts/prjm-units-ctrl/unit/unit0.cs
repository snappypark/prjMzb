using UnityEngine;

public class unit0 : unit
{
    public static unit inst = null;
    void Awake()
    {
        cdx = 0;
        inst = this;
        Inactive(Vector3.zero);
    }

    public static void Inactive()
    {
        Inactive(Vector3.zero, true);
    }

    public static void Inactive(Vector3 pos, bool setPos = false)
    {
        unit u = inst;
        u.gameObject.SetActive(false);
        u.Init(Type.None);
        ctrls.Inst.SetState(ctrls.State.None, u);
        if (setPos)
        {
            u.SetPosDir(pos, Vector3.forward);
            u.SetCell();
            cams.Inst.mainTran.SetTarget(u.tran, new Vector3(4.1f, -0.4f, -6.6f));

        }
    }
}
