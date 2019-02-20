using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DDDToolkit.Querying.Tests
{
    public class Query_BooleanExtensionsShould
    {
        [Fact]
        public void TrueCorrectlyReturnsTrue()
        {
            var query = Query<bool>.Is.True();

            true.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void TrueCorrectlyReturnsFalse()
        {
            var query = Query<bool>.Is.True();

            false.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void FalseCorrectlyReturnsTrue()
        {
            var query = Query<bool>.Is.False();

            false.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void FalseCorrectlyReturnsFalse()
        {
            var query = Query<bool>.Is.False();

            true.Satisfies(query).Should().BeFalse();
        }
    }
}
