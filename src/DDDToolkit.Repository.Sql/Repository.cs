using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DDDToolkit.Repository.Sql
{
    public class Repository<T, TId, TContext> 
        : ReadableRepository<T, TId, TContext>,
        IRepository<T, TId>
        where T : class, IAggregateRoot<TId>
        where TContext : DbContext
    {
        private WritableRepository<T, TId, TContext> _writableRepository;

        public Repository(TContext context) : base(context)
        {
            _writableRepository = new WritableRepository<T, TId, TContext>(context);
        }
        
        public Task Add(T entity)
        {
            return _writableRepository.Add(entity);
        }

        public Task Update(T entity)
        {
            return _writableRepository.Update(entity);
        }

        public Task Remove(TId id)
        {
            return _writableRepository.Remove(id);
        }
    }
}
