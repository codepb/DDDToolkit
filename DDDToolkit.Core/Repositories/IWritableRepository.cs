using System.Threading.Tasks;

namespace DDDToolkit.Core.Repositories
{
    public interface IWritableRepository<T, TId> where T : AggregateRoot<TId>
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Remove(TId id);
    }
}
