using DDDToolkit.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDDToolkit.EntityFramework.Extensions
{
    /// <summary>
    /// Extensions on Queryables
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Converts a Queryable to a <see cref="IReadOnlyCollection{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of entity for the IQueryable.</typeparam>
        /// <param name="queryable">The IQueryable to convert.</param>
        /// <param name="cancellationToken">A cancellation token to use for the operation.</param>
        /// <returns>A Task returning a <see cref="IReadOnlyCollection{T}"/> from the IQueryable.</returns>
        public static Task<IReadOnlyCollection<T>> ToReadOnlyCollectionAsync<T>(this IQueryable<T> queryable, CancellationToken cancellationToken = default(CancellationToken))
            => queryable.ToListAsync(cancellationToken).ToReadOnlyCollection();
    }
}
