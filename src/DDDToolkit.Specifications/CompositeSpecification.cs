using DDDToolkit.Querying;

namespace DDDToolkit.Specifications
{
    internal class CompositeSpecification<T> : Specification<T>
    {
        private CompositeSpecification(IQuery<T> query) : base(query) { }

        public static CompositeSpecification<T> FromAnd(ISpecification<T> first, ISpecification<T> second)
            => new CompositeSpecification<T>(Query<T>.Has(e => first.IsSatisfiedBy(e)).And().Has(e => second.IsSatisfiedBy(e)));

        public static CompositeSpecification<T> FromOr(Specification<T> first, ISpecification<T> second)
            => new CompositeSpecification<T>(Query<T>.Has(e => first.IsSatisfiedBy(e)).Or().Has(e => second.IsSatisfiedBy(e)));
    }
}