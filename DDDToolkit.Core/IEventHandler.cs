using System;

namespace DDDToolkit.Core
{
    interface IEventHandler
    {
        void Handle(IDomainEvent @event);
    }
}