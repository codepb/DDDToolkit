using System.Collections.Generic;
using System.Threading.Tasks;
using DDDToolkit.Core;

namespace DDDToolkit.ApplicationLayer
{
    public interface IApplicationService<T, TId> where T : AggregateRoot<TId>
    {
        Task Add(T aggregate);
        Task Delete(TId id);
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T> GetById(TId id);
        Task Update(TId id, T aggregate);
    }
}