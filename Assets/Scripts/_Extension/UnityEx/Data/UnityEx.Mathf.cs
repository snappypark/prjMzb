using UnityEngine;

namespace UnityEngineEx
{
    public static class MathfEx
    {
        //Clamp list indices
        //Will even work if index is larger/smaller than listSize, so can loop multiple times
        public static int ClampListIndex(int index, int listSize)
        {
            return ((index % listSize) + listSize) % listSize;
        }
    }

}

