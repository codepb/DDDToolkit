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

        [Fact]
        public void CallHandlerForEventType()
        {
            _aggregateRoot.ApplyEvent(new TestDomainEvent());

            _aggregateRoot.TestDomainEventHandled.Should().BeTrue();
        }

        [Fact]
        public void CallHandlerForBaseEventTypeIfNoHandlerForEventType()
        {
            _aggregateRoot.ApplyEvent(new ChildTestDomainEvent());

            _aggregateRoot.TestDomainEventHandled.Should().BeTrue();
        }

        [Fact]
        public void CallHandlerForInterfaceIfNoHandlerForEventTypeOrAnyBaseType()
        {
            _aggregateRoot.ApplyEvent(new UnhandledDomainEvent());

            _aggregateRoot.FallbackHandlerCalled.Should().BeTrue();
        }

        [Fact]
        public void CallOnlyTheClosestHandlerToType()
        {
            _aggregateRoot.ApplyEvent(new ChildTestDomainEvent());

            _aggregateRoot.TestDomainEventHandled.Should().BeTrue();

            _aggregateRoot.FallbackHandlerCalled.Should().BeFalse();
        }

        [Fact]
        public void CallTheHandlerForTheBaseInterfaceWhenNoOtherHandlerDefined()
        {
            _aggregateRoot.ApplyEvent(new NestedEventInterface());

            _aggregateRoot.FallbackHandlerCalled.Should().BeTrue();
        }
    }
}
