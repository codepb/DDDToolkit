using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Core.Tests.Implementations.ValueObject
{
    class ValueObjectImpl : Core.ValueObject
    {
        public int IntProperty { get; }
        public string StringProperty { get; }
        public bool BoolProperty { get; }

        public ValueObjectImpl(int intProperty, string stringProperty, bool boolProperty)
        {
            IntProperty = intProperty;
            StringProperty = stringProperty;
            BoolProperty = boolProperty;
        } 
    }
}
