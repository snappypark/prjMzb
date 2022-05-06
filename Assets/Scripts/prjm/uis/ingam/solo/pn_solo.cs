using UnityEngine;

public class pn_solo : MonoBehaviour
{
    public void OnActive(ui_ingam.State state_)
    {
        gameObject.SetActive(state_ == ui_ingam.State.solo);
    }

    #region UI Action
    public void OnBtn_Option()
    {
        if (uis.IsEnableBtnTime(1.7f))
            uis.pops.result.option.Active(popOption.Type.InStage);
    }
    #endregion
}
