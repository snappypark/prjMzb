using UnityEngine;

public class zjs : MonoBehaviour
{
    static public zjs inst;

    public static tiles tiles;
    public static walls walls;
    public static zbgs zbgs;
    public static zprps zprps;

    public static rprs rprs;

    void Awake()
    {
        inst = this;

        tiles = tiles.Inst;
        walls = walls.Inst;
        zbgs = zbgs.Inst;
        zprps = zprps.Inst;
        rprs = rprs.Inst;
    }

    public void Clear()
    {
        rprs.Clear();
        zbgs.UnuseAllGamObj();
        zprps.UnactiveAll();
        tiles.UnuseAllGamObj();
        walls.UnuseAllGamObj();
    }
}
