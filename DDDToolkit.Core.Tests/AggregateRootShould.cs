using FluentAssertions;
using Xunit;

namespace DDDToolkit.Core.Tests
{
    public class AggregateRoot_ApplyShould
    {
        private readonly AggregateRootImpl<int> _aggregateRoot;

        public AggregateRoot_ApplyShould()
        {
            _aggregateRoot = new AggregateRootImpl<int>();
        }

        [Fact]
        public void AddEventToEventQueue()
        {
            _aggregateRoot.ApplyEvent(new TestDomainEvent());

            _aggregateRoot.DequeueAllEvents().Should().ContainItemsAssignableTo<TestDomainEvent>();
        }
    }
}
