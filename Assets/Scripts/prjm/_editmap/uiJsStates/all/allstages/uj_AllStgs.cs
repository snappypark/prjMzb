using System.Collections;
using System;
using UnityEngine;
using UnityEngineEx;

public partial class uj_AllStgs : uj_abs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState._AutoAllStages; } }

    float _startTime;

    public void MakeStages()
    {
        INFO.edxIn100 = INFO.idxIn100 % 100;
        INFO.idxX100 = INFO.edxX100 = (int)(INFO.idxIn100 / 100);
        StartCoroutine(generateStages_());
    }

    public void MakeMultilpleStages()
    {
        StartCoroutine(generateStages_());
    }

    mats _mats;
    IEnumerator generateStages_()
    {
        startGenerate();
        while (INFO.Check_CurIdx())
        {
            int idxIn100 = INFO.BeginCurIdx();

            clearForReset(true);

            zmSZ[] zmsz = new zmSZ[] { getZmSZ(0, idxIn100), getZmSZ(1, idxIn100), getZmSZ(2, idxIn100), getZmSZ(3, idxIn100) };
            _mats = getMats(idxIn100);
            
            ResetStageInfo(idxIn100);

            Reset_ZnRt_Znm(idxIn100, zmsz[0]);

            addEntryZone(zmsz[0]);
            addTunnelAndZone(_znRts[0], _znms[0], zmsz[1], true);
            addTunnelAndZone(_znRts[1], _znms[1], zmsz[2]);
            addTunnelAndZone(_znRts[2], _znms[2], zmsz[3]);
            addEndry(_znRts[3], _znms[3], zmsz[3]);

            for (int i = 0; i < 4; ++i)
                yield return generateZone(js.Inst.zones[1 + i * 2], _znms[i], zmsz[i]);
            
            digWallsOfZn(true, true, true, true);

            for (int i = 0; i < 4; ++i)
                js.Inst.AddZprp_ArrowByExit(js.Inst.zones[1 + i * 2]);

            setRemainSec();
            
            int fileName = INFO.GetCurIdx();
            FileIO.Local.Write(string.Format("s{0}", fileName), js.Inst.CreateJSON(), "txt");

            INFO.End_CurIdx();
        }
        yield return null;
        endGenerate();
    }

    void setRemainSec()
    {
        js.Inst.info.remainSec = 30 + (int)(INFO.dist * 0.25f);
        //Debug.Log(string.Format("s{0}: {1}sec", editAllStgs.GetCurIdx(), js.Inst.info.remainSec));
    }

    void startGenerate()
    {
        Debug.Log("NEADI ");
        Application.targetFrameRate = 210;
        _startTime = Time.time;
    }

    void endGenerate()
    {
        Application.targetFrameRate = 32;
        Debug.Log("IDAEN " + (Time.time - _startTime) / 60 + " min");
        clearForReset();
        audios.Inst.PlaySound(audios.eSoundType.victory, 0.6f);
    }

    void clearForReset(bool gc = false)
    {
        js.Inst.Clear();
        core.ClearZz();
        if (gc) GC.Collect();
    }

    #region stageInfo
    void ResetStageInfo(int stageIdx)
    {
        if (stageIdx % 5 == 2)
            js.Inst.info.soloType = loads.TypeBg.Dark;
        else if (stageIdx % 5 == 4)
            js.Inst.info.soloType = loads.TypeBg.DarkAndRoll;
        else
            js.Inst.info.soloType = loads.TypeBg.Default;
    }
    #endregion

    #region mats

    mats getMats(int stageIdx)
    {
        int by100 = stageIdx % 100;
        int tileBy18 = Mathf.Clamp(1 + (int)(by100 / 24), 0, 4); // 4*5
        int by3 = Mathf.Clamp((int)(by100 / 4), 0, 28); 

        switch (by100)
        {
            case 97: case 98: case 99:
                return new mats(matTiles.Gray, matWalls.DarkDark);
        }

        //if(by100 % 6 == 3)
        //    return new mats(matTiles.Gray, (byte)by3);
        return new mats((byte)tileBy18, (byte)by3);
    }

    struct mats
    {
        public byte tile, wall;
        public mats(byte tile_, byte wall_)
        {
            tile = tile_; wall = wall_;
        }
    }


    #endregion

    public class INFO
    {
        public static bool[] checkZones = new bool[4] { false, false, false, false };

        public static int GetCurIdx() { return idxIn100 + idxX100 * 100; }
        public static int idxIn100 = 0, edxIn100 = 99;
        public static int idxX100 = 0, edxX100 = 9;

        public static bool Check_CurIdx()
        {
            return idxIn100 <= edxIn100 && idxX100 <= edxX100;
        }

        public static void End_CurIdx()
        {
            if (++idxIn100 > edxIn100)
            {
                idxIn100 = 0;
                ++idxX100;
            }
        }

        public static float dist = 0.0f;
        public static int iBy6;

        public static int BeginCurIdx()
        {
            iBy6 = idxIn100 % 6;
            dist = 0.0f;

            int i = iBy6;

            _bwall = new info_(1, i==1 || i==3, RandEx.Get(3, 5));

            _bush = new info_(1, i==2 || i==4 || i==5, RandEx.Get(3, 5));

            _trap = new info_(1 + RandEx.GetN(4) + (int)(idxIn100 * 0.1f) + (int)(idxX100 * 2.0f), i!=0);

            _meteor = new info_(1, i==1 || i==3 || i==5, RandEx.Get(3, 5));
            _meteor2 = new info_(1, i==2 || i==4);

            return idxIn100;
        }
    }

    static info_ _bwall;
    static info_ _bush;

    // rpr?
    static info_ _trap;
    static info_ _meteor, _meteor2;
    //npc0
    //npc1

    struct info_
    {
        public int Max;
        public bool Active;
        public int ZnIdx;
        public info_(int max=0, bool active=false, int znIdx=0)
        {
            Max = max;
            Active = active;
            ZnIdx = znIdx;
        }
    }
}
