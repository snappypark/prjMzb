using System.Collections.Generic;
using UnityEngine;
using UnityEngineExJSON;

public partial class js /*.parse.write*/
{
    public string CreateJSON()
    {
        JSONObject jsRoot = new JSONObject(); // for return jsRoot.Print()

        JSONObject jsMap = NewJSONObj.With(jsK.Map, jsRoot, JSONObject.Type.OBJECT);

        //info
        JSONObject jsInfo = NewJSONObj.With(jsK.Info, jsMap, JSONObject.Type.OBJECT);
        jsInfo.AddField("mode", (int)info.mode);
        jsInfo.AddField("solotype", (int)info.soloType);
        jsInfo.AddField("dur-sec", (int)info.remainSec);
        
        // zones
        JSONObject jsZones = NewJSONObj.With(jsK.Zones, jsMap, JSONObject.Type.ARRAY);
        for (byte i = 0; i < zones.Count; ++i)
        {
            JSONObject jsZn = NewJSONObj.With(jsZones, JSONObject.Type.ARRAY);

            zone_ zn = zones[i];
            jsZn.Add(zn.bd.X0);
            jsZn.Add(zn.bd.Z0);
            jsZn.Add(zn.bd.X1);
            jsZn.Add(zn.bd.Z1);
            jsZn.Add(zn.bd.Y);
            jsZn.Add((int)zn.eSight);
            makeJSON_ZnOption(ref zn.options, jsZn.AddObj(JSONObject.Type.ARRAY));
            makeJSON_TileRs(ref zn.tileRs, jsZn.AddObj(JSONObject.Type.ARRAY));
            makeJSON_Tiles(ref zn.tile1s, jsZn.AddObj(JSONObject.Type.ARRAY));
            makeJSON_Tiles(ref zn.tile5s, jsZn.AddObj(JSONObject.Type.ARRAY));
            makeJSON_Tiles(ref zn.tileTs, jsZn.AddObj(JSONObject.Type.ARRAY));
            makeJS_wallsMaze(ref zn.wallsMazes, jsZn.AddObj(JSONObject.Type.ARRAY));
            makeJS_wallsRect(ref zn.wallsRects, jsZn.AddObj(JSONObject.Type.ARRAY));
            makeJS_wallsLines(ref zn.wallsLines, jsZn.AddObj(JSONObject.Type.ARRAY));
            makeJS_walls(ref zn.walls, jsZn.AddObj(JSONObject.Type.ARRAY));
            
            makeJS_wallsDel(ref zn.wallsDel, jsZn.AddObj(JSONObject.Type.ARRAY));

            makeJS_zprps(ref zn.prps, jsZn.AddObj(JSONObject.Type.ARRAY));
            makeJS_npcs(ref zn.npcs, jsZn.AddObj(JSONObject.Type.ARRAY));
            makeJS_zbgs(ref zn.zbgs, jsZn.AddObj(JSONObject.Type.ARRAY));

            makeJS_rprs(ref zn.rprs, jsZn.AddObj(JSONObject.Type.ARRAY));
        }

        JSONObject jsSpawns = NewJSONObj.With(jsK.Spawns, jsMap, JSONObject.Type.ARRAY);
        for (byte i = 0; i < spawns.Count; ++i)
        {
            JSONObject jsSpawn = NewJSONObj.With(jsSpawns, JSONObject.Type.ARRAY);

            spawn_ spawn = spawns[i];
            jsSpawn.Add(spawn.ally);
            jsSpawn.Add(spawn.pos.x); jsSpawn.Add(spawn.pos.y); jsSpawn.Add(spawn.pos.z);
            jsSpawn.Add(spawn.dir.x); jsSpawn.Add(spawn.dir.y); jsSpawn.Add(spawn.dir.z);
            jsSpawn.Add(spawn.numAmmoPistol);
            jsSpawn.Add(spawn.numAmmoRifle);
            jsSpawn.Add(spawn.numBomb);
        }
        string result = jsRoot.Print();
        //Debug.Log(jsRoot.Print());
        jsRoot.Clear();
        return result;
    }

    void makeJSON_ZnOption(ref List<zone.Option> objs, JSONObject jsObjs)
    {
        for (int i = 0; i < objs.Count; ++i)
        {
            JSONObject jsObj = NewJSONObj.With(jsObjs, JSONObject.Type.ARRAY);
            jsObj.Add((int)objs[i]);
        }
    }


