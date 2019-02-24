using System.Collections.Generic;
using System.Threading.Tasks;
using DDDToolkit.Core.Interfaces;
using FluentQueries;

namespace DDDToolkit.ApplicationLayer
{
    public interface IReadableApplicationService<T, TId> where T : class, IAggregateRoot<TId>
    {
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T> GetById(TId id);
        Task<IReadOnlyCollection<T>> Query(IQuery<T> query);
    }
}