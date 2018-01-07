using DDDToolkit.Core;
using DDDToolkit.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer.Transactions
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private Dictionary<Type, Lazy<object>> _repositories = new Dictionary<Type, Lazy<object>>();

        protected bool IsRegistered<T>()
        {
            return _repositories.ContainsKey(typeof(T));
        }

        protected void Register<T>(object repository) where T : class
        {
            RegisterLazy<T>(new Lazy<object>(() => repository));
        }

        protected void RegisterLazy<T>(Lazy<object> lazyRepository) where T : class
        {
            if (IsRegistered<T>())
            {
                _repositories[typeof(T)] = lazyRepository;
            }
            else
            {
                _repositories.Add(typeof(T), lazyRepository);
            }
        }
        public virtual T Repository<T>() where T : class
        {
            if (!IsRegistered<T>())
            {
                throw new InvalidOperationException($"No repository of Type {typeof(T)} registered");
            }
            return _repositories[typeof(T)].Value as T;
        }

        public abstract IRepository<T, TId> Repository<T, TId>() where T : AggregateRoot<TId>;
        public abstract IReadableRepository<T, TId> ReadableRepository<T, TId>() where T : AggregateRoot<TId>;
        public abstract IWritableRepository<T, TId> WritableRepository<T, TId>() where T : AggregateRoot<TId>;
        public abstract Task Save();
    }
}
