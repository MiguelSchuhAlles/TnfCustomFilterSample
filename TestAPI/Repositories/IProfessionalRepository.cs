using TestAPI.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Repositories;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TestAPI.Repositories
{
    public interface IProfessionalRepository : IRepository
    {
        Task<Professional> InsertProfessionalAsync(Professional professional);

        Task<IEnumerable<Professional>> GetAllProfessionals();

        Task<Professional> UpdateProfessionalAsync(Professional professional, params Expression<Func<Professional, object>>[] changedProperties);
    }
}
