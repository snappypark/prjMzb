using UnityEngine;

public class zArea : zprp
{
    [SerializeField] ParticleSystem _ps;
    [SerializeField] TextMesh _text;
    [SerializeField] Transform _ladder;
    [SerializeField] GameObject _hole;

    [SerializeField] SpriteRenderer _icon;
    [SerializeField] SpriteRenderer _minimapWin;
    [SerializeField] SpriteRenderer _minimapUp;
    [SerializeField] SpriteRenderer _minimapDown;

    public override void Assign(zone.prp_ info)
    {
        AssignZprp(info);

        _ps.transform.localScale = info.Sz*0.95f;
        var mainModule = _ps.main;
        
        _minimapWin.enabled = info.cellType == cel1l.Type.AreaWin;
        _minimapUp.enabled = info.cellType == cel1l.Type.AreaUpLadder;
        _minimapDown.enabled = info.cellType == cel1l.Type.AreaDownLadder;
        switch (info.cellType)
        {
            case cel1l.Type.AreaWin:
                _ladder.localPosition = new Vector3(0, 0, 0.6f);
                _ladder.gameObject.SetActive(true);
                _hole.SetActive(false);
                _icon.enabled = false;

                mainModule.startColor = Color.white;
                _text.color = Color.white;
                _text.text = "Finish";
                break;
            case cel1l.Type.AreaWayPoint:
                _ladder.gameObject.SetActive(false);
                _hole.SetActive(false);
                _icon.enabled = false;

                mainModule.startColor = Color.cyan;
                _text.color = Color.cyan;
                _text.text = "Check\nPoint";
                break;
            case cel1l.Type.AreaUpLadder:
                _ladder.localPosition = new Vector3(0, 0, 0.1f);
                _ladder.gameObject.SetActive(true);
                _hole.SetActive(false);
                _icon.enabled = true;
                _icon.flipY = false;

                mainModule.startColor = Color.white;
                _text.text = string.Empty;
                break;
            case cel1l.Type.AreaDownLadder:
                _ladder.localPosition = new Vector3(0, -0.6f, 0.1f);
                _ladder.gameObject.SetActive(true);
                _hole.SetActive(true);
                _icon.enabled = true;
                _icon.flipY = true;

                mainModule.startColor = Color.white;
                _text.text = string.Empty;

                break;
        }

        if (core.Inst.flowMgr.CurType == Flow.iTypeMenu)
        {
            _icon.enabled = false;
            _ps.gameObject.SetActive(false);
            return;
        }

        _ps.gameObject.SetActive(true);

    }

}
