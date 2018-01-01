using DDDToolkit.Core;
using DDDToolkit.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDDToolkit.ApplicationLayer.Transactions
{
    public interface IUnitOfWork
    {
        T Repository<T>()
            where T : class;
        IRepository<T, TId> Repository<T, TId>()
            where T : AggregateRoot<TId>;
        IReadableRepository<T, TId> ReadableRepository<T, TId>()
            where T : AggregateRoot<TId>;
        IWritableRepository<T, TId> WritableRepository<T, TId>()
            where T : AggregateRoot<TId>;
        Task Save();
    }
}
