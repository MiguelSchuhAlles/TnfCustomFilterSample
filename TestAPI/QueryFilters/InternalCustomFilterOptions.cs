using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.QueryFilters
{
    public class InternalCustomFilterOptions : ICustomFilterOptions
    {
        public IDictionary<string, bool> Filters => _options.Value.Filters;

        private IOptions<CustomFilterOptions> _options;

        public InternalCustomFilterOptions(IOptions<CustomFilterOptions> options)
        {
            _options = options;
        }
    }
}
