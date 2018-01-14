using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.Core.Repositories
{
    public interface IRepository<T, TId> 
        : IReadableRepository<T, TId>
        , IWritableRepository<T, TId>
        where T : IAggregateRoot<TId>
    {
    }
}
