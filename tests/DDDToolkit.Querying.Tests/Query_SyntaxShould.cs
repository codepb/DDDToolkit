using DDDToolkit.Querying.Tests.Entities;
using FluentAssertions;
using Xunit;

namespace DDDToolkit.Querying.Tests
{
    public class Query_SyntaxShould
    {
        private TestEntity _testEntity = new TestEntity()
        {
            A = "abc",
            B = "def",
            C = 123
        };

        [Fact]
        public void EndsWithCorrectlyReturnsTrue()
        {
            var query = Query<TestEntity>.Has(a => a.A).EndingWith("c").And.Has(a => a.B).EndingWith("ef");

            _testEntity.Satisfies(query).Should().BeTrue();
        }
    }
}
