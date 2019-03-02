using DDDToolkit.EventSourcing.Events;

namespace DDDToolkit.EventSourcing.Tests
{
    class TestDomainEvent : IDomainEvent
    {
        public int Version { get ; set; }
    }
}
