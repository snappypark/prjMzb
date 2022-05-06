using UnityEngine;
//using System;

namespace nj
{
    public static class arrFunc
    {
        public static bool CheckHasValue(ref arr<int> arr_, int v_)
        {
            for (int i = 0; i < arr_.Num; ++i)
                if (arr_[i] == v_)
                    return true;
            return false;
        }

    }

    public class arr<T>
    {
        public int Num = 0;
        int _max = 0;
        T[] _objs = null;
        
        public int Max { get { return _max; } }
        public T this[int i] { get { return _objs[i]; } set { _objs[i] = value; } }

        public T Peek { get { return _objs[Num-1]; } }
        public arr(int max)
        {
            _max = max;
            _objs = new T[_max];
        }

        public void Reset_Add(T obj)
        {
            _objs[0] = obj;
            Num = 1;
        }
        public void Reset()
        {
            Num = 0;
        }
        public void Add(T obj)
        {
            if (Num < _max)
                _objs[Num++] = obj;
        }
        public void Remove(int idx)
        {
            if (idx < 0 || idx >= Num)
                return;
            _objs[idx] = _objs[Num - 1];
            --Num;
        }
        
    }

    public class arr3d<T> where T : class, new()
    {
        public arr3d(int maxX_, int maxY_, int maxZ_, bool initWithAllocated = false) 
        {
            _maxX = maxX_; _maxY = maxY_; _maxZ = maxZ_;
            _objs = new T[maxX_, maxY_, maxZ_];
            if (initWithAllocated)
            {
                for (int x = 0; x < maxX_; ++x)
                    for (int y = 0; y < maxY_; ++y)
                        for (int z = 0; z < maxZ_; ++z)
                            _objs[x, y, z] = new T();
            }
            else
            {
                for (int x = 0; x < maxX_; ++x)
                    for (int y = 0; y < maxY_; ++y)
                        for (int z = 0; z < maxZ_; ++z)
                            _objs[x, y, z] = null;
            }
        }

        public void Clear()
        {
            for (int x = 0; x < _maxX; ++x)
                for (int y = 0; y < _maxY; ++y)
                    for (int z = 0; z < _maxZ; ++z)
                        _objs[x, y, z] = null;
        }

        public bool IsEmpty(int x, int y, int z=0) { return null == _objs[x, y, z]; }
        public bool IsOutIdx(int x, int y, int z=0) { return (x < 0 || y < 0 || z < 0 || x >= _maxX || y >= _maxY || z >= _maxZ); }
        public bool IsOutIdx(Pt pt) { return IsOutIdx(pt.x, pt.y, pt.z); }

        protected T[,,] _objs = null;
        int _maxX, _maxY, _maxZ;
        public int MaxX { get { return _maxX; } }
        public int MaxY { get { return _maxY; } }
        public int MaxZ { get { return _maxZ; } }
        public virtual T this[int x, int y, int z = 0] { get { return _objs[x, y, z]; } set { _objs[x, y, z] = value; } }
        public virtual T this[Pt pt] { get { return _objs[pt.x, pt.y, pt.z]; } set { _objs[pt.x, pt.y, pt.z] = value; } }

        public T Front(Pt pt) { return pt.z + 1 >= MaxZ ? null : this[pt.x, pt.y, pt.z + 1]; }
        public T Back(Pt pt) { return pt.z - 1 < 0 ? null : this[pt.x, pt.y, pt.z - 1]; }
        public T Right(Pt pt) { return pt.x + 1 >= MaxX ? null : this[pt.x + 1, pt.y, pt.z]; }
        public T Left(Pt pt) { return pt.x - 1 < 0 ? null : this[pt.x - 1, pt.y, pt.z]; }
    }


    public class arr4d<T> where T : class
    {
        public arr4d(int maxX, int maxY, int maxZ, int maxW = 1)
        {
            m_maxNumX = maxX; m_maxNumY = maxY; m_maxNumZ = maxZ; m_maxNumW = maxW;
            m_Objs = new T[maxX, maxY, maxZ, maxW];
            for (int x = 0; x < maxX; ++x)
                for (int y = 0; y < maxY; ++y)
                    for (int z = 0; z < maxZ; ++z)
                        for (int w = 0; w < maxW; ++w)
                            m_Objs[x, y, z, w] = null;
        }

        protected T[,,,] m_Objs = null;
        int m_maxNumX, m_maxNumY, m_maxNumZ, m_maxNumW;
        public int MaxNumX { get { return m_maxNumX; } }
        public int MaxNumY { get { return m_maxNumY; } }
        public int MaxNumZ { get { return m_maxNumZ; } }
        public int MaxNumW { get { return m_maxNumW; } }
        public virtual T this[int x, int y, int z, int w = 1] { get { return m_Objs[x, y, z, w]; } set { m_Objs[x, y, z, w] = value; } }
    }

}
