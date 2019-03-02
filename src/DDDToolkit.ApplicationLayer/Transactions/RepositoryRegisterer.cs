using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.Core.Interfaces;
using System;

namespace DDDToolkit.ApplicationLayer.Transactions
{
    internal class RepositoryRegisterer<T, TId> : IRepositoryRegisterer<T, TId> where T : IAggregateRoot<TId>
    {
        private IObjectResolver _objectResolver;

        public RepositoryRegisterer(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public void Use<TResolveTo>() where TResolveTo : IRepository<T, TId>, new()
        {
            _objectResolver.RegisterLazy<IRepository<T, TId>>(new Lazy<object>(() => new TResolveTo()));
        }

        public void Use<TResolveTo>(Func<TResolveTo> factory) where TResolveTo : class, IRepository<T, TId>
        {
            _objectResolver.RegisterLazy<IRepository<T, TId>>(new Lazy<object>(factory));
        }
    }
}