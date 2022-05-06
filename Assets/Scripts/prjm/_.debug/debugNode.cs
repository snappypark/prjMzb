using UnityEngine;

public class debugNode : nj.qObj
{
    [SerializeField] TextMesh[] _dists;

    cel1l _cell = cel1l.Empty;
    public void Init(cel1l cell)
    {
        _cell = cell;
    }
    ///*
    void LateUpdate()
    {
        ///*
        for (int i = 0; i < 8; ++i)
        {
            if (_cell.JpDist(i) > 0)
                _dists[i].text = _cell.JpDist(i).ToString();
            else
                _dists[i].text = "";
        }//*/
        /*
        for (int i = 0; i < 8; ++i)
        {
            if (_cell.isJumpPoint )
                _dists[i].text = "s";
            else
                _dists[i].text = "";

        }
        */
    }//*/
}
