using DDDToolkit.Core.Interfaces;
using System.Collections.Generic;

namespace DDDToolkit.Core
{
    public abstract class AggregateRoot<T> : Entity<T>, IAggregateRoot<T>
    {
        public void SetId(T id) => Id = id;      
    }
}