    void makeJSON_TileRs(ref List<tileR_> tiles, JSONObject jsTiles)
    {
        for (int i = 0; i < tiles.Count; ++i)
        {
            JSONObject jsTile = NewJSONObj.With(jsTiles, JSONObject.Type.ARRAY);
            tileR_ tile = tiles[i];
            jsTile.Add(tile.opts.F1);
            jsTile.Add(tile.opts.F2);
            jsTile.Add(tile.opts.F3);
            jsTile.Add(tile.opts.F4);
            jsTile.Add((int)tile.matIdx);
        }
    }

    void makeJSON_Tiles(ref List<tile_> tiles, JSONObject jsTiles)
    {
        for (int i = 0; i < tiles.Count; ++i)
        {
            JSONObject jsTile = NewJSONObj.With(jsTiles, JSONObject.Type.ARRAY);
            tile_ tile = tiles[i];
            jsTile.Add(tile.bd.xMin);
            jsTile.Add(tile.bd.zMin);
            jsTile.Add(tile.bd.xMax);
            jsTile.Add(tile.bd.zMax);
            jsTile.Add((int)tile.matIdx);
        }
    }
    
    void makeJS_wallsMaze(ref List<wallsMaze_> wallsMazes, JSONObject jsMazes)
    {
        for (int i = 0; i < wallsMazes.Count; ++i)
        {
            JSONObject jsMaze = NewJSONObj.With(jsMazes, JSONObject.Type.ARRAY);
            wallsMaze_ maze = wallsMazes[i];
            jsMaze.Add(maze.bd.X0); jsMaze.Add(maze.bd.Z0);
            jsMaze.Add(maze.bd.X1); jsMaze.Add(maze.bd.Z1);
            jsMaze.Add(maze.szRC.v1);
            jsMaze.Add(maze.szRC.v2);
            jsMaze.Add(maze.numRC.v1);
            jsMaze.Add(maze.numRC.v2);
            jsMaze.Add((int)maze.matMini);
            jsMaze.Add((int)maze.matTop);
            jsMaze.Add((int)maze.matSideUp);
            jsMaze.Add((int)maze.matSideDown);
            jsMaze.Add((int)maze.type);
            jsMaze.Add(maze.rand0);
            jsMaze.Add(maze.option0);
        }
    }

    void makeJS_wallsRect(ref List<wallsRect_> wallsRects, JSONObject jsRects)
    {
        for (int i = 0; i < wallsRects.Count; ++i)
        {
            JSONObject jsRect = NewJSONObj.With(jsRects, JSONObject.Type.ARRAY);
            wallsRect_ rec = wallsRects[i];
            jsRect.Add(rec.bd.X0); jsRect.Add(rec.bd.Z0);
            jsRect.Add(rec.bd.X1); jsRect.Add(rec.bd.Z1);
            jsRect.Add((int)rec.matMini);
            jsRect.Add((int)rec.matTop);
            jsRect.Add((int)rec.matSideUp);
            jsRect.Add((int)rec.matSideDown);
            jsRect.Add((int)rec.type);
        }
    }

    void makeJS_wallsLines(ref List<wallsRect_> wallsLines, JSONObject jsRects)
    {
        for (int i = 0; i < wallsLines.Count; ++i)
        {
            JSONObject jsRect = NewJSONObj.With(jsRects, JSONObject.Type.ARRAY);
            wallsRect_ rec = wallsLines[i];
            jsRect.Add(rec.bd.X0); jsRect.Add(rec.bd.Z0);
            jsRect.Add(rec.bd.X1); jsRect.Add(rec.bd.Z1);
            jsRect.Add((int)rec.matMini);
            jsRect.Add((int)rec.matTop);
            jsRect.Add((int)rec.matSideUp);
            jsRect.Add((int)rec.matSideDown);
            jsRect.Add((int)rec.type);
        }
    }
    
    void makeJS_walls(ref List<wallSingle_> walls_, JSONObject jsRects)
    {
        for (int i = 0; i < walls_.Count; ++i)
        {
            JSONObject jsRect = NewJSONObj.With(jsRects, JSONObject.Type.ARRAY);
            wallSingle_ wal = walls_[i];
            jsRect.Add(wal.x);
            jsRect.Add(wal.z);
            jsRect.Add((int)wal.matMini);
            jsRect.Add((int)wal.matTop);
            jsRect.Add((int)wal.matSideUp);
            jsRect.Add((int)wal.matSideDown);
            jsRect.Add((int)wal.cType);
        }
    }

