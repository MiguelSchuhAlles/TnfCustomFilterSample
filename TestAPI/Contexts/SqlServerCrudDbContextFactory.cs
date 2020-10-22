using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Tnf.Runtime.Session;

namespace TestAPI.Contexts
{
    public class SqlServerCrudDbContextFactory : IDesignTimeDbContextFactory<SqlServerCrudDbContext>
    {
        public SqlServerCrudDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ProfessionalDbContext>();

            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile($"appsettings.Development.json", false)
                                    .Build();

            var databaseConfiguration = new DatabaseConfiguration(configuration);

            builder.UseSqlServer(databaseConfiguration.ConnectionString);

            return new SqlServerCrudDbContext(builder.Options, NullTnfSession.Instance);
        }
    }
}
