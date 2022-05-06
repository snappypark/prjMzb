using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zWall : zprp
{
    public enum Type
    {
        None,
        CW_Loop,
        CCW_Loop,
        CW_PING,
        CCW_PING,
    }
    [SerializeField] MeshRenderer _renderMini;
    [SerializeField] Transform _tranMini;

    [SerializeField] MeshRenderer _renderUpper;
    [SerializeField] Transform _tranUpper;

    [SerializeField] MeshRenderer _renderLower;
    [SerializeField] Transform _tranLower;


    [SerializeField] LineRenderer _line;

    // opt: mat, type, 
    public override void Assign(zone.prp_ info)
    {
    }

    public void AssignWall(zone.wall_ info, Material mini, Material top, Material sideUp, Material sideDown)
    {
        _renderMini.sharedMaterial = mini; _tranMini.localScale = Vector3.one;
        _renderUpper.sharedMaterial = sideUp;
        _renderLower.sharedMaterial = sideDown;

        _tranUpper.localScale = info.szUp;
        _tranLower.localScale = info.szDown;
    }

}
