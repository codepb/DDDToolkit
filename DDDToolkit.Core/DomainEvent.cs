﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Core
{
    public class DomainEvent : IDomainEvent
    {
        public int Version { get; set; }
    }
}
