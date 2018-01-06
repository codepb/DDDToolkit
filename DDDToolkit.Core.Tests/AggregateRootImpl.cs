namespace DDDToolkit.Core.Tests
{
    class AggregateRootImpl<T> : AggregateRoot<T>
    {
        public void ApplyEvent(IDomainEvent domainEvent)
        {
            Apply(domainEvent);
        }
    }
}
