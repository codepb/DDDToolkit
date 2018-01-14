using System.Threading.Tasks;
using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.ApplicationLayer
{
    public interface IWritableApplicationService<T, TId> where T : class, IAggregateRoot<TId>
    {
        Task Add(T aggregate);
        Task Delete(TId id);
        Task Update(TId id, T aggregate);
    }
}