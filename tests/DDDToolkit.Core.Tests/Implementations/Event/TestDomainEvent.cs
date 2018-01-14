using DDDToolkit.Core.Events;

namespace DDDToolkit.Core.Tests
{
    class TestDomainEvent : IDomainEvent
    {
        public int Version { get ; set; }
    }
}
