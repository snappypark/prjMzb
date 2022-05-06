using System.Collections.Generic;
using System.Collections.GenericEx;
using UnityEngine;

public partial class zones
{
    nj.arr<int> _znsNew = new nj.arr<int>(16);
    nj.arr<int> _znsOld = new nj.arr<int>(16);
    
    Queue<int> _znsHaving = new Queue<int>();

    public void SetCurZn_Empty()
    {
        _cur = zone.Empty;
    }

    public void OnUpdate_NextZone(zone next, bool camMoveSmooth = true)
    {
        //Debug.Log(_cur.idx);
        if (_cur.idx == next.idx || next.idx == -1)
            return;
        
        _znsNew.Reset_Add(next.idx);
        for (int i = 0; i < next.adjx.Count; ++i)
            _znsNew.Add(next.adjx[i]);
        
        for (int i = 0; i < _znsNew.Num; ++i)
        {
            int idxNew = _znsNew[i];
            if (nj.arrFunc.CheckHasValue(ref _znsOld, idxNew))
                continue;

            //Debug.Log(": " + idxNew);
            _znsHaving.Enque_NoDupli(idxNew);
            zone zn = getZone(idxNew);
            if (zn != null)
                zn.having = true;
        }

        for (int i = 0; i < _znsOld.Num; ++i)
        {
            int idxOld = _znsOld[i];
            if (nj.arrFunc.CheckHasValue(ref _znsNew, idxOld))
                continue;

            _znsHaving.Enque_NoDupli(idxOld);
            zone zn = getZone(idxOld);
            if (zn != null)
                zn.having = false;
        }

        _znsOld.Reset();
        for (int i = 0; i < _znsNew.Num; ++i)
            _znsOld.Add(_znsNew[i]);

        //    Debug.Log("wall: " + core.walls.NumObj(0)); //Debug.Log("tiles: " + core.tiles.NumObj(0));
        _cur = next;
        _cur.OnEnter(camMoveSmooth);
    }

    void Update()//
    {
        int num = _znsHaving.Count;
        if (num == 0)
            return;

        for (int i = 0; i < num; ++i)
        {
            int idx = _znsHaving.Dequeue();
            if (_zones.Count <= idx)
                continue;

            if (_zones[idx].having)
                _znsHaving.Enqueue(idx);
            else
            {
                if (_zones[idx].HasTask_OnOff())
                    _znsHaving.Enqueue(idx);
                return;
            }
        }

        num = _znsHaving.Count;
        for (int i = 0; i < num; ++i)
        {
            int idx = _znsHaving.Dequeue();
            if (_zones.Count <= idx)
                continue;

            if (_zones[idx].HasTask_OnOn())
                _znsHaving.Enqueue(idx);
        }
    }

    zone getZone(int idx)
    {
        if (_zones.Count <= idx)
            return null;
        return _zones[idx];
    }

}
