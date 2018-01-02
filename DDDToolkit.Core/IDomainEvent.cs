using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Core
{
    public interface IDomainEvent
    {
        int Version { get; set; }
    }
}
