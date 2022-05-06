using System.Runtime.CompilerServices;
using UnityEngine;

namespace nj
{ 
    public class ObjsRoller
    {
        protected int _bdX, _bdZ, _bdHalfX, _bdHalfZ;
        protected Vector3 _cellSz, _cellOvSz, _cellHalfSz;
        protected Pt _preCt = new Pt(-1000, 0, -1000);
        protected Pt _curCt = new Pt(-1000, 0, -1000);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ResetCt()
        {
            _preCt = new Pt(-1000, 0, -1000);
            _curCt = new Pt(-1000, 0, -1000);
        }

        public ObjsRoller(int bdX, int bdZ, 
            int cellSzX, int cellSzY, int cellSzZ)
        {
            _bdX = bdX; _bdZ = bdZ;
            _bdHalfX = _bdX >> 1; _bdHalfZ = _bdZ >> 1;
            _cellSz = new Vector3(cellSzX, cellSzY, cellSzZ);
            _cellOvSz = new Vector3(1/(float)cellSzX, 1/ (float)cellSzY, 1/ (float)cellSzZ);
            _cellHalfSz = _cellSz * 0.5f;
        }
        
        public void OnUpdate(Pt nextCenter)
        {
            Pt newPt = new Pt((int)(nextCenter.x * _cellOvSz.x),
                             nextCenter.y,
                             (int)(nextCenter.z * _cellOvSz.z));
            if (_curCt == newPt)
                return;
            _preCt = _curCt;
            _curCt = newPt;

            int y = nextCenter.y;
            Pt Gap = new Pt(_curCt.x - _preCt.x, _curCt.y - _preCt.y, _curCt.z - _preCt.z);
            int signX = (_curCt.x >= _preCt.x) ? 1 : -1;
            int signZ = (_curCt.z >= _preCt.z) ? 1 : -1;

            int beginX = _preCt.x - signX * _bdHalfX;
            int beginZ = _preCt.z - signZ * _bdHalfZ;
            int sizeX = Mathf.Clamp(Mathf.Abs(Gap.x), 0, _bdX);
            int sizeZ = Mathf.Clamp(Mathf.Abs(Gap.z), 0, _bdZ);


            SetUnShowGameObjs(beginX, beginZ, sizeX, sizeZ, signX, signZ, _curCt.y);
            SetUnShowGameObjs(beginX, beginZ + signZ * sizeZ,
                                sizeX, _bdZ - sizeZ,
                                signX, signZ, _curCt.y);
            SetUnShowGameObjs(beginX + signX * sizeX, beginZ,
                                _bdX - sizeX, sizeZ,
                                signX, signZ, _curCt.y);

            beginX = _curCt.x + signX * _bdHalfX;
            beginZ = _curCt.z + signZ * _bdHalfZ;

            SetShowGameObjs(beginX, beginZ, sizeX, sizeZ, signX, signZ, _curCt.y);
            SetShowGameObjs(beginX - signX * sizeX, beginZ,
                            _bdX - sizeX, sizeZ,
                            signX, signZ, _curCt.y);
            SetShowGameObjs(beginX, beginZ - signZ * sizeZ,
                            sizeX, _bdZ - sizeZ,
                            signX, signZ, _curCt.y);


            if (_curCt.y != _preCt.y)
            {
                for (int i = 0; i < _bdX; ++i)
                    for (int j = 0; j < _bdZ; ++j)
                    {
                        int x = _curCt.x + i - _bdHalfX;
                        int z = _curCt.z + j - _bdHalfZ;
                        SetUnuseObj(x, _preCt.y, z);
                        SetUseObj(x, _curCt.y, z);
                    }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SetUnShowGameObjs(int beginX, int beginZ, int sizeX, int sizeZ, int signX, int signZ, int y)
        {
            for (int i = 0; i < sizeX; ++i)
                for (int j = 0; j < sizeZ; ++j)
                    SetUnuseObj(beginX + i * signX, y, beginZ + j * signZ);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void SetShowGameObjs(int beginX, int beginZ, int sizeX, int sizeZ, int signX, int signZ, int y)
        {
            for (int i = 0; i < sizeX; ++i)
                for (int j = 0; j < sizeZ; ++j)
                    SetUseObj(beginX - i * signX, y, beginZ - j * signZ);
        }
        
        protected virtual void SetUnuseObj(int x, int y, int z) { }
        
        protected virtual void SetUseObj(int x, int y, int z) { }
    }
}
