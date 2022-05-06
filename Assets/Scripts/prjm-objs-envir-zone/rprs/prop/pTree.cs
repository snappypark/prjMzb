using UnityEngine;

public class pTree : rpr
{
    [SerializeField] Renderer _tr1;
    [SerializeField] Renderer _tr2;
    [SerializeField] Renderer _tr3;
    [SerializeField] Renderer _tr4;

    //options: rotation, height, scale_radius, scale_height, 
    public override void Assign(cel1l cell)
    {
        cell.obj.transform.localEulerAngles = new Vector3(0, cell.opts.F1, 0);
        cell.obj.transform.localPosition += new Vector3(0, cell.opts.F2, 0);
        cell.obj.transform.localScale = new Vector3(cell.opts.F3, cell.opts.F4, cell.opts.F3);

        switch (cell.type)
        {
            case cel1l.Type.Tree1A_Blue:
                _tr1.gameObject.SetActive(true);
                _tr2.gameObject.SetActive(false);
                _tr3.gameObject.SetActive(false);
                _tr4.gameObject.SetActive(false);
                _tr1.sharedMaterial = zjs.rprs.mats[matProps.Tree1A];
                break;
            case cel1l.Type.Tree1B_Pink:
                _tr1.gameObject.SetActive(true);
                _tr2.gameObject.SetActive(false);
                _tr3.gameObject.SetActive(false);
                _tr4.gameObject.SetActive(false);
                _tr1.sharedMaterial = zjs.rprs.mats[matProps.Tree1B];
                break;
            case cel1l.Type.Tree2A_Blue:
                _tr1.gameObject.SetActive(false);
                _tr2.gameObject.SetActive(true);
                _tr3.gameObject.SetActive(false);
                _tr4.gameObject.SetActive(false);
                _tr2.sharedMaterial = zjs.rprs.mats[matProps.Tree1A];
                break;
            case cel1l.Type.Tree2B_Pink:
                _tr1.gameObject.SetActive(false);
                _tr2.gameObject.SetActive(true);
                _tr3.gameObject.SetActive(false);
                _tr4.gameObject.SetActive(false);
                _tr2.sharedMaterial = zjs.rprs.mats[matProps.Tree1B];
                break;
            case cel1l.Type.Tree3A_Red:
                _tr1.gameObject.SetActive(false);
                _tr2.gameObject.SetActive(false);
                _tr3.gameObject.SetActive(true);
                _tr4.gameObject.SetActive(false);
                _tr3.sharedMaterial = zjs.rprs.mats[matProps.Tree2A];
                break;
            case cel1l.Type.Tree3B_Green:
                _tr1.gameObject.SetActive(false);
                _tr2.gameObject.SetActive(false);
                _tr3.gameObject.SetActive(true);
                _tr4.gameObject.SetActive(false);
                _tr3.sharedMaterial = zjs.rprs.mats[matProps.Tree2B];
                break;
            case cel1l.Type.Tree4A_Red:
                _tr1.gameObject.SetActive(false);
                _tr2.gameObject.SetActive(false);
                _tr3.gameObject.SetActive(false);
                _tr4.gameObject.SetActive(true);
                _tr4.sharedMaterial = zjs.rprs.mats[matProps.Tree2A];
                break;
            case cel1l.Type.Tree4B_Green:
                _tr1.gameObject.SetActive(false);
                _tr2.gameObject.SetActive(false);
                _tr3.gameObject.SetActive(false);
                _tr4.gameObject.SetActive(true);
                _tr4.sharedMaterial = zjs.rprs.mats[matProps.Tree2B];
                break;
        }
    }


}
