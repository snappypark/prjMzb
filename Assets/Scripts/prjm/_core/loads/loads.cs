using System.Collections;
using UnityEngine;
using UnityEngineExJSON;

public partial class loads : nj.MonoSingleton<loads>
{
    public enum TypeBg {
        ByJs = -1,
        Default = 0,
        DarkAndRoll,
        Dark,
        Dark2,
        Menu,
    }

    TypeBg _soloType;
    bool _load_Zbg = true;

    public void SetBgsFromJson(bool refresh = true)
    {
        switch (_soloType)
        {
            case TypeBg.DarkAndRoll:
                audios.Inst.PlayMusic(audios.eMusicType.rock1);
                core.sights.SetAlpha(0);
                cams.Inst.SetBgColor(new Color(0, 0, 0));
                audios.Inst.SetNeedToPlayDefaultMusic();
                break;

            case TypeBg.Menu:
                audios.Inst.PlayMazeMusic(audios.eMusicType.maze);
                core.sights.SetAlpha(0.35f);
                cams.Inst.SetBgColorSky();
                break;
                
            case TypeBg.Dark2:
                audios.Inst.PlayMazeMusic(audios.eMusicType.maze);
                core.sights.SetAlpha(0);
                cams.Inst.SetBgColor(new Color(0, 0, 0));
                break;
            case TypeBg.Dark:
                audios.Inst.PlayMazeMusic(audios.eMusicType.maze);
                core.sights.SetAlpha(0.27f);
                cams.Inst.SetBgColor(new Color(0, 0, 0));
                break;
            case TypeBg.Default:
                audios.Inst.PlayMazeMusic(audios.eMusicType.maze);
                core.sights.SetAlpha(sights.alpha);
                cams.Inst.SetBgColorSky();
                break;
        }
        if(refresh)
            core.sights.trSs.Refresh();
    }

    public void SetBgsMusic()
    {
        switch (_soloType)
        {
            case TypeBg.DarkAndRoll:
                audios.Inst.PlayMusic(audios.eMusicType.rock1);
                audios.Inst.SetNeedToPlayDefaultMusic();
                break;

            case TypeBg.Dark2:
                audios.Inst.PlayMazeMusic(audios.eMusicType.maze);
                break;
            case TypeBg.Dark:
                audios.Inst.PlayMazeMusic(audios.eMusicType.maze);
                break;
            case TypeBg.Default:
                audios.Inst.PlayMazeMusic(audios.eMusicType.maze);
                break;
        }
    }


