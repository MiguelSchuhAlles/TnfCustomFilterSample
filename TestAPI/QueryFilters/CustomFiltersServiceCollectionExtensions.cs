using Microsoft.Extensions.DependencyInjection;

using System;

using TestAPI.QueryFilters;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CustomFiltersServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomFilters(this IServiceCollection services, Action<CustomFilterOptions> configure)
        {
            services.Configure(configure);

            //Serviços relacionados ao gerenciamento de Custom Query Filters
            services.AddSingleton<ICustomFilterOptions, InternalCustomFilterOptions>();
            services.AddScoped<ICustomFilterProvider, CustomFilterProvider>();

            return services;
        }
    }
}
