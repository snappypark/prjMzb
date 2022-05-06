using UnityEngine;

public class uj_AutoAllChange : uj_wallsAbs
{
    public override uj_editmap.eState State { get { return uj_editmap.eState._AutoAllChange; } }

    [SerializeField] js.wallSingle_ _jsWall;
    [SerializeField] js.tile_ _jsTile;

    public void ChangeAllWallMats()
    {

        for (int i = 0; i < js.Inst.zones.Count; ++i)
        {
            js.zone_ jsZn = js.Inst.zones[i];

            for (int j = 0; j < jsZn.walls.Count; ++j)
            {
                if (jsZn.walls[j].matTop == matWalls.Type.Metal)
                    jsZn.walls[j].SetMat(_jsWall.matMini, matWalls.Type.Metal, matWalls.Type.Metal, matWalls.Type.Metal);
                else
                    jsZn.walls[j].SetMat(_jsWall.matMini, _jsWall.matMini, _jsWall.matMini, _jsWall.matMini);
            }

            for (int j = 0; j < jsZn.wallsLines.Count; ++j)
            {
                if (jsZn.wallsLines[j].matTop == matWalls.Type.Metal)
                    jsZn.wallsLines[j].SetMat(_jsWall.matMini, matWalls.Type.Metal, matWalls.Type.Metal, matWalls.Type.Metal);
                else
                    jsZn.wallsLines[j].SetMat(_jsWall.matMini, _jsWall.matMini, _jsWall.matMini, _jsWall.matMini);
            }

            for (int j = 0; j < jsZn.wallsRects.Count; ++j)
            {
                if (jsZn.wallsRects[j].matTop == matWalls.Type.Metal)
                    jsZn.wallsRects[j].SetMat(_jsWall.matMini, matWalls.Type.Metal, matWalls.Type.Metal, matWalls.Type.Metal);
                else
                    jsZn.wallsRects[j].SetMat(_jsWall.matMini, _jsWall.matMini, _jsWall.matMini, _jsWall.matMini);
            }

            for (int j = 0; j < jsZn.wallsMazes.Count; ++j)
            {
                if (jsZn.wallsMazes[j].matTop == matWalls.Type.Metal)
                    jsZn.wallsMazes[j].SetMat(_jsWall.matMini, matWalls.Type.Metal, matWalls.Type.Metal, matWalls.Type.Metal);
                else
                    jsZn.wallsMazes[j].SetMat(_jsWall.matMini, _jsWall.matMini, _jsWall.matMini, _jsWall.matMini);
            }
            //
            for (int j = 0; j < jsZn.rprs.Count; ++j)
            {
                if (jsZn.rprs[j].type == rprs.Type.Push)
                {
                    f4 opts = jsZn.rprs[j].opt;
                    jsZn.rprs[j].opt = new f4((float)_jsWall.matMini + 0.01f, opts.F2, opts.F3, opts.F4);
                }
            }

        }

        ctrls.Unit.cell.zn.Refresh_WithRelatedZn_ByJs();
        zjs.rprs.roller.OnUpdate(new Pt(0, 2, 0));
        core.collis.Clear();
    }


    public void ChangeAllTileMats()
    {
        for (int i = 0; i < js.Inst.zones.Count; ++i)
        {
            js.zone_ jsZn = js.Inst.zones[i];

            for (int j = 0; j < jsZn.tile1s.Count; ++j)
            {
                jsZn.tile1s[j].matIdx = _jsTile.matIdx;
            }

            for (int j = 0; j < jsZn.tile5s.Count; ++j)
            {
                jsZn.tile5s[j].matIdx = _jsTile.matIdx;
            }

            for (int j = 0; j < jsZn.tileRs.Count; ++j)
            {
                jsZn.tileRs[j].matIdx = _jsTile.matIdx;
            }

            for (int j = 0; j < jsZn.tileTs.Count; ++j)
            {
                jsZn.tileTs[j].matIdx = _jsTile.matIdx;
            }
        }
        
        ctrls.Unit.cell.zn.Refresh_WithRelatedZn_ByJs();
        zjs.rprs.roller.OnUpdate(new Pt(0, 2, 0));
        core.collis.Clear();
    }

    
}
