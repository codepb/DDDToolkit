using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer
{
    public class ApplicationService<T, TId> : IApplicationService<T, TId> where T : AggregateRoot<TId>
    {
        protected readonly IUnitOfWork _unitOfWork;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual Task<T> GetById(TId id)
        {
            return _unitOfWork.Repository<T,TId>().GetById(id);
        }

        public virtual Task<IReadOnlyCollection<T>> GetAll()
        {
            return _unitOfWork.Repository<T, TId>().GetAll();
        }

        public virtual async Task Add(T aggregate)
        {
            await _unitOfWork.Repository<T, TId>().Add(aggregate);
            await _unitOfWork.Save();
        }

        public virtual async Task Update(TId id, T aggregate)
        {
            aggregate.SetId(id);
            await _unitOfWork.Repository<T, TId>().Update(aggregate);
            await _unitOfWork.Save();
        }

        public virtual async Task Delete(TId id)
        {
            await _unitOfWork.Repository<T, TId>().Remove(id);
            await _unitOfWork.Save();
        }
    }
}
