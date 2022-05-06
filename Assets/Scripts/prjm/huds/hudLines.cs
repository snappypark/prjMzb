using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

[System.Serializable]
public class hudLines
{
    [SerializeField] Material MatDefaultLine;
    [SerializeField] Material MatDashedLine;

    const string _NAME_LINE_RENDER = "line";
    const string _NAME_DASHLINE_RENDER = "dashline";

    public hudLine With(float width, float depth,
        Color color, Transform parent, string name = null)
    {
        return With(Vector3.zero, Vector3.zero, width, depth, color, parent, name);
    }

    public hudLine With(
        Vector3 p0, Vector3 p1, float width, float depth,
        Color color, Transform parent, string name = null)
    {
        GameObject gameObj = CreateGameObject.With(name ?? _NAME_LINE_RENDER, parent);
        LineRenderer lineRenderer = gameObj.AddComponent<LineRenderer>();
        hudLine lineDraw = gameObj.AddComponent<hudLine>();

        InitLine_CommonOption(lineRenderer, p0, p1, width, color);
        lineRenderer.material = MatDefaultLine;

        lineDraw.Init(lineRenderer, depth);
        return lineDraw;
    }

    public hudLine WithDashDot(
        float width, float depth, Color color, Transform parent)
    {
        return WithDashDot(Vector3.zero, Vector3.zero, width, depth, color, parent);
    }

    public hudLine WithDashDot(
        Vector3 p0, Vector3 p1, float width, float depth,
        Color color, Transform parent)
    {
        GameObject gameObj = CreateGameObject.With(_NAME_DASHLINE_RENDER, parent);
        LineRenderer lineRenderer = gameObj.AddComponent<LineRenderer>();
        hudLine lineDraw = gameObj.AddComponent<hudLine>();

        InitLine_CommonOption(lineRenderer, p0, p1, width, color);
        lineRenderer.material = MatDashedLine;

        lineDraw.Init(lineRenderer, depth);
        lineDraw.IsDashed = true;
        return lineDraw;
    }

    void InitLine_CommonOption(LineRenderer line, Vector3 p0, Vector3 p1, float width, Color color)
    {
        line.DisableRendererOption();
        line.widthMultiplier = width;//endWidth, startWidth = width;
        line.numCornerVertices = 4;
        line.numCapVertices = 4;
        line.startColor = color;
        line.endColor = color;
        line.SetPositions(new Vector3[] { p0, p1 });
    }
}
