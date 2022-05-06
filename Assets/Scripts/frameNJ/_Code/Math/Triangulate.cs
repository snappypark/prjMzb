using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineEx;

public class Triangulate
{
    /**
 	* @brief 점들을 XZ 좌표계 기준으로 로 변환한다.\n
    * XZ기준으로 Triangulate을 하기 때문이다. \n
    * Triangulate와 같이 좀더 유연한 코드로 개선 필요하다. \n
 	* @param int points - 변환할 점들
 	* @param Vector3 origin - XZ좌표 기준이될 로컬 좌표계 원점
 	* @param Vector3 AxisX  - XZ좌표 기준이될 로컬 좌표계 X축
 	* @param Vector3 AxisZ  - XZ좌표 기준이될 로컬 좌표계 Z축
 	* @return XZ - XZ좌표계로 변환된 점들.
 	*/
    public static List<Vector3> NormalizePoints(List<Vector3> points, Vector3 origin, Vector3 AxisX, Vector3 AxisZ)
    {
        List<Vector3> result = new List<Vector3>();
        Vector3 norAxisX = AxisX.normalized;
        Vector3 norAxisZ = AxisZ.normalized;

        for (int i = 0; i < points.Count; ++i)
        {
            Vector3 X = Vector3.Project(points[i] - origin, norAxisX);
            Vector3 Z = Vector3.Project(points[i] - origin, norAxisZ);
            result.Add(new Vector3(X.magnitude, 0, Z.magnitude));
        }

        return result;
    }

    public static List<int> GetRect(List<Vector3> convexHullpoints)
    {
        List<int> tris = new List<int>();

        tris.Add(0);
        tris.Add(1);
        tris.Add(2);

        tris.Add(2);
        tris.Add(3);
        tris.Add(0);

        return tris;
    }

    public static List<int> GetConvexPolygon2(List<Vector3> convexHullpoints)
    {
        List<int> tris = new List<int>();

        for (int i = 2; i < convexHullpoints.Count; i++)
        {
            tris.Add(0);
            tris.Add(i - 1);
            tris.Add(i);
        }

        return tris;
    }

    // http://www.habrador.com/tutorials/math/10-triangulation/ 참조
    public static List<int> GetConvexPolygon(List<Vector3> convexHullpoints)
    {
        List<int> tris = new List<int>();

        for (int i = 2; i < convexHullpoints.Count; i++)
        {
            tris.Add(i - 2);
            tris.Add(i - 1);
            tris.Add(i);
        }
        
        return tris;
    }

    // http://www.habrador.com/tutorials/math/10-triangulation/ 참조
    // 트라이엥글 인덱스 배열을 리턴하게 수정
    //This assumes that we have a polygon and now we want to triangulate it
    //The points on the polygon should be ordered counter-clockwise
    //This alorithm is called ear clipping and it's O(n*n) Another common algorithm is dividing it into trapezoids and it's O(n log n)
    //One can maybe do it in O(n) time but no such version is known
    //Assumes we have at least 3 points
    public static List<int> GetFromConcavePolygon(List<Vector3> points)
    {
        //The list with triangles the method returns
        List<Triangle> triangles = new List<Triangle>();
        List<int> tris = new List<int>();

        //If we just have three points, then we dont have to do all calculations
        if (points.Count == 3)
        {
            //triangles.Add(new Triangle(points[0], points[1], points[2]));
            tris.Add(0); tris.Add(1); tris.Add(2);
            return tris;
        }

        //Step 1. Store the vertices in a list and we also need to know the next and prev vertex
        List<Vertex> vertices = new List<Vertex>();

        for (int i = 0; i < points.Count; i++)
        {
            vertices.Add(new Vertex(points[i], i));
        }

        //Find the next and previous vertex
        for (int i = 0; i < vertices.Count; i++)
        {
            int nextPos = MathfEx.ClampListIndex(i + 1, vertices.Count);

            int prevPos = MathfEx.ClampListIndex(i - 1, vertices.Count);

            vertices[i].prevVertex = vertices[prevPos];

            vertices[i].nextVertex = vertices[nextPos];
        }

        //Step 2. Find the reflex (concave) and convex vertices, and ear vertices
        for (int i = 0; i < vertices.Count; i++)
        {
            CheckIfReflexOrConvex(vertices[i]);
        }

        //Have to find the ears after we have found if the vertex is reflex or convex
        List<Vertex> earVertices = new List<Vertex>();

        for (int i = 0; i < vertices.Count; i++)
        {
            IsVertexEar(vertices[i], vertices, earVertices);
        }

        //Step 3. Triangulate!
        while (true)
        {
            //This means we have just one triangle left
            if (vertices.Count == 3)
            {
                //The final triangle
                triangles.Add(new Triangle(vertices[0], vertices[1], vertices[2]));

                break;
            }

            //Make a triangle of the first ear
            Vertex earVertex = earVertices[0];

            Vertex earVertexPrev = earVertex.prevVertex;
            Vertex earVertexNext = earVertex.nextVertex;

            Triangle newTriangle = new Triangle(earVertex, earVertexPrev, earVertexNext);

            triangles.Add(newTriangle);

            //Remove the vertex from the lists
            earVertices.Remove(earVertex);

            vertices.Remove(earVertex);

            //Update the previous vertex and next vertex
            earVertexPrev.nextVertex = earVertexNext;
            earVertexNext.prevVertex = earVertexPrev;

            //...see if we have found a new ear by investigating the two vertices that was part of the ear
            CheckIfReflexOrConvex(earVertexPrev);
            CheckIfReflexOrConvex(earVertexNext);

            earVertices.Remove(earVertexPrev);
            earVertices.Remove(earVertexNext);

            IsVertexEar(earVertexPrev, vertices, earVertices);
            IsVertexEar(earVertexNext, vertices, earVertices);
        }

        //Debug.Log(triangles.Count);

        for (int i = 0; i < triangles.Count; ++i)
        {
            tris.Add(triangles[i].v1.Idx);
            tris.Add(triangles[i].v2.Idx);
            tris.Add(triangles[i].v3.Idx);
        }
        return tris;
    }

