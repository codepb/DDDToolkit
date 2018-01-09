using DDDToolkit.ApplicationLayer;
using DDDToolkit.ApplicationLayer.Transactions;
using DDDToolkit.Repository.Sql.Transactions;
using DDDToolkit.Samples.Library.Application;
using DDDToolkit.Samples.Library.Domain;
using DDDToolkit.Samples.Library.Repository.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDToolkit.Samples.Library.UI.Web.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationService<Book, int>, BookService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<LibraryContext>>();
            // Entity framework
            services.AddDbContextPool<LibraryContext>(options => {
                options.UseSqlServer(connectionString);
            }
            );
            return services;
        }
    }
}
