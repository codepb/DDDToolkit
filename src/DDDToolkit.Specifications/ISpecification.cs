namespace DDDToolkit.Specifications
{
    public interface ISpecification<T>
    {
        ISpecification<T> And(ISpecification<T> other);
        ISpecification<T> Or(ISpecification<T> other);
        bool IsSatisfiedBy(T obj);
    }
}