using UnityEngine;
using UnityEngineExJSON;

public partial class js /*.parse.read*/
{
    public void ReadJSON(string strJson, SeriType seriType)
    {
        JSONObject jsMap = new JSONObject(strJson)[jsK.Map];
        jsK.ObjInfo = jsMap[jsK.Info];
        jsK.ObjZones = jsMap[jsK.Zones];
        jsK.ObjPrps = jsMap[jsK.Prps];
        jsK.ObjNpcs = jsMap[jsK.Npcs];
        jsK.ObjSpwan = jsMap[jsK.Spawns];

        info.mode = (multis.Mode)jsK.ObjInfo["mode"].i;
        info.soloType = (loads.TypeBg)jsK.ObjInfo["solotype"].i;
        
        info.remainSec = (int)jsK.ObjInfo["dur-sec"].i;

        Clear();

        for (byte i = 0; i < jsK.ObjZones.Count; ++i) {
            jsArr arJs = new jsArr(jsK.ObjZones[i]);
            zone_ zn_ = AddZone(arJs.Int, arJs.Int, arJs.Int, arJs.Int, arJs.Int, arJs.Int, seriType); }
        core.zones.Reset_AllWays();

        for (byte i = 0; i < jsK.ObjZones.Count; ++i)
        {
            jsArr arJs = new jsArr(jsK.ObjZones[i]);
            zone_ zn_ = zones[i];
            arJs.SetIdx(6);

            // tile
            JSONObject jsObj = arJs.Obj;

            for (int j = 0; j < jsObj.Count; ++j)
            {
                jsArr arJs2 = new jsArr(jsObj[j]);
                zn_.options.Add((zone.Option)arJs2.Int);
            }

            ///*
            jsObj = arJs.Obj;
            for (int j = 0; j < jsObj.Count; ++j) {
                jsArr arJs2 = new jsArr(jsObj[j]);
                AddTileR(zn_, arJs2.F, arJs2.F, arJs2.F, arJs2.F, (byte)arJs2.Int, seriType);
            }
            //*/
            jsObj = arJs.Obj; for (int j = 0; j < jsObj.Count; ++j) {
                jsArr arJs2 = new jsArr(jsObj[j]);
                AddTile1(zn_, arJs2.Int, arJs2.Int, arJs2.Int, arJs2.Int, (byte)arJs2.Int, seriType);
            }
            jsObj = arJs.Obj; for (int j = 0; j < jsObj.Count; ++j) {
                jsArr arJs2 = new jsArr(jsObj[j]);
                AddTile5(zn_, arJs2.Int, arJs2.Int, arJs2.Int, arJs2.Int, (byte)arJs2.Int, seriType);
            }
            jsObj = arJs.Obj;
            for (int j = 0; j < jsObj.Count; ++j) {
                jsArr arJs2 = new jsArr(jsObj[j]);
                AddTileT(zn_, arJs2.Int, arJs2.Int, arJs2.Int, arJs2.Int, (byte)arJs2.Int, seriType);
            }

            // wall
            loadMazes(zn_, arJs.Obj, seriType);
            loadWallRects(zn_, arJs.Obj, seriType);
            loadWallLines(zn_, arJs.Obj, seriType);
            loadWalls(zn_, arJs.Obj, seriType);

            loadWallsDel(zn_, arJs.Obj, seriType);

            loadZprps(zn_, arJs.Obj, seriType);
            loadNpcs(zn_, arJs.Obj, seriType);
            loadZbgs(zn_, arJs.Obj, seriType);
            loadRprs(zn_, arJs.Obj, seriType);
        }
        //Debug.Log("prp: " + props.Count);

        // spawns
        spawns.Clear();
        for (byte i = 0; i < jsK.ObjSpwan.Count; ++i)
        {
            jsArr arJs = new jsArr(jsK.ObjSpwan[i]);

            AddSpawn(
                arJs.Byte,
                arJs.F, arJs.F, arJs.F,
                arJs.F, arJs.F, arJs.F,
                arJs.Int, arJs.Int, arJs.Int, seriType);
        }//*/

        if (seriType != SeriType.WithLoad)
            return;

        if (spawns.Count == 0)
            AddSpawn(0, core.zones[0].bd.X0, 0.5f, core.zones[0].bd.Z0, 0, 0, 1, 0, 0, 0, seriType);
        
        zone startzone = core.zones[ctrls.Inst.GetSpawn(0).ZoneIdx];
        core.zones.OnUpdate_NextZone(startzone, false);
        ctrls.Inst.SpawnOnEdit(spawns[0].pos.x, spawns[0].pos.z);
    }

