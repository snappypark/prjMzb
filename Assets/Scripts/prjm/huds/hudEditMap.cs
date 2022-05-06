using UnityEngine;
using UnityEngine.UI;
using UnityEngineEx;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class hudEditMap : MonoBehaviour
{
    public void Clear()
    {
        GameObjectEx.DestroyChildren(this.transform);
    }
    
    #region Cel1l
    hudLine _cel1l = null;
    hudLine _select1 = null;
    public void DrawCel1l(Vector3 pos)
    {
        if (_cel1l == null)
            _cel1l = core.huds.lines.With(0.13f, 0.1f, Color.gray, transform, "cel1lHover(Clone)");
        _cel1l.ActiveRectOnXZ(cel1l.Center(pos) );
    }

    public void DrawCel1lSelect(Vector3 pos1, Vector3 pos2)
    {
        if (_select1 == null)
            _select1 = core.huds.lines.With(0.13f, 0.1f, Color.white, transform, "cel1lSelect(Clone)");
        _select1.ActiveRectOnXZ(cel1l.Pos00(pos1), cel1l.Pos11(pos2));
    }

    public hudLine CreateCel1l(Vector3 pos, Color color, float width = 0.2f, string name = "line(Clone)")
    {
        hudLine hud = core.huds.lines.With(width, 0.1f, color, transform, name);
        hud.ActiveRectOnXZ(pos.WithGapY(0.01f));
        return hud;
    }

    public hudLine CreateCel1lArea(i5 b1d, Color color, float width = 0.2f, string name = "line(Clone)")
    {
        hudLine hud = core.huds.lines.With(width, 0.1f, color, transform, name);
        hud.ActiveRectOnXZ(
            new Vector3(b1d.left, b1d.Y * 3 + 0.51f, b1d.bottom),
            new Vector3(b1d.right + 1, b1d.Y * 3 + 0.51f, b1d.top + 1));
        return hud;
    }
    #endregion

    #region Cel5l
    hudLine _cel5l = null;
    hudLine _select5 = null;
    public void DrawCel5l(Vector3 pos)
    {
        if (_cel5l == null)
            _cel5l = core.huds.lines.With(0.13f, 0.1f, Color.gray, transform, "cel5lHover(Clone)");
        _cel5l.ActiveRectOnXZ(cel5l.Center(pos), 2.5f );
    }

    public void DrawCel5lSelect(Vector3 pos1, Vector3 pos2)
    {
        if (_select5 == null)
            _select5 = core.huds.lines.With(0.2f, 0.1f, Color.white, transform, "cel5lSelect(Clone)");
        _select5.ActiveRectOnXZ(cel5l.Pos00(pos1), cel5l.Pos11(pos2) );
    }
    
    public hudLine CreateCel5lArea(i5 b5d, Color color, float width = 0.2f, string name = "line(Clone)")
    {
        hudLine hud = core.huds.lines.With(width, 0.1f, color, transform, name);
        hud.ActiveRectOnXZ(
            new Vector3(b5d.left * 5, b5d.Y*3+0.51f, b5d.bottom * 5),
            new Vector3(b5d.right * 5 + 5, b5d.Y*3+0.51f, b5d.top * 5 + 5));
        return hud;
    }
    #endregion

}
