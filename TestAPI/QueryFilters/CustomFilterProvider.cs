using System;
using System.Collections.Generic;
using Tnf;

namespace TestAPI.QueryFilters
{
    public class CustomFilterProvider : ICustomFilterProvider
    {
        private IDictionary<string, bool> _filters;

        public CustomFilterProvider(ICustomFilterOptions customFilterOptions) 
        {
            _filters = new Dictionary<string, bool>(customFilterOptions.Filters);
        }

        public bool IsFilterEnabled(string filter)
        {
            if (!_filters.ContainsKey(filter))
                return false;

            return _filters[filter];
        }

        public IDisposable DisableFilter(string filter)
        {
            if(!_filters.ContainsKey(filter))
                return null;

            _filters[filter] = false;

            return new DisposeAction(() => EnableFilter(filter));
        }

        public IDisposable EnableFilter(string filter)
        {
            if (!_filters.ContainsKey(filter))
                return null;

            _filters[filter] = true;

            return new DisposeAction(() => DisableFilter(filter));
        }
    }
}
