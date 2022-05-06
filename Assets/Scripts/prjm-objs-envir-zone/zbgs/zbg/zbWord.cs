using UnityEngine;

public class zbWord : zbg
{
    public enum Type {
        Word_Stage,
        This_Way_Left,
        This_Way_Right,
        Welcome,
    }
    public enum PosType { Up, Down, }

    [SerializeField] TextMesh _txt;
    [SerializeField] TextMesh _txtfr;

    // option: Type, PosType, size, angle
    public override void Assign(ref zone.zbg_ info)
    {
        Type type = (Type)info.opts.F1;
        switch (type) {
            case Type.Word_Stage:
                _txt.text = langs.stageSharp(dStage.PlayingIdx, dStage.LastIdx);
                break;
            case Type.This_Way_Left:
                _txt.text = "☜ This Way";
                break;
            case Type.This_Way_Right:
                _txt.text = "This Way ☞";
                break;
            case Type.Welcome:
                _txt.text = "Welcome";
                break;
        }

        PosType posType = (PosType)info.opts.F2;
        switch (posType)
        {
            case PosType.Up:
                _txt.transform.localPosition = new Vector3(0, 1, 0.45f);
                _txtfr.transform.localPosition = new Vector3(0, 1, 0.4f);
                _txt.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case PosType.Down:
                _txt.transform.localPosition = new Vector3(0, 0.05f, 0.0f);
                _txtfr.transform.localPosition = _txt.transform.localPosition;
                _txt.transform.localEulerAngles = new Vector3(90, 0, 0);
                break;
        }

        transform.localScale = new Vector3(info.opts.F3, info.opts.F3, info.opts.F3);

        _txtfr.transform.localEulerAngles = _txt.transform.localEulerAngles;
        _txtfr.text = _txt.text;
        // _textMesh.color = matWalls.Col(matWalls.GreenLight) + new Color(0.2f, 0.2f, 0.2f, 0.0f);

    }



}
