using System.Collections.Generic;

namespace FutureArbitrage.Util
{
    public class QueueThreadSafe<T> : Queue<T>
    {
        private object locker = new object();
        public new void Enqueue(T item)
        {
            lock (this.locker)
            {
                base.Enqueue(item);
            }
        }
        public new T Dequeue()
        {
            lock (this.locker)
            {
                return base.Dequeue();
            }
        }
        public List<T> DequeueAll()
        {
            List<T> list = new List<T>();
            lock (this.locker)
            {
                while (this.Count != 0)
                {
                    list.Add(base.Dequeue());
                }
            }
            return list;
        }
    }
}
