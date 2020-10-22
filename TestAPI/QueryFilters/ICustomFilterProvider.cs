using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace TestAPI.QueryFilters
{
    public interface ICustomFilterProvider
    {
        bool IsFilterEnabled(string filter);

        IDisposable DisableFilter(string filter);

        IDisposable EnableFilter(string filter);
    }
}
