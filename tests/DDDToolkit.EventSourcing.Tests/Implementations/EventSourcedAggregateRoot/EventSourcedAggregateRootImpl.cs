using DDDToolkit.EventSourcing.Events;

namespace DDDToolkit.EventSourcing.Tests
{
    class EventSourcedAggregateRootImpl<T> : EventSourcedAggregateRoot<T>
    {
        public bool TestDomainEventHandled { get; private set; }
        public bool FallbackHandlerCalled { get; private set; }

        public void ApplyEvent(IDomainEvent domainEvent)
        {
            Apply(domainEvent);
        }

        private void Handle(TestDomainEvent @event)
        {
            TestDomainEventHandled = true;
        }

        private void Handle(IDomainEvent @event)
        {
            FallbackHandlerCalled = true;
        }
    }
}
