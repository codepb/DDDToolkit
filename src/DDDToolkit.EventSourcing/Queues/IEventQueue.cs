using System.Collections.Generic;

namespace DDDToolkit.EventSourcing.Queues
{
    internal interface IEventQueue<T>
    {
        IReadOnlyCollection<T> Dequeue();
        void Enqueue(T obj);
    }
}