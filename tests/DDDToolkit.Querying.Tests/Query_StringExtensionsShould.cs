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
            var query = Query<TestEntity>.Has(a => a.A).StartingWith("a").And.Has(a => a.B).StartingWith("de");
            
            _testEntity.Satisfies(query).Should().BeTrue();
        }
        [Fact]
        public void StartsWithCorrectlyReturnsFalse()
        {
            var query = Query<TestEntity>.Has(a => a.A).StartingWith("a").And.Has(a => a.B).StartingWith("e");

            _testEntity.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void EndsWithCorrectlyReturnsTrue()
        {
            var query = Query<TestEntity>.Has(a => a.A).EndingWith("c").And.Has(a => a.B).EndingWith("ef");

            _testEntity.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EndsWithCorrectlyReturnsFalse()
        {
            var query = Query<TestEntity>.Has(a => a.A).EndingWith("ab").And.Has(a => a.B).EndingWith("ef");

            _testEntity.Satisfies(query).Should().BeFalse();
        }
    }
}
