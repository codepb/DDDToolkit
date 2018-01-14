using System.Collections.Generic;
using System.Threading.Tasks;
using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.ApplicationLayer
{
    public interface IReadableApplicationService<T, TId> where T : class, IAggregateRoot<TId>
    {
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T> GetById(TId id);
    }
}