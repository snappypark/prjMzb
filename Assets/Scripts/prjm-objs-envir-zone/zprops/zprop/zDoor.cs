using System.Runtime.CompilerServices;
using UnityEngine;

public class zDoor : zprp
{
    public enum State
    {
        Locked = 0,
        Timer,
        None,
        MultiTimer,
    }

    [SerializeField] Transform _tranCube;
    [SerializeField] TextMesh _textMesh;
    [SerializeField] SpriteRenderer _spLock;
    [SerializeField] SpriteRenderer _minimap;

    zone.prp_ _info;
    State _state;

    // option: state, delay, 
    public override void Assign(zone.prp_ info)
    {
        _info = info;
        _state = (State)(_info.opts.F1 + 0.1f);
        _tranCube.localScale = info.Sz;

        refreshByType();
    }

    void Update()
    {
        switch (_state)
        {
            case State.Locked:
                break;
            case State.Timer:
                if (_info.timin.IsEnd())
                    remove();
                else
                {
                    float remain = _info.timin.fRemain();
                    _textMesh.text = string.Format("{0:0}", (int)remain);

                    if (_info.checkForPrePath && remain < _info.zn.durationOfJsp)
                    {
                        setPrePath();
                        _info.checkForPrePath = false;
                    }
                }
                break;
            case State.MultiTimer:
                if (core.multis.IsPlaying)
                {
                    remove();
                    _state = State.None;
                }
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void refreshByType()
    {
        switch (_state)
        {
            case State.Locked:
            case State.MultiTimer:
                _textMesh.gameObject.SetActive(false);
                _spLock.enabled = true;
                break;
            case State.Timer:
                _textMesh.gameObject.SetActive(true);
                _spLock.enabled = false;

                if (_info.timin.CountAndCheck())
                    _info.timin.SetEnd(_info.opts.F2);
                else if (_info.timin.IsEnd())
                    remove();

                break;
            case State.None:
                break;
        }
    }

    void setPrePath()
    {
        for (int x = _info.bd.xMin; x <= _info.bd.xMax; ++x)
            for (int z = _info.bd.zMin; z <= _info.bd.zMax; ++z)
                core.zells[x, z].SetPrePath();
    }
    
    void remove()
    {
        zjs.zprps.Effect(_info);
        if(_info.checkForPrePath)
            _info.zn.changed = true;
        _info.zn.RemovePrp(_info.type, _info.cellType);

    }
}
