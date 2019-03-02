using DDDToolkit.Core.Interfaces;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer.Repositories
{
    public interface IWritableRepository<T, TId>
        where T : IAggregateRoot<TId>
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Remove(TId id);
    }
}
