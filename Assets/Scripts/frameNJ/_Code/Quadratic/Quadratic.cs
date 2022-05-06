using UnityEngine;

//https://www.desmos.com/calculator/pfdwlq5qht
public static class Quadratic
{
    public static float Get(float input)
    {
        return Mathf.Abs((input - (int)input) - 0.5f);
    }

    public static float Get1(float input)
    {
        return Mathf.Abs((input - (int)(input * 0.5f) * 2) - 1.0f);
    }

    public static float Zigzag01(float input, float fast) // 
    {
        input = input * fast - 1;
        return Mathf.Abs((input - (int)(input * 0.5f) * 2) - 1.0f);
    }


    public static Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {   //http://tehc0dez.blogspot.com/2010/04/nice-curves-catmullrom-spline-in-c.html
        float t2 = t * t; float t3 = t2 * t;
        Vector3 ret = new Vector3(
            0.5f * ((2.0f * p1.x) +
            (-p0.x + p2.x) * t +
            (2.0f * p0.x - 5.0f * p1.x + 4 * p2.x - p3.x) * t2 +
            (-p0.x + 3.0f * p1.x - 3.0f * p2.x + p3.x) * t3),
            p3.y,
            0.5f * ((2.0f * p1.z) +
            (-p0.z + p2.z) * t +
            (2.0f * p0.z - 5.0f * p1.z + 4 * p2.z - p3.z) * t2 +
            (-p0.z + 3.0f * p1.z - 3.0f * p2.z + p3.z) * t3)
            );
        return ret;
    }

    /*
    void makeSmoothPath(ref Queue<Vector3> path)
    {
        path.Clear();
        Vector3 p0; Vector3 p1 = _wayPts.Pop();
        Vector3 p2 = _wayPts.Pop(); Vector3 p3 = _wayPts.Pop();
        path.Enqueue(p2);
        path.Enqueue(p3);
        while (_wayPts.Count > 0)
        {
            p0 = p1; p1 = p2;
            p2 = p3; p3 = _wayPts.Pop();
            path.Enqueue(p3);
            //for (int j = 1; j <= 4; j++)
            //    path.Enqueue(Quadratic.CatmullRom(p0, p1, p2, p3, 0.25f * j));
        }
    }*/
}
