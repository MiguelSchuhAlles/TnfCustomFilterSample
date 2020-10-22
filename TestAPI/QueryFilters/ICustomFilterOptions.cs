using System;
using System.Collections.Generic;
using System.Transactions;

namespace TestAPI.QueryFilters
{
    public interface ICustomFilterOptions
    {
        IDictionary<string, bool> Filters { get; }
    }
}
