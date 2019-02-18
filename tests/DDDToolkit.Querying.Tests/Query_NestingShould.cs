using FluentAssertions;
using Xunit;

namespace DDDToolkit.Querying.Tests
{
    public class Query_OrShould
    {
        private IQuery<string> _c = Query<string>.Is.EqualTo("c");
        private IQuery<string> _a = Query<string>.Is.EqualTo("a");
        private IQuery<string> _b = Query<string>.Is.EqualTo("b");
        [Fact]
        public void CorrectlyHandleNesting()
        {
            _b.AndSatisfying(_c.OrSatisfying(_a)).IsSatisfiedBy("b").Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueWhenRightHandSideIsTrue()
        {
            _b.AndSatisfying(_c).OrSatisfying(_a).IsSatisfiedBy("a").Should().BeTrue();
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
            var query = _a.AndSatisfying(_a).OrSatisfying(_b);
            query.IsSatisfiedBy("a").Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseWhenBothSidesAreFalse()
        {
            _b.AndSatisfying(_a).OrSatisfying(_c).IsSatisfiedBy("a").Should().BeFalse();
        }
    }
}
