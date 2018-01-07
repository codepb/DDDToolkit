using System;
using System.Collections.Generic;
using System.Text;

namespace DDDToolkit.Core.Tests.Implementations.ValueObject
{
    class ValueObjectWithComplexType : ValueObjectImpl
    {
        public ValueObjectImpl ComplexProperty { get; }

        public ValueObjectWithComplexType(ValueObjectImpl complexProperty, int intProperty, string stringProperty, bool boolProperty) : base(intProperty, stringProperty, boolProperty)
        {
            ComplexProperty = complexProperty;
        }
    }
}
