using DDDToolkit.Querying.Tests.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DDDToolkit.Querying.Tests
{
    public class Query_SatisfyingShould
    {
        private TestEntity _testEntity = new TestEntity()
        {
            A = "abc",
            B = "def",
            C = 123,
            D = new TestEntityChild()
            {
                E = "ghi",
                F = true,
                G = 456
            }
        };

        [Fact]
        public void ReturnFalseIfLeftHandSideIsFalse()
        {
            var query = Query<TestEntity>.Has(q => q.A).EqualTo("def").And.Has(q => q.D).Satisfying(d => d.E == "ghi");
            query.IsSatisfiedBy(_testEntity).Should().BeFalse();
        }
    }
}
