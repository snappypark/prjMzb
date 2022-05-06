using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using Beebyte.Obfuscator;

public partial class ui_ingam : MonoBehaviour
{
    public enum State : byte
    {
        none = 0,
        solo,
        multi_wait,
        multi_center,
        multi_ply,
    }

    [SerializeField] public pn_multi multi;
    [SerializeField] public pn_play play;
    [SerializeField] public pn_solo solo;
    
    [SerializeField] Text[] _equipNums;
    [SerializeField] Image[] _imgCoolTimes;

    public void Active(State state_, ui_cover.State coverState = ui_cover.State.None)
    {
        gameObject.SetActive(state_ != State.none);
        uis.cover.SetState(coverState);
        multi.OnActive(state_);
        play.OnActive(state_);
        solo.OnActive(state_);
    }
    
    nj.idxArr _sfx = new nj.idxArr(6);
    void Update()//
    {
        switch(_sfx.GetAfterRoll())
        {
            case 0:
                _imgCoolTimes[0].fillAmount = ctrls.Unit.wp.dlyMelee.Ratio10();
                break;
            case 1:
                _imgCoolTimes[1].fillAmount = ctrls.Unit.wp.dlyPistol.Ratio10();
                break;
            case 2:
                _imgCoolTimes[2].fillAmount = ctrls.Unit.wp.dlyRifle.Ratio10();
                break;
            case 3:
                _imgCoolTimes[3].fillAmount = ctrls.Unit.wp.dlyBomb.Ratio10();
                break;
            case 4:
                play.lbCountdownRemain.OnUpdate();
                break;
            case 5: break;
        }
    }


    public void RefreshAllWeaponNum(unit.weapon wp)
    {
        _equipNums[1].text = string.Format("{0}", wp.AmmoPistol.value);
        _equipNums[2].text = string.Format("{0}", wp.AmmoRifle.value);
        _equipNums[3].text = string.Format("{0}", wp.Bomb.value);
    }
    public void RefreshWeaponNum(unit.weapon wp)
    {
        switch (wp.state)
        {
            case model.Equip.Pistol:
                _equipNums[1].text = string.Format("{0}", wp.AmmoPistol.value);
                break;
            case model.Equip.Rifle:
                _equipNums[2].text = string.Format("{0}", wp.AmmoRifle.value);
                break;
            case model.Equip.Bomb:
                _equipNums[3].text = string.Format("{0}", wp.Bomb.value);
                break;
        }
    }
    public void RefreshPistolNum(int num){
        _equipNums[1].text = string.Format("{0}", num); }
    public void RefreshRifleNum(int num){
        _equipNums[2].text = string.Format("{0}", num); }
    public void RefreshBombNum(int num){
        _equipNums[3].text = string.Format("{0}", num); }
}
