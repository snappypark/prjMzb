using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mosframe;

public class scitem_region : UIBehaviour, IDynamicScrollViewItem
{
    [SerializeField] Text _lb;
    [SerializeField] Image _img;

    int _idx = -1;

    public void onUpdateItem(int idx)
    {
        _lb.text = synRegion.names[idx];
        _img.color = synRegion.CurIdx == idx ?
            new Color(0.8570113f, 0.965283f, 0.8125507f, 0.9372549f) :
            new Color(0.7370113f, 0.745283f, 0.6925507f, 0.9372549f);
        _idx = idx;
    }

    #region UI Action
    public void OnBtn_Select()
    {
        synRegion.CurIdx = _idx;
        uis.outgam.multiEntry.Refresh();
    }
    #endregion
}
