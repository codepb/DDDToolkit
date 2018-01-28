using DDDToolkit.Querying;
using System;
using System.Linq.Expressions;

namespace DDDToolkit.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        private IQuery<T> Query { get; }

        protected Specification(IQuery<T> query) => Query = query;

        public ISpecification<T> And(ISpecification<T> other)
            => CompositeSpecification<T>.FromAnd(this, other);

        public ISpecification<T> Or(ISpecification<T> other)
            => CompositeSpecification<T>.FromOr(this, other);

        public bool IsSatisfiedBy(T obj)
            => Query.EvaluateOn(obj);
    }
}
