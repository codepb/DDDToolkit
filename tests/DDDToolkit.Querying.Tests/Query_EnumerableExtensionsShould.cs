using DDDToolkit.Querying.Tests.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DDDToolkit.Querying.Tests
{
    public class Query_EnumerableExtensionsShould
    {
        private IEnumerable<int> _testEnumerable = new[] { 1, 2, 3, 4, 5 };
        private IEnumerable<int> _emptyEnumerable = new int[] { };

        [Fact]
        public void ContainsShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.Containing(3);
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void ContainsShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.Containing(6);
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NotEmptyShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.NotEmpty();
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEmptyShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.NotEmpty();
            _emptyEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void EmptyShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.Empty();
            _emptyEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EmptyShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.Empty();
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void WithAnyShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.WithAny(i => i == 3);
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAnyShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.WithAny(i => i == 9);
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void WithoutAnyShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.WithoutAny(i => i == 7);
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithoutAnyShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.WithoutAny(i => i == 3);
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void WithAllShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.WithAll(i => i < 6 && i > 0);
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithAllShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.WithAll(i => i < 5);
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void WithNotAllShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.WithNotAll(i => i < 5);
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void WithNotAllShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.WithNotAll(i => i < 6 && i > 0);
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void EqualToSequenceShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.EqualToSequence(new[] { 1, 2, 3, 4, 5 });
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void EqualToSequenceShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.EqualToSequence(new[] { 1, 2, 4, 5 });
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }

        [Fact]
        public void NotEqualToSequenceShouldCorrectlyReturnTrue()
        {
            var query = Query<IEnumerable<int>>.Is.NotEqualToSequence(new[] { 1, 2, 3, 4, 5, 6 });
            _testEnumerable.Satisfies(query).Should().BeTrue();
        }

        [Fact]
        public void NotEqualToSequenceShouldCorrectlyReturnFalse()
        {
            var query = Query<IEnumerable<int>>.Is.NotEqualToSequence(new[] { 1, 2, 3, 4, 5 });
            _testEnumerable.Satisfies(query).Should().BeFalse();
        }
    }
}
