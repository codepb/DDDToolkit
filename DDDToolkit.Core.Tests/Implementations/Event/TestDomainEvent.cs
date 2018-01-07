using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Core.Tests
{
    class TestDomainEvent : IDomainEvent
    {
        public int Version { get ; set; }
    }
}
