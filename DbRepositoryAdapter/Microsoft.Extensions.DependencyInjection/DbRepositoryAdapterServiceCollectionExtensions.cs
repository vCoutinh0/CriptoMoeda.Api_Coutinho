using DbRepositoryAdapter;
using DbRepositoryAdapter.Repositories;
using Domain.Adapters;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection.DbRepositoryAdapter
{
    public static class DbRepositoryAdapterServiceCollectionExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddDbRepositoryAdapter(
            this IServiceCollection services,
            DbRepositoryAdapterConfiguration dbRepositoryAdapterConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (dbRepositoryAdapterConfiguration == null)
            {
                throw new ArgumentNullException(nameof(dbRepositoryAdapterConfiguration));
            }

            services.AddSingleton(dbRepositoryAdapterConfiguration);


            services.AddDbContext<DbRepositoryAdapterContext>(options =>
                options.UseSqlServer(dbRepositoryAdapterConfiguration.SqlConnectionString));


            services.AddScoped<INegociacoesDbRepositoryAdapter,
                NegociacoesDbRepositoryAdapter>();

            services.AddScoped<INegociacoesHistoricoDbRepositoryAdapter,
                NegociacoesHistoricoDbRepositoryAdapter>();

            return services;
        }
    }
}
