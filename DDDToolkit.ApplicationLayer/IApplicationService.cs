using DDDToolkit.Core;
using DDDToolkit.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer
{
    public interface IApplicationService<T, TId>
        where T : IAggregateRoot<TId>
    {
        Task Add(T aggregate);
        Task Delete(TId id);
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T> GetById(TId id);
        Task Update(TId id, T aggregate);
    }
}