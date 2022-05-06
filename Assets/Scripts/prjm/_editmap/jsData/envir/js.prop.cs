using UnityEngine;
using UnityEngineEx;
using System.Collections.Generic;

public partial class js
{
    public void AddRpr(zone_ zn, int x, int z, byte objType_, cel1l.Type cellType_, f4 options, SeriType seriType)
    {
        cel1l cell = core.zells[x, z];
        switch (seriType)
        {
            case SeriType.WithCell:
            case SeriType.WithLoad:
                if (cell.IsPath)
                {
                    zn.rprs.Add(new js.rpr_(x, z, objType_, cellType_, options));
                    zjs.rprs.Add(x, z, objType_, cellType_, options);
                }
                break;
            default:
                zn.rprs.Add(new js.rpr_(x, z, objType_, cellType_, options));
                break;
        }
    }

    public void RemoveRpr(zone_ zn, int x, int z)
    {
        int idx = findIdxProp(zn, x, z);
        if (idx > -1)
            zn.rprs.RemoveAt(idx);
    }

    int findIdxProp(zone_ zn, int x, int z)
    {
        for (int i = 0; i < zn.rprs.Count; ++i)
        {
            if (zn.rprs[i].x == x && zn.rprs[i].z == z)
                return i;
        }
        return -1;
    }


    [System.Serializable]
    public class rpr_
    {
        [HideInInspector] public int x, z;
        [SerializeField] public rprs.Type type;
        [SerializeField] public cel1l.Type cellType = cel1l.Type.None;
        [HideInInspector] public f4 opt;

        //[HideInInspector] public hudLine hud;

        public rpr_(int x_, int z_, byte objType_, cel1l.Type cellType_, f4 options_)
        {
            x = x_;
            z = z_;
            type = (rprs.Type)objType_;
            cellType = cellType_;
            opt = options_;
        }
    }

    [HideInInspector] BoxDistribLv _BoxDistribLv;
    [HideInInspector] TreeDistribLv _TreeDistribLv;
    [HideInInspector] TreeTypes _TreeTypes;

