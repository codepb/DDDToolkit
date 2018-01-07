using System.Collections.Generic;

namespace DDDToolkit.Core
{
    internal interface IEventQueue<T>
    {
        IReadOnlyCollection<T> Dequeue();
        void Enqueue(T obj);
    }
}