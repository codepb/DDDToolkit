using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.ApplicationLayer.Repositories
{
    public interface IRepository { }

    public interface IRepository<T, TId> 
        : IReadableRepository<T, TId>
        , IWritableRepository<T, TId>
        , IRepository
        where T : IAggregateRoot<TId>
    {
    }
}
