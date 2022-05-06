using UnityEngine;

public class uj_wallsAbs : uj_abs
{
    protected enum matType
    {
        Deafult,
        UnitedByMiniMat,
        Metal,
    }

    [SerializeField] protected matType _matType = matType.UnitedByMiniMat;

    [HideInInspector] protected wal4Mat _mats;

    protected void setMats(matWalls.Type matMini, matWalls.Type matTop, matWalls.Type matSideUp, matWalls.Type matSideDown)
    {
        switch (_matType)
        {
            case matType.UnitedByMiniMat:
                _mats = wal4Mat.N(matMini);
                break;
            case matType.Metal:
                _mats = wal4Mat.N(matMini, matWalls.Metal);
                break;
            default:
                _mats = wal4Mat.N((byte)matMini, (byte)matTop, (byte)matSideUp, (byte)matSideDown);
                break;
        }

    }
}
