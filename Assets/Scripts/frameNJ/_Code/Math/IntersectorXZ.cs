using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectorXZ
{


    static public bool Check_LineLine(Vector3 line1_pt1, Vector3 line1_pt2, Vector3 line2_pt1, Vector3 line2_pt2)
    {
        return Check_LineLine(line1_pt1.x, line1_pt1.z, line1_pt2.x, line1_pt2.z,
                                line2_pt1.x, line2_pt1.z, line2_pt2.x, line2_pt2.z);
    }

    static public bool Check_LineLine(float x1, float y1, float x2, float y2,
                                 float x3, float y3, float x4, float y4)
    {
        float y4y3 = y4 - y3;
        float y1y3 = y1 - y3;
        float y2y1 = y2 - y1;
        float x4x3 = x4 - x3;
        float x2x1 = x2 - x1;
        float x1x3 = x1 - x3;
        float denom = y4y3 * x2x1 - x4x3 * y2y1;
        float numera = x4x3 * y1y3 - y4y3 * x1x3;
        float numerb = x2x1 * y1y3 - y2y1 * x1x3;

        const float eps = 0.0001f;
        if (ABS(numera) < eps && ABS(numerb) < eps && ABS(denom) < eps)
        {   //Lines coincident (on top of each other)
            return true;
        }

        if (ABS(denom) < eps)
        {   //Lines parallel
            return false;
        }

        float mua = numera / denom;
        float mub = numerb / denom;
        if (mua < 0 || mua > 1 || mub < 0 || mub > 1)
        {   //No intersection
            return false;
        }
        else
        {   //Intersection
            return true;
        }
    }

    static public bool Check_LineCircle(Vector3 cPoint, float cRadius, Vector3 linePt0, Vector3 linePt1)
    {
        Vector2 point1 = new Vector2(linePt0.x, linePt0.z);
        Vector2 point2 = new Vector2(linePt1.x, linePt1.z);

        float dx, dy, A, B, C, det, t;

        dx = point2.x - point1.x;
        dy = point2.y - point1.y;

        A = dx * dx + dy * dy;
        B = 2 * (dx * (point1.x - cPoint.x) + dy * (point1.y - cPoint.z));
        C = (point1.x - cPoint.x) * (point1.x - cPoint.x) +
            (point1.y - cPoint.z) * (point1.y - cPoint.z) -
            cRadius * cRadius;

        det = B * B - 4 * A * C;
        if ((A <= 0.0000001) || (det < 0))
            return false;

        return true;
    }

    //http://csharphelper.com/blog/2014/09/determine-where-a-line-intersects-a-circle-in-c/
    static public int Find_LineCircle(
                    float cx, float cy, float radius,
                    Vector2 point1, Vector2 point2,
                    out Vector2 intersection1, out Vector2 intersection2)
    {
        float dx, dy, A, B, C, det, t;

        dx = point2.x - point1.x;
        dy = point2.y - point1.y;

        A = dx * dx + dy * dy;
        B = 2 * (dx * (point1.x - cx) + dy * (point1.y - cy));
        C = (point1.x - cx) * (point1.x - cx) +
            (point1.y - cy) * (point1.y - cy) -
            radius * radius;

        det = B * B - 4 * A * C;
        if ((A <= 0.0000001) || (det < 0))
        {
            // No real solutions.
            intersection1 = new Vector2(float.NaN, float.NaN);
            intersection2 = new Vector2(float.NaN, float.NaN);
            return 0;
        }
        else if (det == 0)
        {
            // One solution.
            t = -B / (2 * A);
            intersection1 =
                new Vector2(point1.x + t * dx, point1.y + t * dy);
            intersection2 = new Vector2(float.NaN, float.NaN);
            return 1;
        }
        else
        {
            // Two solutions.
            t = (float)((-B + Mathf.Sqrt(det)) / (2 * A));
            intersection1 =
                new Vector2(point1.x + t * dx, point1.y + t * dy);
            t = (float)((-B - Mathf.Sqrt(det)) / (2 * A));
            intersection2 =
                new Vector2(point1.x + t * dx, point1.y + t * dy);
            return 2;
        }
    }


    static float ABS(float num)
    {
        return (num < 0) ? -num : num;
    }

}
