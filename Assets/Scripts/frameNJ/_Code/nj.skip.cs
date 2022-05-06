using System;

namespace nj
{
    public struct skip
    {
        int _skipIdx, _skipCount;
        public skip(int skipCount) {
            _skipIdx = 1;
            _skipCount = skipCount;
        }

        public void OnUpdate(Action callback)
        {
            switch (_skipIdx)
            {
                case 0: callback(); _skipIdx = _skipCount;
                break;
                default:
                    --_skipIdx;
                    break;
            }
        }

        public void OnUpdate2(Action cb1, Action cb2)
        {
            switch (_skipIdx)
            {
                case 0:
                    cb1();
                    _skipIdx = _skipCount;
                    break;
                case 1:
                    cb2();
                    --_skipIdx;
                    break;
                default:
                    --_skipIdx;
                    break;
            }
        }

        public void OnUpdate4(Action cb1, Action cb2, Action cb3, Action cb4)
        {
            switch (_skipIdx)
            {
                case 0:
                    cb1();
                    _skipIdx = _skipCount;
                    break;
                case 1:
                    cb2();
                    --_skipIdx;
                    break;
                case 2:
                    cb3();
                    --_skipIdx;
                    break;
                case 3:
                    cb4();
                    --_skipIdx;
                    break;
                default:
                    --_skipIdx;
                    break;
            }
        }


        public void OnUpdate6(Action cb1, Action cb2, Action cb3, Action cb4)
        {
            switch (_skipIdx)
            {
                case 0:
                    cb1();
                    _skipIdx = _skipCount;
                    break;
                case 1:
                    cb2();
                    --_skipIdx;
                    break;
                case 2:
                    cb3();
                    --_skipIdx;
                    break;
                case 3:
                    cb4();
                    --_skipIdx;
                    break;
                default:
                    --_skipIdx;
                    break;
            }
        }
    }
}
