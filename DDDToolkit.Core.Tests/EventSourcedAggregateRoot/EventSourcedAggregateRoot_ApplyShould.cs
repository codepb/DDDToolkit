using FluentAssertions;
using Xunit;

namespace DDDToolkit.Core.Tests
{
    public class EventSourcedAggregateRoot_ApplyShould
    {
        private readonly EventSourcedAggregateRootImpl<int> _aggregateRoot;

        public EventSourcedAggregateRoot_ApplyShould()
        {
            _aggregateRoot = new EventSourcedAggregateRootImpl<int>();
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

        [Fact]
        public void IncrementVersionByOne()
        {
            var initialVersion = _aggregateRoot.Version;

            _aggregateRoot.ApplyEvent(new TestDomainEvent());

            _aggregateRoot.Version.Should().Be(initialVersion + 1);
        }

        [Fact]
        public void SetEventVersionToAggregateVersion()
        {
            var @event = new TestDomainEvent();

            _aggregateRoot.ApplyEvent(@event);

            @event.Version.Should().Be(_aggregateRoot.Version);
    }
    }
}
