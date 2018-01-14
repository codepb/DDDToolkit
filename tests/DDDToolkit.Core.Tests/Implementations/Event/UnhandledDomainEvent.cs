using DDDToolkit.Core.Events;

namespace DDDToolkit.Core.Tests
{
    class UnhandledDomainEvent : IDomainEvent
    {
        public int Version { get; set; }
    }
}
