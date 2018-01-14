using System.Collections.Generic;

namespace DDDToolkit.Core.Queues
{
    internal interface IEventQueue<T>
    {
        IReadOnlyCollection<T> Dequeue();
        void Enqueue(T obj);
    }
}