using System.Collections;
using UnityEngine;

public partial class stages
{
    public IEnumerator StartMenu_()
    {
        ads.Inst.StartLoad();
        cams.Inst.SetClearFlag(CameraClearFlags.Color);
        zone z0 = core.zones.AddZone(110, 115, 144, 139, 0, sights.eState.ByCtrlUnit);

        z0.AddBgs(zbgs.eCloud, new Vector3(71, -12.2f, 192.0f), new f4(7, 40, 0));
        z0.AddBgs(zbgs.eCloud, new Vector3(131.7f, -6.5f, 211.7f), new f4(5, 80, 0));

        #region tile wall
        z0.AddTile_lTl(110, 115, 144, 139, matTiles.Green); //GrayLight SmarDark
        
        wal4Mat wMat = wal4Mat.N(matWalls.GreenLight);
        z0.AddRectWalls(new i4(110, 115, 144, 139), wMat, cel1l.Type.Wall);
        
        z0.AddWall(117, 138, wMat, cel1l.Type.Wall);
        z0.AddWall(117, 137, wMat, cel1l.Type.Wall);
        z0.AddWall(121, 136, wMat, cel1l.Type.Wall);
        z0.AddWall(122, 136, wMat, cel1l.Type.Wall);
        
        z0.AddWall(111, 132, wMat, cel1l.Type.Wall);
        z0.AddWall(111, 131, wMat, cel1l.Type.Wall);
        z0.AddWall(111, 130, wMat, cel1l.Type.Wall);

        z0.AddWallLines(new i4(123, 132, 140, 132), wMat, cel1l.Type.Wall);

        z0.AddWallLines(new i4(112, 126, 116, 126), wMat, cel1l.Type.Wall);
        z0.AddWallLines(new i4(119, 125, 122, 125), wMat, cel1l.Type.Wall);
        z0.AddWallLines(new i4(120, 120, 125, 120), wMat, cel1l.Type.Wall);

        z0.AddWallLines(new i4(115,116, 115, 125), wMat, cel1l.Type.Wall);
        z0.AddWallLines(new i4(118,113, 118, 120), wMat, cel1l.Type.Wall);
        z0.AddWallLines(new i4(120, 126, 120, 128), wMat, cel1l.Type.Wall);
        z0.AddWallLines(new i4(125, 123, 125, 131), wMat, cel1l.Type.Wall);
        #endregion

        zjs.rprs.Add(119, 137, rprs.eLamb, cel1l.Type.LambOff, f4.O);

        zjs.rprs.Add(117, 136, rprs.eBox, cel1l.Type.Box, f4.O);
        zjs.rprs.Add(118, 133, rprs.eBox, cel1l.Type.Box, f4.O);
        zjs.rprs.Add(113, 131, rprs.eBox, cel1l.Type.Box, f4.O);

        z0.AddPrp(115, 138, zprps.eArea, cel1l.Type.AreaUpLadder, f4.O);

        z0.AddPrp(116, 136, zprps.eBomb, cel1l.Type.Bomb, new f4(-1.1f, 0.1f, 0.47f, 0.2f));
        z0.AddPrp(112, 134, zprps.eBomb, cel1l.Type.Bomb, new f4(-1.1f, 0.1f, 0.47f, -0.1f));
        z0.AddPrp(111, 134, zprps.eBomb, cel1l.Type.Bomb, new f4(-1.1f, 0.6f, 0.47f, -0.1f));
        z0.AddPrp(112, 133, zprps.eBomb, cel1l.Type.Bomb, new f4(-1.1f, -0.1f, 0.47f, 0.4f));

        z0.AddPrp(117, 134, zprps.eAmmo, cel1l.Type.Ammo, new f4(-1.1f, 0.1f, 0.18f, 0.1f));
        #region tree bush grass
        zjs.rprs.Add(111, 137, rprs.eTree, cel1l.Type.Tree3B_Green, new f4(Random.Range(0, 360), 0, 1, 0.85f));
        zjs.rprs.Add(111, 135, rprs.eTree, cel1l.Type.Tree3B_Green, new f4(Random.Range(0, 360), 0, 1, 0.85f));
        zjs.rprs.Add(112, 136, rprs.eTree, cel1l.Type.Tree4B_Green, new f4(Random.Range(0, 360), 0, 1, 0.85f));
        zjs.rprs.Add(120, 135, rprs.eTree, cel1l.Type.Tree3B_Green, new f4(Random.Range(0, 360), 0, 1, 0.85f));
        zjs.rprs.Add(121, 138, rprs.eTree, cel1l.Type.Tree4B_Green, new f4(Random.Range(0, 360), 0, 1, 0.85f));
        zjs.rprs.Add(118, 129, rprs.eTree, cel1l.Type.Tree3B_Green, new f4(Random.Range(0, 360), 0, 1, 0.85f));
        zjs.rprs.Add(116, 126, rprs.eTree, cel1l.Type.Tree3B_Green, new f4(Random.Range(0, 360), 0, 1, 0.85f));
        zjs.rprs.Add(117, 123, rprs.eTree, cel1l.Type.Tree3B_Green, new f4(Random.Range(0, 360), 0, 1, 0.85f));

        zjs.rprs.Add(113, 127, rprs.eTree, cel1l.Type.Tree3B_Green, new f4(Random.Range(0, 360), 0, 1, 0.85f));

        zjs.rprs.Add(119, 136, rprs.eBush, cel1l.Type.Bush, f4.O);
        zjs.rprs.Add(112, 138, rprs.eBush, cel1l.Type.Bush, f4.O);
        zjs.rprs.Add(113, 138, rprs.eBush, cel1l.Type.Bush, f4.O);

        z0.AddBgs(zbgs.eGrass, new Vector3(114, -0.01f, 138), new f4(0, 1.0f, 0.6f));
        z0.AddBgs(zbgs.eGrass, new Vector3(113, -0.01f, 137), new f4(70, 0.8f, 0.7f));
        z0.AddBgs(zbgs.eGrass, new Vector3(118, -0.01f, 135), new f4(160, 0.7f, 0.7f));
        z0.AddBgs(zbgs.eGrass, new Vector3(117, -0.01f, 133), new f4(290, 0.6f, 0.6f));
        #endregion
        
        //    gbjs.props.Add(115, 0, 132, props.ePlat, cel1l.Type.PlatOil, new f4(0,
        //    Random.Range(0.27f, 0.31f), Random.Range(0.0f, 6.3f)));

        //for (int i = 0; i < core.zones.Num; ++i)
        //    yield return core.zones[i].SetWallsRandomGap_();


        yield return null; 
        core.zones.OnUpdate_NextZone(z0, false);
        yield return null;

        _dt01 = 1;
        ctrls.Inst.SpawnOnMenu(_firstCamPos, _gapCamTarget);
        durationTime = -1;
        core.sights.SetAlpha(0.35f);

        uis.outgam.menu.ActiveAdBtn(false);
        _checkAdBtn = true;
    }

    bool _checkAdBtn = true;
    float _dt01;
    Vector3 _firstCamPos = new Vector3(3, 7.5f, -23);
    Vector3 _secondCamPos = new Vector3(4.7f, 5.4f, -9.5f); // new Vector3(-0.2f, 2.9f, -5.1f);
    Vector3 _gapCamTarget = new Vector3(2.1f,-0.5f, 0.57f);
    public void Update_OnMenu(unit o)
    {
        _dt01 *= 0.97f;
        Vector3 dt10 = Vector3.Lerp(_firstCamPos, _secondCamPos, 1 - _dt01);
        cams.Inst.mainTran.SetTarget(o.tran, dt10, _gapCamTarget);

        if(_dt01 < 0.1f && _checkAdBtn )
        {
            if(ads.Inst.IsLoadRewardX10())
            {
                uis.outgam.menu.ActiveAdBtn(true);
                _checkAdBtn = false;
            }
        }
    }
}
