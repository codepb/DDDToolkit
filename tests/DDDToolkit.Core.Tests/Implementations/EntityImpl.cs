using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Core.Tests.Implementations
{
    class EntityImpl<T> : Entity<T>
    {
        public string SomeOtherProperty { get; }

        public EntityImpl(T id, string someOtherProperty)
        {
            Id = id;
            SomeOtherProperty = someOtherProperty;
        }
    }
}
