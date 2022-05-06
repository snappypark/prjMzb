using UnityEngine;

public class wall : nj.qObj
{
    [SerializeField] MeshRenderer _renderMini;
    [SerializeField] Transform _tranMini;

    [SerializeField] MeshRenderer _renderUpper;
    [SerializeField] Transform _tranUpper;

    [SerializeField] MeshRenderer _renderLower;
    [SerializeField] Transform _tranLower;

    public override void AssignWall(zone.wall_ info, Material mini, Material top, Material sideUp, Material sideDown)
    {
        //_renderMini.sharedMaterial = mini;
        _tranMini.localScale = Vector3.one;
        _renderUpper.sharedMaterial = sideUp;
        _renderLower.sharedMaterial = sideDown;
        
        _tranUpper.localScale = info.szUp;
        _tranLower.localScale = info.szDown;
    }
}
