using DDDToolkit.Querying.Tests.Entities;
using FluentAssertions;
using Xunit;

namespace DDDToolkit.Querying.Tests
{
    public class Query_SyntaxShould
    {
        [Fact]
        public void AllowAndWithoutHas()
        {
            var query = Query<TestEntity>.Has(a => a.A).EqualTo("a").And.NotEqualTo("b").And.Has(a => a.B).EqualTo("b");
            var testEntity = new TestEntity()
            {
                A = "a",
                B = "b",
                C = 1
            };

            testEntity.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void AllowOrWithoutHas()
        {
            var query = Query<TestEntity>.Has(a => a.A).EqualTo("a").Or.EqualTo("b").And.Has(a => a.B).EqualTo("b");
            var testEntity = new TestEntity()
            {
                A = "b",
                B = "b",
                C = 1
            };

            testEntity.Satisfies(query).Should().BeTrue();
        }
    }
}