    void loadMazes(zone_ zn, JSONObject jsMazes, SeriType seriType)
    {
        for (int i = 0; i < jsMazes.Count; ++i)
        {
            jsArr arrJs = new jsArr(jsMazes[i]);
            i4 bd = new i4(arrJs.Int, arrJs.Int, arrJs.Int, arrJs.Int);
            i2 szRC = new i2(arrJs.Int, arrJs.Int);
            i2 numRC = new i2(arrJs.Int, arrJs.Int);
            wal4Mat mats = wal4Mat.N(arrJs.Byte, arrJs.Byte, arrJs.Byte, arrJs.Byte);
            int type = arrJs.Int;
            int rand0 = arrJs.Int; int option0 = arrJs.Int;
            
            uj_abs.matWall = (matWalls.Type)mats.Top;
            AddWallsMaze(zn, bd, szRC, numRC, mats,
                (cel1l.Type)type, rand0, option0, seriType);
        }
    }

    void loadWallRects(zone_ zn, JSONObject jsWallsRect, SeriType seriType)
    {
        for (int i = 0; i < jsWallsRect.Count; ++i)
        {
            jsArr arrJs = new jsArr(jsWallsRect[i]);
            i4 bd = new i4(arrJs.Int, arrJs.Int, arrJs.Int, arrJs.Int);
            wal4Mat mats = wal4Mat.N(arrJs.Byte, arrJs.Byte, arrJs.Byte, arrJs.Byte);
            int type = arrJs.Int;
            
            AddWallsRect(zn, bd, mats, (cel1l.Type)type, seriType);
        }
    }

    void loadWallLines(zone_ zn, JSONObject jsWallsLine, SeriType seriType)
    {
        for (int i = 0; i < jsWallsLine.Count; ++i)
        {
            jsArr arrJs = new jsArr(jsWallsLine[i]);
            i4 bd = new i4(arrJs.Int, arrJs.Int, arrJs.Int, arrJs.Int);
            wal4Mat mats = wal4Mat.N(arrJs.Byte, arrJs.Byte, arrJs.Byte, arrJs.Byte);
            
            AddWallsLine(zn, bd, mats, (cel1l.Type)arrJs.Int, seriType);
        }
    }

    void loadWalls(zone_ zn, JSONObject jsWalls, SeriType seriType)
    {
        for (int i = 0; i < jsWalls.Count; ++i)
        {
            jsArr arJs = new jsArr(jsWalls[i]);
            AddWallsSingle(zn, arJs.Int, arJs.Int,
                wal4Mat.N(arJs.Byte, arJs.Byte, arJs.Byte, arJs.Byte), (cel1l.Type)arJs.Int, seriType);
        }
    }

    void loadWallsDel(zone_ zn, JSONObject jsWalls, SeriType seriType)
    {
        for (int i = 0; i < jsWalls.Count; ++i)
        {
            jsArr arJs = new jsArr(jsWalls[i]);
            AddWallsDel(zn, arJs.Int, arJs.Int, arJs.Int, arJs.Int, seriType);
        }
    }


    void loadZprps(zone_ zn, JSONObject jsZprps, SeriType seriType)
    {
        for (int i = 0; i < jsZprps.Count; ++i)
        {
            jsArr arJs = new jsArr(jsZprps[i]);
            AddZprop(zn, arJs.Int, arJs.Int, arJs.Int, arJs.Int,
                (byte)arJs.Int, (cel1l.Type)arJs.Int, new f4(arJs.F, arJs.F, arJs.F, arJs.F), seriType);
        }
    }

    void loadNpcs(zone_ zn, JSONObject jsNpcs, SeriType seriType)
    {
        for (int i = 0; i < jsNpcs.Count; ++i)
        {
            jsArr arJs = new jsArr(jsNpcs[i]);
            AddNpc(zn,
                new Vector3(arJs.F, arJs.F, arJs.F),
                new Vector3(arJs.F, arJs.F, arJs.F),
                (model.eType)arJs.Long, arJs.Byte, (model.Equip)arJs.Long,
                arJs.Byte, arJs.Byte, arJs.Byte, arJs.Byte, arJs.Byte,
                (unit.SubType)arJs.Long, seriType);
        }
    }

    void loadZbgs(zone_ zn, JSONObject jsZBgs, SeriType seriType)
    {
        for (int i = 0; i < jsZBgs.Count; ++i)
        {
            jsArr arJs = new jsArr(jsZBgs[i]);
            AddZbg(zn, (zbgs.Type)arJs.Int,
                new Vector3(arJs.F, arJs.F, arJs.F),
                new f4(arJs.F, arJs.F, arJs.F, arJs.F), seriType);
        }
    }

    void loadRprs(zone_ zn, JSONObject jsObjs, SeriType seriType)
    {
        for (int i = 0; i < jsObjs.Count; ++i)
        {
            jsArr arJs = new jsArr(jsObjs[i]);

            js.Inst.AddRpr(zn, arJs.Int, arJs.Int,
                (byte)arJs.Int, (cel1l.Type)arJs.Int,
                new f4(arJs.F, arJs.F, arJs.F, arJs.F), seriType);
        }
    }
}
