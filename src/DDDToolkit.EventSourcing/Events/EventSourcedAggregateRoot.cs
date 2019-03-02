using DDDToolkit.EventSourcing.EventHandling;
using DDDToolkit.EventSourcing.Queues;
using System.Collections.Generic;
using DDDToolkit.Core;

namespace DDDToolkit.EventSourcing.Events
{
    public class EventSourcedAggregateRoot<T> : AggregateRoot<T>, IEventSourcedAggregateRoot<T>
    {
        private readonly IEventQueue<IDomainEvent> _eventQueue = new EventQueue<IDomainEvent>();
        private readonly IEventHandler _eventHandler;
        public int Version { get; private set; }

        protected EventSourcedAggregateRoot()
        {
            _eventHandler = new EventHandler(this);
        }

        public IReadOnlyCollection<IDomainEvent> DequeueAllEvents() => _eventQueue.Dequeue();

        protected void Apply(IDomainEvent @event, bool isNew = true)
        {
            Version++;
            @event.Version = Version;

            CallEventHandler(@event);

            if (isNew)
            {
                _eventQueue.Enqueue(@event);
            }
        }

        private void CallEventHandler(IDomainEvent @event) => _eventHandler.Handle(@event);
    }
}
