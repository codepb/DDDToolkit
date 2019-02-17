using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Querying.Tests.Entities
{
    class TestEntity
    {
        public string A { get; set; }
        public string B { get; set; }
        public int C { get; set; }
        public TestEntityChild D { get; set; }
    }
}
