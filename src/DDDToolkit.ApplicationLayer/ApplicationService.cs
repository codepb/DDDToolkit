using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer
{
    public class ApplicationService<T, TId> : IApplicationService<T, TId>
        where T : class, IAggregateRoot<TId>
    {
        private IReadableApplicationService<T, TId> _readableApplicationService;
        private IWritableApplicationService<T, TId> _writableApplicationService;

        public ApplicationService(IUnitOfWork unitOfWork)
        {
            _readableApplicationService = new ReadableApplicationService<T, TId>(unitOfWork);
            _writableApplicationService = new WritableApplicationService<T, TId>(unitOfWork);
        }

        public virtual Task<T> GetById(TId id) => _readableApplicationService.GetById(id);

        public virtual Task<IReadOnlyCollection<T>> GetAll() => _readableApplicationService.GetAll();

        public virtual Task Add(T aggregate) => _writableApplicationService.Add(aggregate);

        public virtual Task Update(TId id, T aggregate) => _writableApplicationService.Update(id, aggregate);

        public virtual Task Delete(TId id) => _writableApplicationService.Delete(id);
    }
}
