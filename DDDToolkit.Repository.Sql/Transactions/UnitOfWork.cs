using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Core;
using DDDToolkit.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            if (!IsRegistered<IRepository<T, TId>>())
            {
                RegisterLazy<IRepository<T, TId>>(new Lazy<object>(() => new Repository<T, TId, TContext>(DbContext)));
            }
            return Repository<IRepository<T, TId >>();
        }

        public override IReadableRepository<T, TId> ReadableRepository<T, TId>() => Repository<T, TId>();

        public override IWritableRepository<T, TId> WritableRepository<T, TId>() => Repository<T, TId>();

        public override async Task Save()
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
