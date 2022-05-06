using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class uj_mulBattle : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState._AllMultiBattle; } }

    public void Generate()
    {
        js.Inst.Clear();
        core.ClearZz();

        js.Inst.info.soloType = loads.TypeBg.Default;

        genA();

        // 
        int fileName = 0;
        FileIO.Local.Write(string.Format("b{0}", fileName), js.Inst.CreateJSON(), "txt");
        Debug.Log("VCZX");
    }



    static zmSz[] _zmSzs = new zmSz[] {

        zmSz.s46, zmSz.s47, zmSz.s55,
        zmSz.s49, zmSz.s57};

    zmSZ getZmSZ()
    {
        int idx0 = Random.Range(_zmSzs.Length - 5, _zmSzs.Length - 2);
        int idx1 = Random.Range(_zmSzs.Length - 5, _zmSzs.Length - 2);
        return new zmSZ(_zmSzs[idx0], _zmSzs[idx1]);
    }

    zmSZ getZmSZCenter()
    {
        int idx0 = Random.Range(_zmSzs.Length-2, _zmSzs.Length);
        int idx1 = Random.Range(_zmSzs.Length-2, _zmSzs.Length);
        return new zmSZ(_zmSzs[idx0], _zmSzs[idx1]);
    }
}
