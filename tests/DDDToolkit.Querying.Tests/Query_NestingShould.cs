using FluentAssertions;
using Xunit;

namespace DDDToolkit.Querying.Tests
{
    public class Query_OrShould
    {
        private Query<string, string> _c = Query<string>.Is.EqualTo("c");
        private Query<string, string> _a = Query<string>.Is.EqualTo("a");
        private Query<string, string> _b = Query<string>.Is.EqualTo("b");
        [Fact]
        public void CorrectlyHandleNesting()
        {
            _b.And.Satisfying(_c.Or.Satisfying(_a)).IsSatisfiedBy("b").Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueWhenRightHandSideIsTrue()
        {
            _b.And.Satisfying(_c).Or.Satisfying(_a).IsSatisfiedBy("a").Should().BeTrue();
        }

        [Fact]
        public void ReturnTrueWhenLeftHandSideIsTrue()
        {
            var query = _a.AndSatisfying(_a).OrSatisfying(_b);
            query.IsSatisfiedBy("a").Should().BeTrue();
        }

        [Fact]
        public void ReturnTrueWhenLeftHandSideIsTrueUsingPropertySyntax()
        {
            var query = _a.And.Satisfying(_a).Or.Satisfying(_b);
            query.IsSatisfiedBy("a").Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseWhenBothSidesAreFalse()
        {
            _b.And.Satisfying(_a).Or.Satisfying(_c).IsSatisfiedBy("a").Should().BeFalse();
        }
    }
}
