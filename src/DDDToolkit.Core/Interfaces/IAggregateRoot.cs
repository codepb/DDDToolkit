namespace DDDToolkit.Core.Interfaces
{
    public interface IAggregateRoot {}

    public interface IAggregateRoot<T> : IEntity<T>, IAggregateRoot
    {
    }
}