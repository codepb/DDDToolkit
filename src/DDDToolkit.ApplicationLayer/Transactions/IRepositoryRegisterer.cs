using System;
using DDDToolkit.ApplicationLayer.Repositories;
using DDDToolkit.Core.Interfaces;

namespace DDDToolkit.ApplicationLayer.Transactions
{
    public interface IRepositoryRegisterer<T, TId> where T : IAggregateRoot<TId>
    {
        void Use<TResolveTo>() where TResolveTo : IRepository<T, TId>, new();
        void Use<TResolveTo>(Func<TResolveTo> factory) where TResolveTo : class, IRepository<T, TId>;
    }
}