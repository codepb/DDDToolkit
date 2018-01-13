using DDDToolkit.Core.Events;

namespace DDDToolkit.Core.Tests
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
