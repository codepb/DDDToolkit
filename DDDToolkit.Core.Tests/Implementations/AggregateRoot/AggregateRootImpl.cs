namespace DDDToolkit.Core.Tests
{
    class AggregateRootImpl<T> : AggregateRoot<T>
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
