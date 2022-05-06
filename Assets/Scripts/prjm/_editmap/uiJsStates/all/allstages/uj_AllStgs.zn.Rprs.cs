using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

public partial class uj_AllStgs
{
    void generateRprs(js.zone_ zn, int szR, int szC)
    {
        js.Inst.AddRprRandomly(zn, szR, szC);

        for (int i = 0; i < zn.wallsDel.Count; ++i)
        {
            js.wall_Del_ wd = zn.wallsDel[i];
            for (int x = wd.bd.X0; x <= wd.bd.X1; ++x)
                for (int z = wd.bd.Z0; z <= wd.bd.Z1; ++z)
                    js.Inst.RemoveRpr(zn, x, z);
        }
        
    }


}