    public IEnumerator FromJson_(string strJs, TypeBg bgType, bool usdRandJs = true)
    {
        JSONObject jsMap = new JSONObject(strJs)[jsK.Map];
        jsK.ObjInfo = jsMap[jsK.Info];
        jsK.ObjZones = jsMap[jsK.Zones];
        jsK.ObjPrps = jsMap[jsK.Prps];
        jsK.ObjNpcs = jsMap[jsK.Npcs];
        jsK.ObjSpwan = jsMap[jsK.Spawns];

        if (bgType == TypeBg.ByJs)
            _soloType = (TypeBg)jsK.ObjInfo["solotype"].i;
        else
            _soloType = bgType;

        switch (_soloType) {
            case TypeBg.DarkAndRoll:
            case TypeBg.Dark:
            case TypeBg.Dark2:
                _load_Zbg = false;
                break;
            case TypeBg.Default:
                _load_Zbg = true;
                break;
        }

        // zone
        for (byte i = 0; i < jsK.ObjZones.Count; ++i)  {
            jsArr arJs = new jsArr(jsK.ObjZones[i]);
            zone zn = core.zones.AddZone(arJs.Int, arJs.Int, arJs.Int, arJs.Int, arJs.Int, (sights.eState)arJs.Int, true);  }
        core.zones.Reset_AllWays();


        // zone
        for (byte i = 0; i < jsK.ObjZones.Count; ++i)
        {
            jsArr arJs = new jsArr(jsK.ObjZones[i]);
            zone zn = core.zones[i];
            arJs.SetIdx(6);

            // tile
            JSONObject jsObj = arJs.Obj;

            for (int j = 0; j < jsObj.Count; ++j)
            {
                jsArr arJs2 = new jsArr(jsObj[j]);
                zn.AddOption((zone.Option)arJs2.Int);
            }

            ///*
            jsObj = arJs.Obj;
            for (int j = 0; j < jsObj.Count; ++j)
            {
                jsArr arJs2 = new jsArr(jsObj[j]);
                zn.AddTile_l1lRot45(new f4(arJs2.F, arJs2.F, arJs2.F, arJs2.F), (byte)arJs2.Int);
            }
            //*/
            jsObj = arJs.Obj;
            for (int j = 0; j < jsObj.Count; ++j)
            {
                jsArr arrJs2 = new jsArr(jsObj[j]);
                zn.AddTile_l1l(arrJs2.Int, arrJs2.Int, arrJs2.Int, arrJs2.Int, (byte)arrJs2.Int);
            }
            jsObj = arJs.Obj;
            for (int j = 0; j < jsObj.Count; ++j)
            {
                jsArr arrJs2 = new jsArr(jsObj[j]);
                zn.AddTile_l5l(arrJs2.Int, arrJs2.Int, arrJs2.Int, arrJs2.Int, (byte)arrJs2.Int);
            }
            jsObj = arJs.Obj;
            for (int j = 0; j < jsObj.Count; ++j)
            {
                jsArr arrJs2 = new jsArr(jsObj[j]);
                zn.AddTile_lTl(arrJs2.Int, arrJs2.Int, arrJs2.Int, arrJs2.Int, (byte)arrJs2.Int);
            }

            // wall
            loadMazes(zn, arJs.Obj, usdRandJs);
            yield return null;
            loadWallRects(zn, arJs.Obj);
            loadWallLines(zn, arJs.Obj);
            yield return null;
            loadWalls(zn, arJs.Obj);
            loadWaycalls(zn, arJs.Obj);
            yield return null;

            // zprop
            loadZprps(zn, arJs.Obj);

            // npc
            loadNpcs(zn, arJs.Obj);
            loadZbgs(zn, arJs.Obj);

            loadRprs(zn, arJs.Obj);
        }

        // spawns
        ctrls.Inst.ClearSpawns();
        for (byte i = 0; i < jsK.ObjSpwan.Count; ++i)
        {
            jsArr arJs = new jsArr(jsK.ObjSpwan[i]);
            ctrls.Inst.AddSpawn(arJs.Byte,
                arJs.F, arJs.F, arJs.F,
                arJs.F, arJs.F, arJs.F,
                arJs.Int, arJs.Int, arJs.Int);
        }
        
        zone startzone = core.zones[ctrls.Inst.GetSpawn(0).ZoneIdx];
        core.zones.OnUpdate_NextZone(startzone, false);

        yield return core.zones.InitJsps_();

        core.stages.SetTimes((float)jsK.ObjInfo["dur-sec"].i);
    }


    void loadMazes(zone zn, JSONObject jsMazes, bool usdRandJs)
    {
        for (int i = 0; i < jsMazes.Count; ++i)
        {
            jsArr arrJs = new jsArr(jsMazes[i]);
            i4 bd = new i4(arrJs.Int, arrJs.Int, arrJs.Int, arrJs.Int);
            i2 szRC = new i2(arrJs.Int, arrJs.Int);
            i2 numRC = new i2(arrJs.Int, arrJs.Int);
            zn.mats = wal4Mat.N(arrJs.Byte, arrJs.Byte, arrJs.Byte, arrJs.Byte);
            int type = arrJs.Int;
            int rand0 = arrJs.Int; int option0 = arrJs.Int;
            
            zn.AddMazeWalls(bd,
                szRC.v1, szRC.v2, numRC.v1, numRC.v2,
                zn.mats, (cel1l.Type)type,
                rand0, option0, usdRandJs);
        }
    }

    void loadWallRects(zone zn, JSONObject jsWallsRect)
    {
        for (int i = 0; i < jsWallsRect.Count; ++i)
        {
            jsArr arrJs = new jsArr(jsWallsRect[i]);
            zn.AddRectWalls(new i4(arrJs.Int, arrJs.Int, arrJs.Int, arrJs.Int),
                         wal4Mat.N(arrJs.Byte, arrJs.Byte, arrJs.Byte, arrJs.Byte),
                         (cel1l.Type)arrJs.Int);
        }
    }

