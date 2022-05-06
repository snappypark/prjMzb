using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class unitClones /*.ally : */
{
    public class ally_
    {
        public Queue<short>[] qs = new Queue<short>[allyMax] { new Queue<short>(), new Queue<short>(), new Queue<short>(), new Queue<short>(), new Queue<short>(), new Queue<short>() };
        public Queue<short> this[int ally] { get { return qs[ally]; } }

        public void Clear()
        {
            for (byte i = 0; i < allyMax; ++i)
                qs[i].Clear();
        }
        public unit RollTryUnit(int ally)
        {
            short cdx = qs[ally].Dequeue();
            qs[ally].Enqueue(cdx);
            return core.unitClones[cdx];
        }
    }

}
