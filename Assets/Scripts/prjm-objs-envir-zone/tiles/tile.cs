using UnityEngine;

public class tile : nj.qObj
{
    [SerializeField] MeshRenderer _render;

    public void Assign(ref zone.tile_ info)
    {
        transform.localEulerAngles = new Vector3(90, info.angle, 0);
        transform.localScale = info.Sz;
        switch (info.matCategory)
        {
            case matTiles.Category.NxN:
                _render.material = zjs.tiles.mats[info.matCategory, info.matType];
                //_render.material.SetTextureScale("_MainTex", info.Tiling*0.25f);
      //          _render.material.SetTextureScale("_MainTex", info.Tiling*0.3333333f);
                _render.material.SetTextureScale("_MainTex", info.Tiling);
                break;
            default:
                _render.sharedMaterial = zjs.tiles.mats[info.matCategory, info.matType];
                _render.sharedMaterial.SetTextureScale("_MainTex", info.Tiling*3);
                //_render.sharedMaterial.SetTextureScale("_MainTex", info.Tiling);
                break;
        }

        info.cdx = cdx;
    }
}
