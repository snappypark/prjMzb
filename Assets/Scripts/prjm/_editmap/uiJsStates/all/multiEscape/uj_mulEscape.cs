using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

public partial class uj_mulEscape : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState._AllMultiEscape; } }
    
    public void Generate()
    {
        js.Inst.Clear();
        core.ClearZz();
        js.Inst.info.soloType = loads.TypeBg.Default;
        genA_1();
        FileIO.Local.Write("m0", js.Inst.CreateJSON(), "txt");


        js.Inst.Clear();
        core.ClearZz();
        js.Inst.info.soloType = loads.TypeBg.Default;
        genA_2();
        FileIO.Local.Write("m1", js.Inst.CreateJSON(), "txt");

        js.Inst.Clear();
        core.ClearZz();
        js.Inst.info.soloType = loads.TypeBg.Default;
        genB_1();
        FileIO.Local.Write("m2", js.Inst.CreateJSON(), "txt");

        js.Inst.Clear();
        core.ClearZz();
        js.Inst.info.soloType = loads.TypeBg.Default;
        genB_2();
        FileIO.Local.Write("m3", js.Inst.CreateJSON(), "txt");

        Debug.Log("VCZX");
    }


    static zmSz[] _zmSzs = new zmSz[] {
        zmSz.s48, zmSz.s49, zmSz.s57, zmSz.s57, zmSz.s58};

    zmSZ getZmSZ()
    {
        int idx0 = Random.Range(0, _zmSzs.Length);
        int idx1 = Random.Range(0, _zmSzs.Length);
        return new zmSZ(_zmSzs[idx0], _zmSzs[idx1]);
    }
}
