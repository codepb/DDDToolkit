using DDDToolkit.Core.Tests.Implementations.ValueObject;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DDDToolkit.Core.Tests.ValueObject
{
    public class ValueObject_EqualsShould
    {
        [Theory]
        [InlineData(1, "test", true)]
        [InlineData(17, "", false)]
        [InlineData(-5, null, true)]
        public void BeEqualIfAllPropertiesAreEqual(int intProperty, string stringProperty, bool boolProperty)
        {
            var valueObject1 = new ValueObjectImpl(intProperty, stringProperty, boolProperty);
            var valueObject2 = new ValueObjectImpl(intProperty, stringProperty, boolProperty);

            valueObject1.Should().Be(valueObject2);
        }

        [Theory]
        [InlineData(1, 1, "test", "test", true, false)]
        [InlineData(17, 4, "", "", false, false)]
        [InlineData(-5, -5, null, "", true, true)]
        [InlineData(-5, -3, null, "", true, false)]
        public void NotBeEqualIfSomePropertiesDiffer(int intProperty1, int intProperty2, string stringProperty1, string stringProperty2, bool boolProperty1, bool boolProperty2)
        {
            var valueObject1 = new ValueObjectImpl(intProperty1, stringProperty1, boolProperty1);
            var valueObject2 = new ValueObjectImpl(intProperty2, stringProperty2, boolProperty2);

            valueObject1.Should().NotBe(valueObject2);
        }

        [Fact]
        public void BeEqualIfAllPropertiesAreEqualIncludingComplexTypes()
        {
            var valueObject1 = new ValueObjectImpl(2, "string", true);
            var valueObject2 = new ValueObjectImpl(2, "string", true);

            var complexValueObject1 = new ValueObjectWithComplexType(valueObject1, 17, "test", false);
            var complexValueObject2 = new ValueObjectWithComplexType(valueObject2, 17, "test", false);

            complexValueObject1.Should().Be(complexValueObject2);
        }

        [Fact]
        public void NotBeEqualIfComplexTypePropertiesDiffer()
        {
            var valueObject1 = new ValueObjectImpl(2, "string", true);
            var valueObject2 = new ValueObjectImpl(2, "different", true);

            var complexValueObject1 = new ValueObjectWithComplexType(valueObject1, 17, "test", false);
            var complexValueObject2 = new ValueObjectWithComplexType(valueObject2, 17, "test", false);

            complexValueObject1.Should().NotBe(complexValueObject2);
        }
    }
}
