using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Constants;

namespace TestAPI.QueryFilters
{
    public class CustomFilterOptions
    {
        private readonly Dictionary<string, bool> _filters = new Dictionary<string, bool>();

        public IDictionary<string, bool> Filters => _filters;

        public CustomFilterOptions()
        {
            RegisterFilter(CustomFilters.Gmail, true);
            RegisterFilter(CustomFilters.PhonePOA, false);
        }

        public void RegisterFilter(string filterName, bool isEnabledByDefault)
        {
            if (_filters.Any(f => f.Key == filterName))
                throw new Exception("There is already a filter with name: " + filterName);

            _filters.Add(filterName, isEnabledByDefault);
        }
    }
}
