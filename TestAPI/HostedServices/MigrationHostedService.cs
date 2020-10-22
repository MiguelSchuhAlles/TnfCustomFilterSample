using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestAPI.Contexts;
using TestAPI.Entities;

namespace TestAPI.HostedServices
{
    public class MigrationHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MigrationHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ProfessionalDbContext>();

            await context.Database.MigrateAsync();

            if (!context.Professionals.Any())
            {
                context.Professionals.Add(new Professional { Id = Guid.NewGuid(), Name = "User a", Phone = "(51)987654321", Email = "a@gmail.com" });
                context.Professionals.Add(new Professional { Id = Guid.NewGuid(), Name = "User b", Phone = "(55)121343411", Email = "b@gmail.com" });
                context.Professionals.Add(new Professional { Id = Guid.NewGuid(), Name = "User c", Phone = "(51)121343411", Email = "c@hotmail.com" });
                context.Professionals.Add(new Professional { Id = Guid.NewGuid(), Name = "User d", Phone = "(21)121343411", Email = "d@hotmail.com" });

                context.SaveChanges();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
