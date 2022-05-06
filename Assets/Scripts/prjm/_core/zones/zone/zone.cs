using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine;

public partial class zone
{
    public static zone Empty = new zone();

    public zone this[int idx] { get { return core.zones[ adjx[idx] ]; } }
    
    [HideInInspector] public i5 bd;
    [HideInInspector] public float PosY;
    [HideInInspector] public int idx = -1;
    [HideInInspector] public List<int> adjx = new List<int>();

    Vector3 _center;
    float _szX, _szZ, _szL;

    public wal4Mat mats = new wal4Mat(matWalls.SkyLight, matWalls.SkyLight, matWalls.SkyLight, matWalls.SkyLight);

    public float durationOfJsp;
    public bool changed = false;

    public zone() { }
    public zone(int idx_, i5 bd_, sights.eState eSight_, bool firstEnter_ = false)
    {
        PosY = 0;
        idx = idx_;
        sight = eSight_;
        bd = bd_;
        _szX = (bd.GapX+1) * 0.5f;
        _szZ = (bd.GapZ+1) * 0.5f;
        _szL = _szX > _szZ ? _szX : _szZ;
        _center = new Vector3(bd.X0 + _szX, PosY, bd.Z0 + _szZ);
        _firstEnter = firstEnter_;
        durationOfJsp = getDurationOfJspCalculation();
    }

    bool _firstEnter = false;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnEnter(bool camMoveSmooth = false)
    {
        core.sights.SetState(sight);
        cams.Inst.MiniMap.SetPosAndOrth(_center, _szL, camMoveSmooth);
        if (_firstEnter)
        {
            if (_hasMeteor)
            {
                sMeteor.Count = 0;
                core.unitClones.EnqMsg(msgType.Meteor, -1, (short)idx);
            }
            core.unitClones.EnqMsg(msgType.SpawnNpc, -1, (short)idx);
            _firstEnter = false;
        }
    }
    
    public void Clear()
    {
        clear_Walls();
        clear_Tiles();
        clear_Bgs();
        clear_Prps();

        waitNpcs.Clear();
        Mobs.Clear();

        adjx.Clear();
        mzs.Clear();
        waycells.Clear();
        ways.Clear();
    }


    float getDurationOfJspCalculation()
    {
        int gapBy5 = (int)((bd.GapX + 1) * 0.2f);
        // 5 -> 2:05, 6 -> 2:30, 7 -> 2:55, 8 -> 3:20
        switch (gapBy5)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                return 1.8f;
            case 5:
                return 2.08f;//0.0714285f
            case 6:
                return 2.5f;//0.0714285f
            case 7:
                return 2.93f;//0.0714285f
            case 8:
                return 3.333f;
            default:
                return 3.333f;
        }
    }
}
