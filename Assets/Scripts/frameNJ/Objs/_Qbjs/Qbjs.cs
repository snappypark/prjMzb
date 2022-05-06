using System.Collections.Generic;

namespace nj
{
    public class Qbjs<T> where T : Qbj
    {
        protected Queue<T> _q = new Queue<T>();

        public int Count { get { return _q.Count; } }
        public T Dequeue { get { return _q.Dequeue(); } }
    }
}