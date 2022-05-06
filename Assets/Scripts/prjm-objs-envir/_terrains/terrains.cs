using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrains : MonoBehaviour
{
    public enum eState
    {
        Basement = 0,
        Ground,
        Space,
    }

    public static terrains Inst;

    public static float Height = 5;
    
    [SerializeField] Material _mat0;
    [SerializeField] Material _mat1;

    void Awake()
    {
        Inst = this;
    }

    public void SetHeight(float height_)
    {
        /*
        Height = height_;
        _terrain.transform.localPosition
            = new Vector3(_terrain.transform.localPosition.x,
                        -Height,
                        _terrain.transform.localPosition.z);*/
    }

    public void SetState(eState state)
    {
        /*
        switch (state)
        {
            case eState.Basement:
                _terrain.materialTemplate = _mat0;
                _terrain.drawTreesAndFoliage = false;
                _terrain.gameObject.SetActive(true);
                break;
            case eState.Ground:
                _terrain.materialTemplate = _mat1;
                _terrain.drawTreesAndFoliage = true;
                _terrain.gameObject.SetActive(true);
                break;
            case eState.Space:
                _terrain.gameObject.SetActive(false);
                break;
        }
        */
    }
}
