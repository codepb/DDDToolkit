using DDDToolkit.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDDToolkit.EntityFramework.Extensions
{
    public static class QueryableExtensions
    {
        public static Task<IReadOnlyCollection<T>> ToReadOnlyCollectionAsync<T>(this IQueryable<T> queryable, CancellationToken cancellationToken = default(CancellationToken))
            => queryable.ToListAsync(cancellationToken).ToReadOnlyCollection();
    }
}
