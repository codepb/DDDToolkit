using DDDToolkit.Core;
using DDDToolkit.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.Repository.Sql.Transactions
{
    public class UnitOfWork<TContext>
        where TContext : DbContext
    {
        protected TContext DbContext { get; }
        private Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(TContext context)
        {
            DbContext = context;
        }

        public virtual IRepository<T, TId> Repository<T, TId>()
            where T : AggregateRoot<TId>
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories.Add(typeof(T), new Repository<T, TId, TContext>(DbContext));
            }
            return _repositories[typeof(T)] as IRepository<T, TId>;
        }

        public virtual IReadableRepository<T, TId> ReadableRepository<T, TId>()
            where T : AggregateRoot<TId>
        {
            return Repository<T, TId>();
        }

        public virtual IWritableRepository<T, TId> WritableRepository<T, TId>()
            where T : AggregateRoot<TId>
        {
            return Repository<T, TId>();
        }

        public async Task Save()
        {
            await PreSave();
            await DbContext.SaveChangesAsync();
            await PostSave();
        }

        protected virtual Task PreSave()
        {
            return Task.CompletedTask;
        }

        protected virtual Task PostSave()
        {
            return Task.CompletedTask;
        }
    }
}
