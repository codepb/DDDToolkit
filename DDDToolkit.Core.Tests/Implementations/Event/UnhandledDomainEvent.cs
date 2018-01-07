using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Core.Tests
{
    class UnhandledDomainEvent : IDomainEvent
    {
        public int Version { get; set; }
    }
}
