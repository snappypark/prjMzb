using UnityEngine;

public class uj_abs : MonoBehaviour
{
    public virtual uj_editmap.eState State { get { return uj_editmap.eState.Spawn; } }
    
    [HideInInspector] protected bool _createdFlagForEdit = false;
    public bool checkCreatedFlagOnce()
    {
        if (_createdFlagForEdit){
            _createdFlagForEdit = false;
            return true;
        }
        return false;
    }

    public virtual void Select(i5 b1d, i5 b5d) { }
    public virtual void Create(i5 b1d) { }
    public virtual void Delete(i5 b1d, i5 b5d) { }

    public static matWalls.Type matWall = matWalls.Type.BlueDark;
    public static matTiles.Type matTile = matTiles.Type.Gray;
}
