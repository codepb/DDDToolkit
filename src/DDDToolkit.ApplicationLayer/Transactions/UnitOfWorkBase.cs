using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer.Transactions
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        protected IObjectResolver _objectResolver = new ObjectResolver();

        public T Repository<T>() where T : class, IRepository => _objectResolver.Resolve<T>();
        public abstract IRepository<T, TId> Repository<T, TId>() where T : class, IAggregateRoot<TId>;
        public abstract IReadableRepository<T, TId> ReadableRepository<T, TId>() where T : class, IAggregateRoot<TId>;
        public abstract IWritableRepository<T, TId> WritableRepository<T, TId>() where T : class, IAggregateRoot<TId>;
        public abstract Task Save();
        public abstract Task<ITransaction> BeginTransaction();
    }
}
