using UnityEngine;

public class zSpin : zprp
{
    [SerializeField] Renderer _render;
    int rotateY = 0;

    // options: speed (pos cw, neg ccw), 
    public override void Assign(zone.prp_ info)
    {
        rotateY = 20;
        transform.localEulerAngles = new Vector3(0, rotateY, 0);
        transform.localScale = info.Sz;
        _render.material = zjs.tiles.mats[matTiles.Category.One, matTiles.Gray];
        _render.material.SetTextureScale("_MainTex", new Vector4(info.Sz.x*0.5f, info.Sz.z * 0.5f));
    }

    void Update()
    {
        rotateY = (rotateY+1) % 360;
        transform.localEulerAngles = new Vector3(0, rotateY, 0);
    }
}
