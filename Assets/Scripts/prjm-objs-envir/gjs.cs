using UnityEngine;

public class gjs : MonoBehaviour
{
    static public gjs inst;

    public static ctrlHuds ctrlHuds;
    public static effs effs;
    public static smudges smudges;

    void Awake()
    {
        inst = this;

        ctrlHuds = ctrlHuds.Inst;
        effs = effs.Inst;
        smudges = smudges.Inst;
    }

    public void Clear()
    {
        effs.UnactiveAll();
        smudges.UnuseAllGamObj();
    }
}
