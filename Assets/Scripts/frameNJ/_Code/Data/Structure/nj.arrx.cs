using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nj
{
    public class arrx3d<T> : arr3d<T> where T : class, new()
    {
        public float IdxSzXorZ;
        public float IdxHalfSzXorZ;
        public float OverIdxSzXorZ;

        public float IdxSzY;
        public float IdxHalfSzY;
        public float OverHalfSzY;

        public arrx3d(int maxX_, int maxY_, int maxZ_, 
            float idxSzXOrZ_, float idxSzY_, bool withAllocated = false) : base(maxX_, maxY_, maxZ_, withAllocated)
        {
            IdxSzXorZ = idxSzXOrZ_;
            IdxHalfSzXorZ = idxSzXOrZ_ * 0.5f;
            OverIdxSzXorZ = 1 / idxSzXOrZ_;

            IdxSzY = idxSzY_;
            IdxHalfSzY = idxSzY_ * 0.5f;
            OverHalfSzY = 1 / idxSzY_;
        }
        
    }
}
