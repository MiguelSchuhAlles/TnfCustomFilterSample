using Microsoft.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace TestAPI.Contexts
{
    public class SqlServerCrudDbContext : ProfessionalDbContext
    {
        public SqlServerCrudDbContext(DbContextOptions<ProfessionalDbContext> options, ITnfSession session) 
            : base(options, session)
        {
        }
    }
}
