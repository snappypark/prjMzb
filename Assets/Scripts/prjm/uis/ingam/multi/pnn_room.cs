using UnityEngine;
using UnityEngine.UI;

public class pnn_room : MonoBehaviour
{
    [SerializeField] Text _name;
    [SerializeField] Text _lbMode;
    [SerializeField] Text _lbMember;
    [SerializeField] Text _lbBtnStart;
    [SerializeField] GameObject _btnStart;

    public void OnActive(bool active_)
    {
        gameObject.SetActive(active_);
        if (active_)
        {
            _name.text = core.multis.GetRoomName();
            _lbMode.text = core.multis.GetModeName();

            if (BoltNetwork.IsServer)
            {
                _lbBtnStart.text = langs.StartGame();
                _btnStart.SetActive(true);
            }
            else
            {
                _btnStart.SetActive(false);
            }
        }
    }

    int _num = 0;
    public void RefreshPlayerNum(int num)
    {
        if (_num == num)
            return;
        _num = num;
        _lbMember.text = langs.MemberMuber(_num);
    }
    
    #region UI Action
    public void OnBtn_StartGame() {
        if (uis.IsEnableBtnTime(2.7f))
            core.multis.StartGame();
    }

    public void OnBtn_Leave() {
        if (uis.IsEnableBtnTime(2.7f))
            nets.Inst.bolts.LeaveGame();
    }
    
    public void editName()
    {
        //     _btnStart.SetActive(Photon.Pun.PhotonNetwork.LocalPlayer.IsMasterClient);
        //if (string.IsNullOrEmpty(_inputRoomName.text))
        //    core.uis.popup.warning.Active(LangTable.CheckRoomName());
        //   return;
    }
    #endregion
}