    void loadWallLines(zone zn, JSONObject jsWallsLine)
    {
        for (int i = 0; i < jsWallsLine.Count; ++i)
        {
            jsArr arrJs = new jsArr(jsWallsLine[i]);
            zn.AddWallLines(new i4(arrJs.Int, arrJs.Int, arrJs.Int, arrJs.Int),
                         wal4Mat.N(arrJs.Byte, arrJs.Byte, arrJs.Byte, arrJs.Byte),
                         (cel1l.Type)arrJs.Int);
        }
    }

    void loadWalls(zone zn, JSONObject jsWalls)
    {
        for (int i = 0; i < jsWalls.Count; ++i)
        {
            jsArr arJs2 = new jsArr(jsWalls[i]);
            zn.AddWall(arJs2.Int, arJs2.Int,
                wal4Mat.N(arJs2.Byte, arJs2.Byte, arJs2.Byte, arJs2.Byte), (cel1l.Type)arJs2.Int);
        }
    }

    void loadWaycalls(zone zn, JSONObject jsWalls)
    {
        for (int i = 0; i < jsWalls.Count; ++i)
        {
            jsArr arJs2 = new jsArr(jsWalls[i]);
            zn.AddWay(arJs2.Int, arJs2.Int, arJs2.Int, arJs2.Int);
        }
    }

    void loadZprps(zone zn, JSONObject jsZprps)
    {
        for (int i = 0; i < jsZprps.Count; ++i)
        {
            jsArr arJs2 = new jsArr(jsZprps[i]);
            zn.AddPrp(arJs2.Int, arJs2.Int, arJs2.Int, arJs2.Int,
                (byte)arJs2.Int, (cel1l.Type)arJs2.Int,
                new f4(arJs2.F, arJs2.F, arJs2.F, arJs2.F));
        }
    }

    void loadNpcs(zone zn, JSONObject jsNpcs)
    {
        for (int i = 0; i < jsNpcs.Count; ++i)
        {
            jsArr arJs = new jsArr(jsNpcs[i]);
            Vector3 pos = new Vector3(arJs.F, arJs.F, arJs.F);
            cel1l cell = core.zells[cel1l.Pt(pos)];
            if (!cell.IsPath)
                continue;
            core.unitClones.PreLoadNpc(zn,
                pos,
                new Vector3(arJs.F, arJs.F, arJs.F),
                (model.eType)arJs.Long, arJs.Byte, (model.Equip)arJs.Long,
                arJs.Byte, arJs.Byte, arJs.Byte, arJs.Byte, arJs.Byte,
                (unit.SubType)arJs.Long);
        }
    }

    void loadZbgs(zone zn, JSONObject jsObjs)
    {
        for (int i = 0; i < jsObjs.Count; ++i)
        {
            jsArr arJs = new jsArr(jsObjs[i]);
            byte type = arJs.Byte;

            if (!_load_Zbg && type == zbgs.eCloud)
                continue;

            zn.AddBgs(type, new Vector3(arJs.F, arJs.F, arJs.F), new f4(arJs.F, arJs.F, arJs.F, arJs.F));
        }
    }

    void loadRprs(zone zn, JSONObject jsObjs)
    {
        for (int i = 0; i < jsObjs.Count; ++i)
        {
            jsArr arJs = new jsArr(jsObjs[i]);
            zjs.rprs.Add(arJs.Int, arJs.Int,
                (byte)arJs.Int, (cel1l.Type)arJs.Int,
                new f4(arJs.F, arJs.F, arJs.F, arJs.F));
        }
    }

    public void ResetBgType()
    {
        audios.Inst.SetNeedToPlayDefaultMusic();
        audios.Inst.StopMusic();
        core.sights.SetAlpha(sights.alpha);
        cams.Inst.SetBgColorSky();
    }
    public IEnumerator FromJsonSpawn_(string strJs)
    {
        JSONObject jsMap = new JSONObject(strJs)[jsK.Map];
        jsK.ObjSpwan = jsMap[jsK.Spawns];
        ctrls.Inst.ClearSpawns();
        for (byte i = 0; i < jsK.ObjSpwan.Count; ++i)
        {
            jsArr arJs = new jsArr(jsK.ObjSpwan[i]);
            ctrls.Inst.AddSpawn(arJs.Byte,
                arJs.F, arJs.F, arJs.F,
                arJs.F, arJs.F, arJs.F,
                arJs.Int, arJs.Int, arJs.Int);
        }

        yield return null;
    }
}
