using System.Collections.Generic;

namespace Host.Common
{
    public class QueueThreadSafe<T>
    {
        private Queue<T> queue = new Queue<T>();
        private object locker = new object();

        public int Count
        {
            get
            {
                return this.queue.Count;
            }
        }

        public void Enqueue(T item)
        {
            lock (this.locker)
            {
                this.queue.Enqueue(item);
            }
        }
        public T Dequeue()
        {
            lock (this.locker)
            {
                return this.queue.Dequeue();
            }
        }
        public List<T> DequeueAll()
        {
            List<T> list = new List<T>();
            lock (this.locker)
            {
                while (this.queue.Count != 0)
                {
                    list.Add(this.queue.Dequeue());
                }
            }
            return list;
        }
    }
}