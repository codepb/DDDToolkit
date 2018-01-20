using DDDToolkit.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DDDToolkit.Core.Repositories
{
    public interface IQuery<T, TId> where T : IAggregateRoot<TId>
    {
        IQuery<T, TId> Where(Expression<Func<T, bool>> query);
        Task<IReadOnlyCollection<T>> Execute();
    }
}
