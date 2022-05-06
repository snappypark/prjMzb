using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

public class hudLine : MonoBehaviour
{
    LineRenderer _renderer;
    float _depth;
    bool _isDashed = false;

    public bool IsDashed { get { return _isDashed; } set { _isDashed = value; } }

    public void SetEndCap(int endCap)
    {
        _renderer.numCapVertices = endCap;
    }

    public void Init(LineRenderer renderer, float depth)
    {
        _renderer = renderer;
        _renderer.enabled = false;
        _depth = depth;
    }

    public void Active(Vector3 pos)
    {
        Active(pos, pos);
    }

    public void Active(Vector3 pos1, Vector3 pos2)
    {
        _renderer.enabled = true;
        _renderer.positionCount = 2;
        _renderer.SetPositions(new Vector3[] { pos1.WithGapY(_depth), pos2.WithGapY(_depth) });
    }

    public void ActiveRectOnXZ(Vector3 pos, float gap = 0.5f)
    {
        _renderer.enabled = true;
        _renderer.positionCount = 5;
        _renderer.SetPositions(
            new Vector3[] { pos.WithGapY(_depth) + new Vector3(-gap,0,-gap),
                            pos.WithGapY(_depth) + new Vector3(-gap,0,gap),
                            pos.WithGapY(_depth) + new Vector3(gap,0,gap),
                            pos.WithGapY(_depth) + new Vector3(gap,0,-gap),
                            pos.WithGapY(_depth) + new Vector3(-gap,0,-gap) });
    }

    public void ActiveRectOnXZ(Vector3 pos1, Vector3 pos2)
    {
        _renderer.enabled = true;
        _renderer.positionCount = 5;
        _renderer.SetPositions(
            new Vector3[] { pos1.WithGapY(_depth),
                            new Vector3(pos2.x,pos1.y,pos1.z).WithGapY(_depth),
                            new Vector3(pos2.x,pos1.y,pos2.z).WithGapY(_depth),
                            new Vector3(pos1.x,pos1.y,pos2.z).WithGapY(_depth),
                            pos1.WithGapY(_depth) });
    }

    public void Inactive()
    {
        _renderer.enabled = false;
    }

}
