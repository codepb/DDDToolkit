using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core.Interfaces;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer
{
    public class WritableApplicationService<T, TId> : IWritableApplicationService<T, TId> where T : class, IAggregateRoot<TId>
    {
        protected readonly IUnitOfWork _unitOfWork;

        public WritableApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public virtual async Task Add(T aggregate)
        {
            await _unitOfWork.WritableRepository<T, TId>().Add(aggregate);
            await _unitOfWork.Save();
        }

        public virtual async Task Update(TId id, T aggregate)
        {
            aggregate.SetId(id);
            await _unitOfWork.WritableRepository<T, TId>().Update(aggregate);
            await _unitOfWork.Save();
        }

        public virtual async Task Delete(TId id)
        {
            await _unitOfWork.WritableRepository<T, TId>().Remove(id);
            await _unitOfWork.Save();
        }
    }
}
