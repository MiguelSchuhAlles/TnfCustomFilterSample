using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestAPI.Constants;
using TestAPI.Entities;
using TestAPI.Extensions;
using TestAPI.QueryFilters;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace TestAPI.Contexts
{
    public class ProfessionalDbContext : TnfDbContext
    {
        public DbSet<Professional> Professionals { get; set; }

        private readonly ICustomFilterProvider _customFilterProvider;

        public ProfessionalDbContext(DbContextOptions<ProfessionalDbContext> options, ITnfSession session)
            : base(options, session)
        {
            _customFilterProvider = session.GetService<ICustomFilterProvider>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Professional>(m =>
            {
                m.HasKey(o => o.Id);

                AddNamedQueryFilter(
                    m,
                    professional => professional.Email.Contains("gmail"),
                    CustomFilters.Gmail
                );

                AddNamedQueryFilter(
                    m,
                    professional => professional.Phone.Contains("(51)"),
                    CustomFilters.PhonePOA
                );
            });
        }

        private void AddNamedQueryFilter<T>(EntityTypeBuilder<T> builder, Expression<Func<T, bool>> filter, string name) where T : class
        {
            Expression<Func<T, bool>> filter2 = _ => !_customFilterProvider.IsFilterEnabled(name);

            builder.AddQueryFilter(filter2.Or(filter));
        }
    }
}
