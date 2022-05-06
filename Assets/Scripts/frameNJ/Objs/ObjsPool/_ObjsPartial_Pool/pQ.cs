using System.Collections.Generic;

namespace nj
{
    public class pQ // Partial Quene
    {
        public int Num { get { return Q.Count; } }
        public Queue<int> Q = new Queue<int>();

        public void En(int cloneIdx)
        {
            Q.Enqueue(cloneIdx);
        }

        public int De()
        {
            return Q.Dequeue();
        }

    }
}