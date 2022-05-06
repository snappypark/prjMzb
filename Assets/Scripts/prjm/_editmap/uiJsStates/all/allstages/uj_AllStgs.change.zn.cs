using System.Collections;
using UnityEngine;

public partial class uj_AllStgs
{
    public void ReMakeZone()
    {
        INFO.edxIn100 = INFO.idxIn100 % 100;
        INFO.idxX100 = INFO.edxX100 = (int)(INFO.idxIn100 / 100);
        StartCoroutine(remakeMaze_());
    }

    IEnumerator remakeMaze_()
    {
        startGenerate();
        clearForReset();

        int idxIn100 = INFO.BeginCurIdx();

        js.Inst.ReadJSON(core.stages.GetJson(INFO.GetCurIdx()), js.SeriType.WithCell);

        zmSZ[] zmsz = new zmSZ[] { getZmSZ2(0, idxIn100), getZmSZ2(1, idxIn100), getZmSZ2(2, idxIn100), getZmSZ2(3, idxIn100) };
        _mats = getMats(idxIn100);
        initZmz(zmsz);

        for (int i = 0; i < 4; ++i)
            if (INFO.checkZones[i])
                yield return generateZone(js.Inst.zones[i * 2 + 1], _znms[i], zmsz[i]);
            else
                yield return generateTime(js.Inst.zones[i * 2 + 1], _znms[i], zmsz[i]);

        digWallsOfZn(INFO.checkZones[0], INFO.checkZones[1], INFO.checkZones[2], INFO.checkZones[3]);

        setRemainSec();

        string stageFileName = core.stages.GetName(INFO.GetCurIdx());
        FileIO.Local.Write(stageFileName, js.Inst.CreateJSON(), "txt");

        endGenerate();
    }


    void initZmz(zmSZ[] zmsz)
    {
        _znms.Clear();

        for (int i = 0; i < 4; ++i)
        {
            js.zone_ zn = js.Inst.zones[1 + i * 2];
            _znms.Add(new znm(zn.bd.i4, zmsz[i]));
        }
    }


    zmSZ getZmSZ2(int znIdx, int stageIdx)
    {
        int by5 = stageIdx % 5;
        zmSz zmSz0 = _zmSzs[INFO.idxX100 + (stageIdx / 5)];
        zmSz zmSz1 = _zmSzs[INFO.idxX100 + (stageIdx / 5) + 1];

        if (znIdx == 0 || by5 == 0) // n1, n1, n1, n1
            return getZmN1_2(zmSz0, zmSz1, js.Inst.zones[1 + znIdx * 2]);
        switch (by5)
        {
            case 1: // n1, 11, n1, n1
                switch (znIdx)
                {
                    case 1: return new zmSZ(zmSz1, zmSz1);
                    case 2: return getZmN1_2(zmSz0, zmSz1, js.Inst.zones[1 + znIdx * 2]);
                    case 3: return getZmN1_2(zmSz0, zmSz1, js.Inst.zones[1 + znIdx * 2]);
                }
                break;
            case 2: // n1, n1, 11, 11
                switch (znIdx)
                {
                    case 1: return getZmN1_2(zmSz0, zmSz1, js.Inst.zones[1 + znIdx * 2]);
                    case 2: return new zmSZ(zmSz1, zmSz1);
                    case 3: return new zmSZ(zmSz1, zmSz1);
                }
                break;
            case 3: // n1, 11, 11, n1
                switch (znIdx)
                {
                    case 1: return new zmSZ(zmSz1, zmSz1);
                    case 2: return new zmSZ(zmSz1, zmSz1);
                    case 3: return getZmN1_2(zmSz0, zmSz1, js.Inst.zones[1 + znIdx * 2]);
                }
                break;
            case 4: // n1, 11, 11, 11
                return new zmSZ(zmSz1, zmSz1);
        }
        return getZmN1_2(zmSz0, zmSz1, js.Inst.zones[1 + znIdx * 2]);
    }

    zmSZ getZmN1_2(zmSz zmSz0, zmSz zmSz1, js.zone_ zn)
    {
        return zn.bd.GapX == zmSz0.totalGap ? new zmSZ(zmSz0, zmSz1) : new zmSZ(zmSz1, zmSz0);
    }
}
