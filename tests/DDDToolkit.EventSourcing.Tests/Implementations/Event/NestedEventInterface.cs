using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.EventSourcing.Tests
{
    class NestedEventInterface : INestedEventInterface
    {
        public int Version { get; set; }
    }
}
