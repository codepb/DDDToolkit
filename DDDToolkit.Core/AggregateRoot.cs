using System.Collections.Generic;

namespace DDDToolkit.Core
{
    public abstract class AggregateRoot<T> : Entity<T>
    {
        private readonly IEventQueue<IDomainEvent> _eventQueue = new EventQueue<IDomainEvent>();
        private readonly IEventHandler _eventHandler;
        public int Version { get; private set; }

        protected AggregateRoot()
        {
            _eventHandler = new EventHandler(this);
        }

        public void SetId(T id)
        {
            Id = id;
        }

        public IReadOnlyCollection<IDomainEvent> DequeueAllEvents()
        {
            return _eventQueue.Dequeue();
        }

        protected void Apply(IDomainEvent @event, bool isNew = true)
        {
            Version++;
            @event.Version = Version;

            CallEventHandler(@event);

            if(isNew)
            {
                _eventQueue.Enqueue(@event);
            }
        }

        private void CallEventHandler(IDomainEvent @event)
        {
            _eventHandler.Handle(@event);
        }

        
    }
}
