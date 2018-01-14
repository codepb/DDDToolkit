using DDDToolkit.Core.Events;
using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.Core.EventHandling
{
    interface IEventHandler
    {
        void Handle(IDomainEvent @event);
    }
}