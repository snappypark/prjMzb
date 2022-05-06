using UnityEngine;

public class pLamp : rpr
{
    cel1l _cell;
    //plight _light = null;

    [SerializeField] GameObject _on;
    [SerializeField] GameObject _off;

    public override void Assign(cel1l cell)
    {
        _cell = cell;
        switch (_cell.type)
        {
            case cel1l.Type.LambOn:
                _on.SetActive(true);
                _off.SetActive(false);
                break;
            case cel1l.Type.LambOff:
                _on.SetActive(false);
                _off.SetActive(true);
                break;
        }
        gameObject.SetActive(true);
    }

    public override void UnAssign()
    {
        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        switch (_cell.type)
        {
            case cel1l.Type.LambOn:
                if (core.skx == core.lastskx)
                {
                    i4 range = new i4(_cell.pt.x-1, _cell.pt.z-1, _cell.pt.x+1, _cell.pt.z+1);
                    for (int x = range.X0; x <= range.X1; ++x)
                        for (int z = range.Z0; z <= range.Z1; ++z)
                            core.sights.textRoller.SetNextPixel(x, z, new Color(1, 1, 1));
                }
                    
                break;
            case cel1l.Type.LambOff:
                break;
        }
    }
}
