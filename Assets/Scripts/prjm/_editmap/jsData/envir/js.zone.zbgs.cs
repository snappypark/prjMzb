using UnityEngine;

public partial class js
{
    public void AddZbg(zone_ zn, zbgs.Type oType_, Vector3 ps_, f4 opts_, SeriType seriType)
    {
        zn.zbgs.Add(new zbg_(oType_, ps_, opts_));
        if (seriType == SeriType.WithLoad)
            zn.zn.AddBgs((byte)oType_, ps_, opts_);
    }

    [System.Serializable]
    public class zbg_
    {
        [SerializeField] public zbgs.Type oType;
        [HideInInspector] public Vector3 ps;
        [SerializeField] public f4 opts;
        public zbg_(zbgs.Type oType_, Vector3 ps_, f4 opts_)
        {
            oType = oType_;
            ps = ps_;
            opts = opts_;
        }
    }

    public void AddZbg_CloudByTunnel(zone_ zn)
    {
        float x = Random.Range(zn.bd.X0, zn.bd.X1);
        float z = Random.Range(zn.bd.Z0, zn.bd.Z1);
        float gap = Random.Range(-3, 3);
        if (zn.bd.GapX < zn.bd.GapZ)
            x += gap;
        else
            z += gap;

        f4 opt = new f4(Random.Range(2.1f, 4), Random.Range(20, 120), 0);
        Vector3 ps = new Vector3(x+ 0.5f, -(opt.F1 + Random.Range(2.5f, 4)), z + 0.5f);
        AddZbg(zn, zbgs.Type.Cloud, ps, opt, SeriType.Default);
    }

    public void Add_Zbg_Zprp_ByEntryZone(zone_ zn)
    {
        Vector3 ps = new Vector3(zn.bd.X0 + 0.5f + zn.bd.GapX*0.5f, 0, zn.bd.Z1 - 0.5f);

        f4 opt = new f4((float)zbWord.Type.Word_Stage + 0.1f,
                        (float)zbWord.PosType.Up + 0.1f,
                        1.2f);
        AddZbg(zn, zbgs.Type.Word, ps, opt, SeriType.Default);

        AddRpr(zn, zn.bd.X0 + 2, zn.bd.Z0+2, rprs.eLamb, cel1l.Type.LambOff, f4.O, js.SeriType.WithCell);
        AddRpr(zn, zn.bd.X1 - 2, zn.bd.Z0 + 2, rprs.eLamb, cel1l.Type.LambOff, f4.O, js.SeriType.WithCell);

        int x = zn.bd.X0 + 5;
        int z = zn.bd.Z0 + 2;
        AddZprop(zn, new i4(x, z, x, z), zprps.eAmmo, cel1l.Type.Ammo, new f4(1, 0, 10, 30), SeriType.WithCell);
    }
}
