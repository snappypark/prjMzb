using System;
using UnityEngine;

public class ui_pops : MonoBehaviour
{
    public enum _type
    {
        Ok,
        YesOrNo,
        Warning,
    }

    [SerializeField] public ui_result result;
    [SerializeField] ui_popLast _popLast;




    public void Show_Ok(string str, Action okFunc = null)
    {
        _popLast.Show(_type.Ok, str, okFunc);
    }

    public void Show_YesOrNo(string str, Action okFunc = null)
    {
        _popLast.Show(_type.YesOrNo, str, okFunc);
    }

    public void Show_Warning(string str, Action okFunc = null)
    {
        _popLast.Show(_type.Warning, str, okFunc);
    }
}
