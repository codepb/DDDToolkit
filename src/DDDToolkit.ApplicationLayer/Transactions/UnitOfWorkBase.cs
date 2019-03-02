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

        protected IRepositoryRegisterer<T, TId> For<T, TId>() where T : IAggregateRoot<TId>
        {
            return new RepositoryRegisterer<T, TId>(_objectResolver);
        }

        public abstract IRepository<T, TId> Repository<T, TId>() where T : class, IAggregateRoot<TId>;
        public abstract IReadableRepository<T, TId> ReadableRepository<T, TId>() where T : class, IAggregateRoot<TId>;
        public abstract IWritableRepository<T, TId> WritableRepository<T, TId>() where T : class, IAggregateRoot<TId>;
        public abstract Task Save();
        public abstract Task<ITransaction> BeginTransaction();
    }
}
