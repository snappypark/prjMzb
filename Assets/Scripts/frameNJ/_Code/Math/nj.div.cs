using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nj
{ 
    public static class div
    {
        static float[] _arr = new float[]{ 0,
            1.0f, 0.5f, 0.3333333333333f, 0.25f, 0.2f,
            0.1666666666666667f, 0.1428571428571428f, 0.125f, 0.1111111111111111f, 0.1f,
            0.0909090909090909f, 0.0833333333333333f, 0.0769230769230769f, 0.0714285714285714f, 0.0666666666666667f,
            0.0625f, };

        public static float By(int num)
        {
            return _arr[num];
        }
    }
}