    void makeJS_wallsDel(ref List<wall_Del_> walls_, JSONObject jsRects)
    {
        for (int i = 0; i < walls_.Count; ++i)
        {
            JSONObject jsRect = NewJSONObj.With(jsRects, JSONObject.Type.ARRAY);
            wall_Del_ wal = walls_[i];
            jsRect.Add(wal.bd.X0);
            jsRect.Add(wal.bd.Z0);
            jsRect.Add(wal.bd.X1);
            jsRect.Add(wal.bd.Z1);
        }
    }

    void makeJS_zprps(ref List<zprp_> zprps_, JSONObject jsZprps)
    {
        for (int i = 0; i < zprps_.Count; ++i)
        {
            JSONObject jszprp = NewJSONObj.With(jsZprps, JSONObject.Type.ARRAY);
            zprp_ prp = zprps_[i];
            jszprp.Add(prp.bd.X0);
            jszprp.Add(prp.bd.Z0);
            jszprp.Add(prp.bd.X1);
            jszprp.Add(prp.bd.Z1);
            jszprp.Add((int)prp.oType);
            jszprp.Add((int)prp.cellType);
            jszprp.Add(prp.opts.F1);
            jszprp.Add(prp.opts.F2);
            jszprp.Add(prp.opts.F3);
            jszprp.Add(prp.opts.F4);
        }
    }

    void makeJS_npcs(ref List<npc_> zprps_, JSONObject jsZprps)
    {
        for (int i = 0; i < zprps_.Count; ++i)
        {
            JSONObject jsNpc = NewJSONObj.With(jsZprps, JSONObject.Type.ARRAY);
            npc_ npc = zprps_[i];

            jsNpc.Add(npc.pos.x);
            jsNpc.Add(npc.pos.y);
            jsNpc.Add(npc.pos.z);

            jsNpc.Add(npc.dir.x);
            jsNpc.Add(npc.dir.y);
            jsNpc.Add(npc.dir.z);

            jsNpc.Add((int)npc.modelType);
            jsNpc.Add((int)npc.matType);
            jsNpc.Add((int)npc.equipType);
            jsNpc.Add((int)npc.ally);
            jsNpc.Add((int)npc.headIdx);
            jsNpc.Add((int)npc.bodyIdx);
            jsNpc.Add((int)npc.meleeIdx);
            jsNpc.Add((int)npc.aniCtrlIdx);
            jsNpc.Add((int)npc.subType);
        }
    }

    void makeJS_zbgs(ref List<zbg_> objs_, JSONObject jsObjs)
    {
        for (int i = 0; i < objs_.Count; ++i)
        {
            JSONObject jsNpc = NewJSONObj.With(jsObjs, JSONObject.Type.ARRAY);

            zbg_ obj = objs_[i];
            jsNpc.Add((int)obj.oType);
            jsNpc.Add(obj.ps.x);
            jsNpc.Add(obj.ps.y);
            jsNpc.Add(obj.ps.z);
            jsNpc.Add(obj.opts.F1);
            jsNpc.Add(obj.opts.F2);
            jsNpc.Add(obj.opts.F3);
            jsNpc.Add(obj.opts.F4);
        }
    }


    void makeJS_rprs(ref List<rpr_> objs_, JSONObject jsObjs)
    {
        for (int i = 0; i < objs_.Count; ++i)
        {
            JSONObject jsNpc = NewJSONObj.With(jsObjs, JSONObject.Type.ARRAY);

            rpr_ obj = objs_[i];
            jsNpc.Add(obj.x);
            jsNpc.Add(obj.z);
            jsNpc.Add((int)obj.type);
            jsNpc.Add((int)obj.cellType);
            jsNpc.Add(obj.opt.F1);
            jsNpc.Add(obj.opt.F2);
            jsNpc.Add(obj.opt.F3);
            jsNpc.Add(obj.opt.F4);
        }
        /*
        JSONObject jsPrps = NewJSONObj.With(jsK.Prps, jsMap, JSONObject.Type.ARRAY);
        for (byte i = 0; i < props.Count; ++i)
        {
            JSONObject jsPrp = NewJSONObj.With(jsPrps, JSONObject.Type.ARRAY);

            rpr_ prp = props[i];
            jsPrp.Add(prp.x);
            jsPrp.Add(prp.y);
            jsPrp.Add(prp.z);
            jsPrp.Add((int)prp.type);
            jsPrp.Add((int)prp.cellType);
            jsPrp.Add(prp.opt.F1);
            jsPrp.Add(prp.opt.F2);
            jsPrp.Add(prp.opt.F3);
            jsPrp.Add(prp.opt.F4);
        }
        */
    }

}
