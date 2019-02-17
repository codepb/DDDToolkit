using FluentAssertions;
using Xunit;

namespace DDDToolkit.Querying.Tests
{
    public class Query_OrShould
    {
        private Query<string> _c = Query<string>.Is.EqualTo("c");
        private Query<string> _a = Query<string>.Is.EqualTo("a");
        private Query<string> _b = Query<string>.Is.EqualTo("b");
        [Fact]
        public void CorrectlyHandleNesting()
        {
            _b.AndSatisfies(_c.OrSatisfies(_a)).IsSatisfiedBy("b").Should().BeFalse();
        }

        [Fact]
        public void ReturnTrueWhenRightHandSideIsTrue()
        {
            _b.AndSatisfies(_c).OrSatisfies(_a).IsSatisfiedBy("a").Should().BeTrue();
        }

        [Fact]
        public void ReturnTrueWhenLeftHandSideIsTrue()
        {
            _a.AndSatisfies(_a).OrSatisfies(_b).IsSatisfiedBy("a").Should().BeTrue();
        }

        [Fact]
        public void ReturnFalseWhenBothSidesAreFalse()
        {
            _b.AndSatisfies(_a).OrSatisfies(_c).IsSatisfiedBy("a").Should().BeFalse();
        }
    }
}
