using DDDToolkit.Core.Interfaces;
using System.Collections.Generic;

namespace DDDToolkit.Core.Events
{
    public interface IEventSourcedAggregateRoot<T> : IAggregateRoot<T>
    {
        int Version { get; }

        IReadOnlyCollection<IDomainEvent> DequeueAllEvents();
    }
}