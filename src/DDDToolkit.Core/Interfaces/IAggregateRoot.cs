namespace DDDToolkit.Core.Interfaces
{
    public interface IAggregateRoot<T> : IEntity<T>
    {
        void SetId(T id);
    }
}