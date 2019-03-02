using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DDDToolkit.Repository.Sql.Transactions
{
    public class UnitOfWork<TContext> : UnitOfWorkBase
        where TContext : DbContext
    {
        protected TContext DbContext { get; }

        public UnitOfWork(TContext context)
        {
            DbContext = context;
        }

        public override IRepository<T, TId> Repository<T, TId>()
        {
            if (!_objectResolver.IsRegistered<IRepository<T, TId>>())
            {
                _objectResolver.RegisterLazy<IRepository<T, TId>>(new Lazy<object>(() => new Repository<T, TId, TContext>(DbContext)));
            }
            return _objectResolver.Resolve<IRepository<T, TId>>();
        }

        public override IReadableRepository<T, TId> ReadableRepository<T, TId>() => Repository<T, TId>();

        public override IWritableRepository<T, TId> WritableRepository<T, TId>() => Repository<T, TId>();

        public override async Task Save()
        {
            await PreSave();
            await DbContext.SaveChangesAsync();
            await PostSave();
        }

        public override async Task<ITransaction> BeginTransaction()
        {
            var transaction = await DbContext.Database.BeginTransactionAsync();
            return new Transaction(transaction);
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
