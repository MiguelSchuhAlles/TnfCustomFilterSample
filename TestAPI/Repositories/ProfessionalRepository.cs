using TestAPI.Entities;
using TestAPI.Contexts;
using System;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Repositories
{
    public class ProfessionalRepository : EfCoreRepositoryBase<ProfessionalDbContext, Professional>, IProfessionalRepository
    {
        public ProfessionalRepository(IDbContextProvider<ProfessionalDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<IEnumerable<Professional>> GetAllProfessionals()
            => await GetAll().ToArrayAsync();

        public async Task<Professional> InsertProfessionalAsync(Professional professional)
            => await InsertAndSaveChangesAsync(professional);

        public async Task<Professional> UpdateProfessionalAsync(Professional professional, params Expression<Func<Professional, object>>[] changedProperties)
            => Update(professional, changedProperties);
    }
}
