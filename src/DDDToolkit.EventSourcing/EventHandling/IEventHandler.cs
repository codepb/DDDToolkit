using DDDToolkit.EventSourcing.Events;

namespace DDDToolkit.EventSourcing.EventHandling
{
    interface IEventHandler
    {
        void Handle(IDomainEvent @event);
    }
}