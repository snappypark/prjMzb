using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pnn_chat : MonoBehaviour
{
    [SerializeField] lstItemMsg[] _msgs;
    [SerializeField] uiRatio _ur;
    [SerializeField] InputField _inputField;

    bool _toggleBtn = false;

    bool _active = false;
    public void OnActive(bool active_)
    {
        if (_active == active_)
            return;

        _active = active_;
        gameObject.SetActive(_active);

        if (active_)
        {
            _toggleBtn = false;
            refreshUI_byToggle();
        }

    }

    void refreshUI_byToggle()
    {
        switch (_toggleBtn)
        {
            case true:
                _ur.Refresh(0.49f, 0.8f, 0.4f, 0.382f);
                break;
            case false:
                _ur.Refresh(0.49f, 0.92f, 0.4f, 0.14f);
                break;
        }

        if (_inputField.gameObject.activeSelf != _toggleBtn)
            _inputField.gameObject.SetActive(_toggleBtn);
    }

    #region UI Action

    public void OnBtn_Chat()
    {
        _toggleBtn = !_toggleBtn;
        refreshUI_byToggle();
        refreshChat();
    }

    public void OnInputField()
    {
        //RefreshChat_WithNewMsg(_inputField.text);
        if (_inputField.gameObject.activeSelf)
        {
            boltPlyor.Chat(_inputField.text);
            _inputField.text = string.Empty;
        }
    }

    #endregion

    void refreshChat()
    {
        int max = _msgs.Length;
        for (int i = 0; i < max; ++i)
            _msgs[i].Refresh(_toggleBtn, max);
    }

    public void RefreshChat_WithNewMsg(string msg)
    {
        // Debug.Log("chat: " + msg);
        int max = _msgs.Length;
        for (int i = 0; i < max; ++i)
            if (_msgs[i].RefreshCount(_toggleBtn, max))
                _msgs[i].lb.text = msg;
    }
}
