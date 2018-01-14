using System.Collections.Generic;
using System.Threading.Tasks;
using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.API
{
    public interface IReadableAggregateController<T, TId> where T : class, IAggregateRoot<TId>
    {
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T> GetById(TId id);
    }
}