    //Check if a vertex if reflex or convex, and add to appropriate list
    private static void CheckIfReflexOrConvex(Vertex v)
    {
        v.isReflex = false;
        v.isConvex = false;

        //This is a reflex vertex if its triangle is oriented clockwise
        Vector2 a = v.prevVertex.GetPos2D_XZ();
        Vector2 b = v.GetPos2D_XZ();
        Vector2 c = v.nextVertex.GetPos2D_XZ();

        if (Geometry.IsTriangleOrientedClockwise(a, b, c))
        {
            v.isReflex = true;
        }
        else
        {
            v.isConvex = true;
        }
    }

    //Check if a vertex is an ear
    private static void IsVertexEar(Vertex v, List<Vertex> vertices, List<Vertex> earVertices)
    {
        //A reflex vertex cant be an ear!
        if (v.isReflex)
        {
            return;
        }

        //This triangle to check point in triangle
        Vector2 a = v.prevVertex.GetPos2D_XZ();
        Vector2 b = v.GetPos2D_XZ();
        Vector2 c = v.nextVertex.GetPos2D_XZ();

        bool hasPointInside = false;

        for (int i = 0; i < vertices.Count; i++)
        {
            //We only need to check if a reflex vertex is inside of the triangle
            if (vertices[i].isReflex)
            {
                Vector2 p = vertices[i].GetPos2D_XZ();

                //This means inside and not on the hull
                if (IsPointInTriangle(a, b, c, p))
                {
                    hasPointInside = true;

                    break;
                }
            }
        }

        if (!hasPointInside)
        {
            earVertices.Add(v);
        }
    }

    //From http://totologic.blogspot.se/2014/01/accurate-point-in-triangle-test.html 참조
    //p is the testpoint, and the other points are corners in the triangle
    public static bool IsPointInTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p)
    {
        bool isWithinTriangle = false;

        //Based on Barycentric coordinates
        float denominator = ((p2.y - p3.y) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.y - p3.y));

        float a = ((p2.y - p3.y) * (p.x - p3.x) + (p3.x - p2.x) * (p.y - p3.y)) / denominator;
        float b = ((p3.y - p1.y) * (p.x - p3.x) + (p1.x - p3.x) * (p.y - p3.y)) / denominator;
        float c = 1 - a - b;

        //The point is within the triangle or on the border if 0 <= a <= 1 and 0 <= b <= 1 and 0 <= c <= 1
        //if (a >= 0f && a <= 1f && b >= 0f && b <= 1f && c >= 0f && c <= 1f)
        //{
        //    isWithinTriangle = true;
        //}

        //The point is within the triangle
        if (a > 0f && a < 1f && b > 0f && b < 1f && c > 0f && c < 1f)
        {
            isWithinTriangle = true;
        }

        return isWithinTriangle;
    }

    //The list describing the polygon has to be sorted either clockwise or counter-clockwise because we have to identify its edges
    public static bool IsPointInPolygon(List<Vector2> polygonPoints, Vector2 point)
    {
        //Step 1. Find a point outside of the polygon
        //Pick a point with a x position larger than the polygons max x position, which is always outside
        Vector2 maxXPosVertex = polygonPoints[0];

        for (int i = 1; i < polygonPoints.Count; i++)
        {
            if (polygonPoints[i].x > maxXPosVertex.x)
            {
                maxXPosVertex = polygonPoints[i];
            }
        }

        //The point should be outside so just pick a number to make it outside
        Vector2 pointOutside = maxXPosVertex + new Vector2(10f, 0f);

        //Step 2. Create an edge between the point we want to test with the point thats outside
        Vector2 l1_p1 = point;
        Vector2 l1_p2 = pointOutside;

        //Step 3. Find out how many edges of the polygon this edge is intersecting
        int numberOfIntersections = 0;

        for (int i = 0; i < polygonPoints.Count; i++)
        {
            //Line 2
            Vector2 l2_p1 = polygonPoints[i];

            int iPlusOne = MathfEx.ClampListIndex(i + 1, polygonPoints.Count);

            Vector2 l2_p2 = polygonPoints[iPlusOne];

            //Are the lines intersecting?
            if (Math_.AreLinesCrossed(l1_p1, l1_p2, l2_p1, l2_p2, true))
            {
                numberOfIntersections += 1;
            }
        }

        //Step 4. Is the point inside or outside?
        bool isInside = true;

        //The point is outside the polygon if number of intersections is even or 0
        if (numberOfIntersections == 0 || numberOfIntersections % 2 == 0)
        {
            isInside = false;
        }

        return isInside;
    }
}

public class Geometry
{
    //Is a triangle in 2d space oriented clockwise or counter-clockwise
    //https://math.stackexchange.com/questions/1324179/how-to-tell-if-3-connected-points-are-connected-clockwise-or-counter-clockwise
    //https://en.wikipedia.org/wiki/Curve_orientation
    public static bool IsTriangleOrientedClockwise(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        bool isClockWise = true;

        float determinant = p1.x * p2.y + p3.x * p1.y + p2.x * p3.y - p1.x * p3.y - p3.x * p2.y - p2.x * p1.y;

        if (determinant > 0f)
        {
            isClockWise = false;
        }

        return isClockWise;
    }
}
