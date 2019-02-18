using DDDToolkit.Querying.Tests.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DDDToolkit.Querying.Tests
{
    public class Query_StringExtensionsShould
    {
        private TestEntity _testEntity = new TestEntity()
        {
            A = "abc",
            B = "def",
            C = 123
        };

        [Fact]
        public void StartsWithCorrectlyReturnsTrue()
        {
            var query = Query<TestEntity>.Has(a => a.A).StartsWith("a").And.Has(a => a.B).StartsWith("de");
            
            _testEntity.Satisfies(query).Should().BeTrue();
        }
        [Fact]
        public void StartsWithCorrectlyReturnsFalse()
        {
            var query = Query<TestEntity>.Has(a => a.A).StartsWith("a").And.Has(a => a.B).StartsWith("e");

            _testEntity.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void EndsWithCorrectlyReturnsTrue()
        {
            var query = Query<TestEntity>.Has(a => a.A).EndsWith("c").And.Has(a => a.B).EndsWith("ef");

            _testEntity.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EndsWithCorrectlyReturnsFalse()
        {
            var query = Query<TestEntity>.Has(a => a.A).EndsWith("ab").And.Has(a => a.B).EndsWith("ef");

            _testEntity.Satisfies(query).Should().BeFalse();
        }
    }
}
