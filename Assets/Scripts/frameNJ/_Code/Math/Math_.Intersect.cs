using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math_
{
    #region Line and Line

    public static bool lineLineIntersection(Vector2 A, Vector2 B, Vector2 C, Vector2 D, out Vector2 result)
    {
        // Line AB represented as a1x + b1y = c1
        double a1 = B.y - A.y;
        double b1 = A.x - B.x;
        double c1 = a1 * (A.x) + b1 * (A.y);

        // Line CD represented as a2x + b2y = c2
        double a2 = D.y - C.y;
        double b2 = C.x - D.x;
        double c2 = a2 * (C.x) + b2 * (C.y);

        double determinant = a1 * b2 - a2 * b1;

        //if (determinant == 0)
        if (determinant < 0.000001f && determinant > -0.000001f)
        {
            // The lines are parallel. This is simplified
            // by returning a pair of FLT_MAX
            result = Vector2.zero;
            return false;
        }
        else
        {
            double x = (b2 * c1 - b1 * c2) / determinant;
            double y = (a1 * c2 - a2 * c1) / determinant;
            result = new Vector2((float)x, (float)y);
            return true;
        }
    }

    //Is a point c between point a and b (we assume all 3 are on the same line)
    private static bool IsPointBetweenPoints(Vector3 a, Vector3 b, Vector3 p)
    {
        bool isBetween = false;

        //Entire line segment
        Vector3 ab = b - a;
        //The intersection and the first point
        Vector3 ac = p - a;

        //Need to check 2 things: 
        //1. If the vectors are pointing in the same direction = if the dot product is positive
        //2. If the length of the vector between the intersection and the first point is smaller than the entire line
        if (Vector3.Dot(ab, ac) > 0f && ab.sqrMagnitude >= ac.sqrMagnitude)
        {
            isBetween = true;
        }

        return isBetween;
    }

    //https://blog.dakwamine.fr/?p=1943
    public static bool FindIntersectionTwoLines(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2, out Vector2 intersection)
    {
        float tmp = (B2.x - B1.x) * (A2.y - A1.y) - (B2.y - B1.y) * (A2.x - A1.x);

        if (tmp < 0.000001f && tmp > -0.000001f) // No solution!
        {
            intersection = Vector2.zero;
            return false;
        }

        float mu = ((A1.x - B1.x) * (A2.y - A1.y) - (A1.y - B1.y) * (A2.x - A1.x)) / tmp;

        intersection = new Vector2( B1.x + (B2.x - B1.x) * mu,
                                    B1.y + (B2.y - B1.y) * mu  );
        return true;
    }


    //http://thirdpartyninjas.com/blog/2008/10/07/line-segment-intersection/
    public static bool AreLinesCrossed(Vector2 l1_p1, Vector2 l1_p2, Vector2 l2_p1, Vector2 l2_p2, bool shouldIncludeEndPoints)
    {
        if (!shouldIncludeEndPoints)
        {
            Vector2 gap1 = (l1_p2 - l1_p1).normalized * 0.05f;
            Vector2 gap2 = (l2_p2 - l2_p1).normalized * 0.05f;
            l1_p1 += gap1;
            l1_p2 -= gap1;
            l2_p1 += gap2;
            l2_p2 -= gap2;
        }

        bool isIntersecting = false;

        float denominator = (l2_p2.y - l2_p1.y) * (l1_p2.x - l1_p1.x) - (l2_p2.x - l2_p1.x) * (l1_p2.y - l1_p1.y);

        //Make sure the denominator is > 0, if not the lines are parallel
        if (denominator != 0f)
        {
            float u_a = ((l2_p2.x - l2_p1.x) * (l1_p1.y - l2_p1.y) - (l2_p2.y - l2_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;
            float u_b = ((l1_p2.x - l1_p1.x) * (l1_p1.y - l2_p1.y) - (l1_p2.y - l1_p1.y) * (l1_p1.x - l2_p1.x)) / denominator;

            //Are the line segments intersecting if the end points are the same
            if (shouldIncludeEndPoints)
            {
                //Is intersecting if u_a and u_b are between 0 and 1 or exactly 0 or 1
                if (u_a >= 0f && u_a <= 1f && u_b >= 0f && u_b <= 1f)
                {
                    isIntersecting = true;
                }
            }
            else
            {
                //Is intersecting if u_a and u_b are between 0 and 1
                if (u_a > 0f && u_a < 1f && u_b > 0f && u_b < 1f)
                {
                    isIntersecting = true;
                }
            }

        }

        return isIntersecting;
    }


    #endregion

    //Get the coordinate if we know a ray-plane is intersecting
    public static Vector3 GetRayPlaneIntersectionCoordinate(Vector3 planePos, Vector3 planeNormal, Vector3 rayStart, Vector3 rayDir)
    {
        float denominator = Vector3.Dot(-planeNormal, rayDir);

        Vector3 vecBetween = planePos - rayStart;

        float t = Vector3.Dot(vecBetween, -planeNormal) / denominator;

        Vector3 intersectionPoint = rayStart + rayDir * t;

        return intersectionPoint;
    }


    //Is a line-plane intersecting?
    public static bool AreLinePlaneIntersecting(Vector3 planeNormal, Vector3 planePos, Vector3 linePos1, Vector3 linePos2)
    {
        bool areIntersecting = false;

        Vector3 lineDir = (linePos1 - linePos2).normalized;

        float denominator = Vector3.Dot(-planeNormal, lineDir);

        //No intersection if the line and plane are parallell
        if (denominator > 0.000001f || denominator < -0.000001f)
        {
            Vector3 vecBetween = planePos - linePos1;

            float t = Vector3.Dot(vecBetween, -planeNormal) / denominator;

            Vector3 intersectionPoint = linePos1 + lineDir * t;

            if (IsPointBetweenPoints(linePos1, linePos2, intersectionPoint))
            {
                areIntersecting = true;
            }
        }

        return areIntersecting;
    }

    //We know a line plane is intersecting and now we want the coordinate of intersection
    public static Vector3 GetLinePlaneIntersectionCoordinate(Vector3 planeNormal, Vector3 planePos, Vector3 linePos1, Vector3 linePos2)
    {
        Vector3 vecBetween = planePos - linePos1;

        Vector3 lineDir = (linePos1 - linePos2).normalized;

        float denominator = Vector3.Dot(-planeNormal, lineDir);

        float t = Vector3.Dot(vecBetween, -planeNormal) / denominator;

        Vector3 intersectionPoint = linePos1 + lineDir * t;

        return intersectionPoint;
    }


}
