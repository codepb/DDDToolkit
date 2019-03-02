using DDDToolkit.EventSourcing.Events;

namespace DDDToolkit.EventSourcing.Tests
{
    class UnhandledDomainEvent : IDomainEvent
    {
        public int Version { get; set; }
    }
}
