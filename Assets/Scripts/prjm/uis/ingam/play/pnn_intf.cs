using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using Beebyte.Obfuscator;

public class pnn_intf : MonoBehaviour
{
    [SerializeField] Image[] _equipSlots;
    [SerializeField] Image[] _equipSlotIcons;
    [SerializeField] GameObject[] _slotNumbers;

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
        for (byte i = 0; i < 4; ++i) _slotNumbers[i].SetActive(false);
#else
        for (byte i = 0; i < 4; ++i) _slotNumbers[i].SetActive(true);
#endif
    }

    public void OnActive(ui_ingam.State state_)
    {
        bool isPlay = state_ == ui_ingam.State.solo || state_ == ui_ingam.State.multi_ply;
        gameObject.SetActive(isPlay);
        
        if (isPlay) /*---*/ refreshEquipSlot((byte)ctrls.Unit.wp.state);
    }

#if !(UNITY_ANDROID || UNITY_IOS) || UNITY_EDITOR
    void Update()//
    {
        if (EventSystem.current.IsPointerOverGameObject()) /*---*/ return;
       // if (Input.GetKeyDown(KeyCode.Alpha1)) OnBtn_W0();
        if (Input.GetKeyDown(KeyCode.Alpha1)) OnBtn_W1();
        else if (Input.GetKeyDown(KeyCode.Alpha2)) OnBtn_W2();
        else if (Input.GetKeyDown(KeyCode.Alpha3)) OnBtn_W3();
    }
#endif


    #region UI Action
    //[SkipRename] public void OnBtn_W0() { refreshEquip_ByBtn(model.Equip.Melee); }
    [SkipRename] public void OnBtn_W1() { refreshEquip_ByBtn(model.Equip.Pistol); }
    [SkipRename] public void OnBtn_W2() { refreshEquip_ByBtn(model.Equip.Rifle); }
    [SkipRename] public void OnBtn_W3() { refreshEquip_ByBtn(model.Equip.Bomb); }
    #endregion

    void refreshEquip_ByBtn(model.Equip state_)
    {
        ctrls.Unit.SetEquip(ctrls.Unit.wp.state == state_ ? model.Equip.None : state_);
        refreshEquipSlot((byte)ctrls.Unit.wp.state);
    }

    Color _colOnEquip = new Color(0.3523051f, 0.7584906f, 0.4232504f, 0.7f);
    Color _colOffEquip = new Color(0.5073158f, 0.5400414f, 0.5867924f, 0.45f);
    Color _colOnEquipIcon = new Color(1.0f, 1.0f, 1.0f, 0.9f);
    Color _colOffEquipIcon = new Color(1.0f, 1.0f, 1.0f, 0.3f);
    void refreshEquipSlot(byte state_)
    {
        for (byte i = 0; i < 4; ++i)
        {
            _equipSlots[i].color = i == state_ ? _colOnEquip : _colOffEquip;
            _equipSlotIcons[i].color = i == state_ ? _colOnEquipIcon : _colOffEquipIcon;
        }
    }

}
