using DDDToolkit.Core;
using DDDToolkit.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDDToolkit.Repository.Sql
{
    public class Repository<T, TId, TContext> 
        : IRepository<T, TId>
        where T : AggregateRoot<TId>
        where TContext : DbContext
    {
        private ReadableRepository<T, TId, TContext> _readableRepository;
        private WritableRepository<T, TId, TContext> _writableRepository;

        public Repository(TContext context)
        {
            _readableRepository = new ReadableRepository<T, TId, TContext>(context);
            _writableRepository = new WritableRepository<T, TId, TContext>(context);
        }

        
        public Task<IReadOnlyCollection<T>> GetAll()
        {
            return _readableRepository.GetAll();
        }

        public Task<T> GetById(TId id)
        {
            return _readableRepository.GetById(id);
        }

        public Task<IReadOnlyCollection<T>> Query(Expression<Func<T, bool>> query)
        {
            return _readableRepository.Query(query);
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