    public void AddRprRandomly(zone_ zn_, int szR, int szC)
    {
        _BoxDistribLv = RandEx.RandomEnumValue<BoxDistribLv>();
        _TreeDistribLv = RandEx.RandomEnumValue<TreeDistribLv>();
        _TreeTypes = RandEx.RandomEnumValue<TreeTypes>();

        List<i2> lstRand = new List<i2>();

        for (int xt = zn_.bd.left; xt < zn_.bd.right; xt += szR)
            for (int zt = zn_.bd.bottom; zt < zn_.bd.top; zt += szC)
                lstRand.Insert(Random.Range(0, lstRand.Count), new i2(xt, zt));

        for (int i = 0; i < lstRand.Count; ++i)
        {
            i2 pt = lstRand[i];
            switch (RandEx.Get012())
            {
                case 0:
                    if (RandEx.IsTrue_Probability((int)_BoxDistribLv, 12))
                    {
                        int rX, rZ;
                        RandEx.GetPt_OnCorner_Randomly(pt.x, pt.z, szR, szC, out rX, out rZ);
                        js.Inst.AddRpr(zn_, rX, rZ, global::rprs.eBox, cel1l.Type.Box, f4.O, js.SeriType.WithCell);
                    }

                    break;
                case 1:
                    if (RandEx.IsTrue_Probability((int)_TreeDistribLv, 12))
                    {
                        int rX, rZ;
                        RandEx.GetPt_OnCorner_Randomly(pt.x, pt.z, szR, szC, out rX, out rZ);
                        js.Inst.AddRpr(zn_, rX, rZ, global::rprs.eTree, (cel1l.Type)_TreeTypes,
                            new f4(Random.Range(0, 360), -Random.Range(0.0f, 0.7f),
                            Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1f)), js.SeriType.WithCell);
                    }
                    break;
                case 2:
                    if (RandEx.IsTrue_Probability((int)_BoxDistribLv, 12))
                    {
                        int rX, rZ;
                        RandEx.GetPt_OnCorner_Randomly(pt.x, pt.z, szR, szC, out rX, out rZ);
                        js.Inst.AddRpr(zn_, rX, rZ, global::rprs.eBox, cel1l.Type.Box, f4.O, js.SeriType.WithCell);
                    }

                    if (RandEx.IsTrue_Probability((int)_TreeDistribLv, 12))
                    {
                        int rX, rZ;
                        RandEx.GetPt_OnCorner_Randomly(pt.x, pt.z, szR, szC, out rX, out rZ);
                        js.Inst.AddRpr(zn_, rX, rZ, global::rprs.eTree, (cel1l.Type)_TreeTypes,
                            new f4(Random.Range(0, 360), -Random.Range(0.0f, 0.7f),
                            Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1f)), js.SeriType.WithCell);
                    }
                    break;
            }

        }

        if (szR == 3 && szC == 4) return;
        if (szR == 4 && szC == 3) return;

        if (szR == 3 && szC == 5) {
            AddMoreProp_Randomly_35(zn_, ref lstRand);
            return;
        } else if (szR == 5 && szC == 3) {
            AddMoreProp_Randomly_53(zn_, ref lstRand);
            return;
        }
        else if (szR == 4 && szC == 4)
        {
            AddMoreProp_Randomly_Corner(zn_, ref lstRand, szR, szC);
            return;
        }
        else if (szR == 5 && szC == 5)
        {
            AddMoreProp_Randomly_Corner(zn_, ref lstRand, szR, szC);
            return;
        }

        //4x5 5x4
        // center
    }

    void AddMoreProp_Randomly_Corner(zone_ zn_, ref List<i2> lst, int szR, int szC)
    {
        for (int i = 0; i < lst.Count; ++i)
        {
            i2 pt = lst[i];

            int r = RandEx.Get01234();
            switch (r)
            {
                case 0:
                    if (core.zells[pt.x, pt.z].IsPath)
                        if (RandEx.IsTrue_Probability((int)_BoxDistribLv, 12))
                            js.Inst.AddRpr(zn_, pt.x + 1, pt.z + 1, global::rprs.eBox, cel1l.Type.Box, f4.O, js.SeriType.WithCell);
                        else if (RandEx.IsTrue_Probability((int)_TreeDistribLv, 14) )
                            js.Inst.AddRpr(zn_, pt.x + 1, pt.z + 1, global::rprs.eTree, (cel1l.Type)_TreeTypes,
                                new f4(Random.Range(0, 360), -Random.Range(0.0f, 0.7f),
                                Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1f)), js.SeriType.WithCell);
                    break;
                case 1:
                    if (core.zells[pt.x, pt.z+szC-1].IsPath)
                        if (RandEx.IsTrue_Probability((int)_BoxDistribLv, 12))
                            js.Inst.AddRpr(zn_, pt.x + 1, pt.z + szC - 2, global::rprs.eBox, cel1l.Type.Box, f4.O, js.SeriType.WithCell);
                        else if (RandEx.IsTrue_Probability((int)_TreeDistribLv, 14))
                            js.Inst.AddRpr(zn_, pt.x + 1, pt.z + szC - 2, global::rprs.eTree, (cel1l.Type)_TreeTypes,
                                new f4(Random.Range(0, 360), -Random.Range(0.0f, 0.7f),
                                Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1f)), js.SeriType.WithCell);
                    break;
                case 2:
                    if (core.zells[pt.x+szR-1, pt.z+ szC-1].IsPath)
                        if (RandEx.IsTrue_Probability((int)_BoxDistribLv, 12))
                            js.Inst.AddRpr(zn_, pt.x + szR - 2, pt.z + szC - 2, global::rprs.eBox, cel1l.Type.Box, f4.O, js.SeriType.WithCell);
                        else if (RandEx.IsTrue_Probability((int)_TreeDistribLv, 14))
                            js.Inst.AddRpr(zn_, pt.x + szR - 2, pt.z + szC - 2, global::rprs.eTree, (cel1l.Type)_TreeTypes,
                                new f4(Random.Range(0, 360), -Random.Range(0.0f, 0.7f),
                                Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1f)), js.SeriType.WithCell);
                    break;
                case 3:
                    if (core.zells[pt.x+szR-1, pt.z].IsPath)
                        if (RandEx.IsTrue_Probability((int)_BoxDistribLv, 12))
                            js.Inst.AddRpr(zn_, pt.x + szR - 2, pt.z + 1, global::rprs.eBox, cel1l.Type.Box, f4.O, js.SeriType.WithCell);
                        else if (RandEx.IsTrue_Probability((int)_TreeDistribLv, 14))
                            js.Inst.AddRpr(zn_, pt.x + szR - 2, pt.z + 1, global::rprs.eTree, (cel1l.Type)_TreeTypes,
                                new f4(Random.Range(0, 360), -Random.Range(0.0f, 0.7f),
                                Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1f)), js.SeriType.WithCell);
                    break;
            }

        }
    }

    void AddMoreProp_Randomly_53(zone_ zn_, ref List<i2> lst)
    {
        for (int i = 0; i < lst.Count; ++i)
        {
            i2 pt = lst[i];

            switch (RandEx.Get012())
            {
                case 0:
                    if (RandEx.IsTrue_Probability((int)_BoxDistribLv, 12))
                        js.Inst.AddRpr(zn_, pt.x + 2, pt.z + 1, global::rprs.eBox, cel1l.Type.Box, f4.O, js.SeriType.WithCell);
                    break;
                case 1:
                    if (RandEx.IsTrue_Probability((int)_TreeDistribLv, 12))
                        js.Inst.AddRpr(zn_, pt.x + 2, pt.z + 1, global::rprs.eTree, (cel1l.Type)_TreeTypes,
                            new f4(Random.Range(0, 360), -Random.Range(0.0f, 0.7f),
                            Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1f)), js.SeriType.WithCell);
                    break;
                case 2:
                    if (RandEx.IsTrue_Probability((int)_BoxDistribLv, 12))
                        js.Inst.AddRpr(zn_, pt.x + 2, pt.z + 1, global::rprs.eBox, cel1l.Type.Box, f4.O, js.SeriType.WithCell);

                    if (RandEx.IsTrue_Probability((int)_TreeDistribLv, 12))
                        js.Inst.AddRpr(zn_, pt.x + 2, pt.z + 1, global::rprs.eTree, (cel1l.Type)_TreeTypes,
                            new f4(Random.Range(0, 360), -Random.Range(0.0f, 0.7f),
                            Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1f)), js.SeriType.WithCell);
                    break;
            }

        }
    }

    void AddMoreProp_Randomly_35(zone_ zn_, ref List<i2> lst)
    {
        for (int i = 0; i < lst.Count; ++i) {
            i2 pt = lst[i];

            switch (RandEx.Get012())
            {
                case 0:
                    if (RandEx.IsTrue_Probability((int)_BoxDistribLv, 12))
                        js.Inst.AddRpr(zn_, pt.x + 1, pt.z + 2, global::rprs.eBox, cel1l.Type.Box, f4.O, js.SeriType.WithCell);
                    break;
                case 1:
                    if (RandEx.IsTrue_Probability((int)_TreeDistribLv, 12))
                        js.Inst.AddRpr(zn_, pt.x + 1, pt.z + 2, global::rprs.eTree, (cel1l.Type)_TreeTypes,
                            new f4(Random.Range(0, 360), -Random.Range(0.0f, 0.7f),
                            Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1f)), js.SeriType.WithCell);
                    break;
                case 2:
                    if (RandEx.IsTrue_Probability((int)_BoxDistribLv, 12))
                        js.Inst.AddRpr(zn_, pt.x + 1, pt.z + 2, global::rprs.eBox, cel1l.Type.Box, f4.O, js.SeriType.WithCell);

                    if (RandEx.IsTrue_Probability((int)_TreeDistribLv, 12))
                        js.Inst.AddRpr(zn_, pt.x + 1, pt.z + 2, global::rprs.eTree, (cel1l.Type)_TreeTypes,
                            new f4(Random.Range(0, 360), -Random.Range(0.0f, 0.7f),
                            Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1f)), js.SeriType.WithCell);
                    break;
            }

        }
    }

    enum BoxDistribLv
    {
        // Lv2_12=2,
    //    Lv3_12 = 3,
    //    Lv4_12 = 4,
        Lv6_12 = 6,
        Lv8_12 = 8,
        Lv9_12 = 9, // 9/12
    }

    enum TreeDistribLv
    {
        // Lv2_12=2,
      //  Lv3_12 = 3,
     //   Lv4_12 = 4,
        Lv6_12 = 6,
        Lv8_12 = 8,
        Lv9_12 = 9, // 9/12
    }

    enum TreeTypes
    {
        Tree1A_Blue = cel1l.Type.Tree1A_Blue,
        Tree1B_Pink,
        Tree2A_Blue,
        Tree2B_Pink,
        Tree3A_Red,
        Tree3B_Green,
        Tree4A_Red,
        Tree4B_Green,
    }


    public bool AddFire(zone_ zn, i2 pt, cel1l.Type type)
    {
        cel1l cell = core.zells[pt];
        if (cell.type == cel1l.Type.Tile)
        {
            js.Inst.AddRpr(zn, pt.x, pt.z, rprs.eTrap, type,
                new f4(0, Random.Range(0.27f, 0.31f), Random.Range(0.0f, 6.3f)), SeriType.WithCell);
            return true;
        }
        return false;
    }
}
