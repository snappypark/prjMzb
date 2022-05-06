using UnityEngine;
using UnityEngine.UI;

public class popPreStage : MonoBehaviour
{
    [SerializeField] GameObject _root;

    [SerializeField] Text _stage;
    [SerializeField] InputField _input;

    [SerializeField] Text _lbOk;
    [SerializeField] Text _lbCancel;

    public void Active()
    {
        _stage.text = string.Format("{0}:", langs.stagePre());
        _lbOk.text = langs.Ok();
        _lbCancel.text = langs.Cancel();
        gameObject.SetActive(true);
    }

    #region UI Action

    public void OnBtn_Ok()
    {
        int idx = int.Parse(_input.text) - 1;
        gameObject.SetActive(false);
        _root.SetActive(false);

        if (idx < 0) {
            uis.pops.Show_Warning(langs.stageWrongNumber());
            return;
        }

        if (idx > dStage.Idx) {
            uis.pops.Show_Warning(langs.stageNeedToClearNext());
            return;
        }

        dStage.SetPlayingIdx(idx);
        uis.outgam.Inactive(ui_cover.State.LoadStage);
        core.Inst.flowMgr.Change<Flow_SoloPlay>();
    }

    public void OnBtn_Cancel()
    {
        gameObject.SetActive(false);
        _root.SetActive(false);
    }
    #endregion
}
