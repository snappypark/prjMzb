using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace nj
{
    public class ObjsRollPool<T, U> : ObjsQuePool<T, U> where T : MonoBehaviour where U : qObj
    {
        protected int _bdX = 21, _bdZ = 21, _bdHalfX = 10, _bdHalfZ = 10;
        protected float _cellSzX, _cellSzY, _cellSzZ, _cellSzhalfX, _cellSzhalfY, _cellSzhalfZ; //half
        float _cellOverSzX, _cellOverSzY, _cellOverSzZ;

        Pt _preCt = new Pt(-1000, 0, -1000);
        Pt _curCt = new Pt(-1000, 0, -1000);

        protected override void _awake()
        {
            base._awake();
            _cellOverSzX = 1 / _cellSzX; _cellOverSzY = 1 / _cellSzY; _cellOverSzZ = 1 / _cellSzZ;
        }

        public void OnUpdate(Pt nextCenter)
        {
            if (_curCt == nextCenter)
                return;
            _preCt = _curCt;
            _curCt = new Pt(
                (int)(nextCenter.x* _cellOverSzX),
                (int)(nextCenter.y * _cellOverSzY),
                (int)(nextCenter.z * _cellOverSzZ));
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

        void SetUnShowGameObjs(int beginX, int beginZ, int sizeX, int sizeZ, int signX, int signZ, int y)
        {
            for (int i = 0; i < sizeX; ++i)
                for (int j = 0; j < sizeZ; ++j)
                    SetUnuseObj(beginX + i * signX, y, beginZ + j * signZ);
        }

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