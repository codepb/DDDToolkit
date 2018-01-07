using System.Collections.Generic;
using System.Linq;

namespace DDDToolkit.Core
{
    internal class EventQueue<T> : IEventQueue<T>
    {
        private readonly IList<T> _eventsPendingPersistence = new List<T>();

        public IReadOnlyCollection<T> Dequeue()
        {
            var events = _eventsPendingPersistence.ToList();

            _eventsPendingPersistence.Clear();

            return events;
        }

        public void Enqueue(T obj)
        {
            _eventsPendingPersistence.Add(obj);
        }
    }
}
