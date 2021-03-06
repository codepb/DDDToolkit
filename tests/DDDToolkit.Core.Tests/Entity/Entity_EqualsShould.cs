﻿using DDDToolkit.Core.Tests.Implementations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DDDToolkit.Core.Tests.Entity
{
    public class Entity_EqualsShould
    {
        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(10001)]
        [InlineData(-1)]
        public void BeEqualIfIdIsEqual(int id)
        {
            var entity1 = new EntityImpl<int>(id, "someString");
            var entity2 = new EntityImpl<int>(id, "other");

            entity1.Should().Be(entity2);
        }

        [Theory]
        [InlineData(1,0)]
        [InlineData(0,100)]
        [InlineData(10001,-1)]
        [InlineData(-1,1)]
        public void NotBeEqualIfIdIsDifferent(int id1, int id2)
        {
            var entity1 = new EntityImpl<int>(id1, "someString");
            var entity2 = new EntityImpl<int>(id2, "other");

            entity1.Should().NotBe(entity2);
        }

        [Fact]
        public void BeEqualIfBothNull()
        {
            EntityImpl<int> entity1 = null;
            EntityImpl<int> entity2 = null;

            var test = entity1 == entity2;

            test.Should().BeTrue();
        }

        [Fact]
        public void NotBeEqualIfLeftNullAndRightNotNull()
        {
            EntityImpl<int> entity1 = null;
            EntityImpl<int> entity2 = new EntityImpl<int>(1, "someString");

            var test = entity1 == entity2;

            test.Should().BeFalse();
        }

        [Fact]
        public void NotBeEqualIfLeftNotNullAndRightNull()
        {
            EntityImpl<int> entity1 = new EntityImpl<int>(1, "someString");
            EntityImpl<int> entity2 = null;

            var test = entity1 == entity2;

            test.Should().BeFalse();
        }
    }
}
