using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class uj_AllStgs
{
    public enum ReMakeStagesType
    {
        Default,
        ChangeAllFormat_FrStages,
        ChangeAllFormat_FrLocal,
        Check_FrStages,
        Check_FrLocal,
    }

    public void ChangeInfoes()
    {
        StartCoroutine(changeInfoes_());
    }

    IEnumerator changeInfoes_()
    {
        startGenerate();

        while (INFO.Check_CurIdx())
        {
            int idxIn100 = INFO.BeginCurIdx();

            clearForReset();

            js.Inst.ReadJSON(core.stages.GetJson(INFO.GetCurIdx()), js.SeriType.WithCell);
            
            zmSZ[] zmsz = new zmSZ[] { getZmSZ2(0, idxIn100), getZmSZ2(1, idxIn100), getZmSZ2(2, idxIn100), getZmSZ2(3, idxIn100) };
            initZmz(zmsz);

            for (int i = 0; i < 4; ++i)
                yield return generateTime(js.Inst.zones[1 + i * 2], _znms[i], zmsz[i]);

            setRemainSec();

            string stageFileName = core.stages.GetName(INFO.GetCurIdx());
            FileIO.Local.Write(stageFileName, js.Inst.CreateJSON(), "txt");

            INFO.End_CurIdx();
        }
        yield return null;
        endGenerate();
    }

    IEnumerator generateTime(js.zone_ zn, znm zm, zmSZ zms)
    {
        cnd[,] cellnodes = new cnd[zms.numGapR, zms.numGapC];
        initCellNodes(zn, ref cellnodes, zm.bd, zms.szGapR, zms.szGapC, zms.numGapR, zms.numGapC);

        yield return null;
    }

    public void ChangeAllJsFormat_FrStages()
    {
        StartCoroutine(changeAllJsFormat_FrStages_());
    }

    IEnumerator changeAllJsFormat_FrStages_()
    {
        startGenerate();

        while (INFO.Check_CurIdx())
        {
            INFO.BeginCurIdx();

            clearForReset();

            int idx = INFO.GetCurIdx();
            string str = core.stages.GetJson(idx);
            js.Inst.ReadJSON(str, js.SeriType.Default);


           // for (int i = 0; i < 4; ++i)
           //     js.Inst.zones[1 + i * 2].options.Clear();
           // for (int i = 0; i < 4; ++i)
            //    generateMeteor(js.Inst.zones[1 + i * 2]);

            FileIO.Local.Write(core.stages.GetName(idx), js.Inst.CreateJSON(), "txt");

            INFO.End_CurIdx();
            yield return null;
        }
        endGenerate();
    }

    public void ChangeAllJsFormat_FrLocal()
    {
        StartCoroutine(changeAllJsFormat_FrLocal_());
    }

    IEnumerator changeAllJsFormat_FrLocal_()
    {
        startGenerate();
        while (INFO.Check_CurIdx())
        {
            INFO.BeginCurIdx(); 
            clearForReset();

            int idx = INFO.GetCurIdx();
            string str = FileIO.Local.Read(string.Format("s{0}", idx), "txt");
            js.Inst.ReadJSON(str, js.SeriType.Default);

            //todo

            FileIO.Local.Write(core.stages.GetName(idx), js.Inst.CreateJSON(), "txt");

            INFO.End_CurIdx();
            yield return null;
        }
        endGenerate();
    }

    public void CheckAllJsFormat_FrLocal()
    {
        startGenerate();
        while (INFO.Check_CurIdx())
        {
            INFO.BeginCurIdx();

            clearForReset();
            
            string str = FileIO.Local.Read(string.Format("s{0}", INFO.GetCurIdx()), "txt");
            js.Inst.ReadJSON(str, js.SeriType.Default);
            
            //todo

            INFO.End_CurIdx();
        }
        endGenerate();
    }
}
