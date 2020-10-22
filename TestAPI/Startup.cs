using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestAPI.Contexts;
using TestAPI.DTOs;
using TestAPI.Entities;
using TestAPI.Repositories;
using TestAPI.QueryFilters;
using Microsoft.OpenApi.Models;
using System.IO;
using TestAPI.HostedServices;

namespace TestAPI
{
    public class Startup
    {
        DatabaseConfiguration DatabaseConfiguration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DatabaseConfiguration = new DatabaseConfiguration(configuration);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCorsAll("AllowAll")
                .AddTnfDomain();

            services.AddTnfAspNetCore(builder =>
            {
                builder.Repository(repositoryConfig =>
                {
                    repositoryConfig.Entity<IEntity>(entity =>
                        entity.RequestDto<IDefaultRequestDto>((e, d) => e.Id == d.Id));
                });

                builder.DefaultConnectionString(DatabaseConfiguration.ConnectionString);
            });

            services.AddTnfMetrics(Configuration);

            services.AddSingleton(DatabaseConfiguration);

            //Serviços relacionados ao gerenciamento de Custom Query Filters
            services.AddSingleton<ICustomFilterOptions, InternalCustomFilterOptions>();
            services.AddScoped<ICustomFilterProvider, CustomFilterProvider>();

            if (DatabaseConfiguration.DatabaseType == DatabaseType.SqlServer)
            {
                services.AddTnfEntityFrameworkCore();

                // Registro dos repositórios
                services.AddTransient<IProfessionalRepository, ProfessionalRepository>();
                services.AddTnfDbContext<ProfessionalDbContext, SqlServerCrudDbContext>((config) =>
                {
                    config.DbContextOptions.EnableSensitiveDataLogging();
                    config.UseLoggerFactory();

                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseSqlServer(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseSqlServer(config.ConnectionString);
                });
            }
            else
            {
                throw new NotSupportedException("No database configuration found");
            }

            services
                .AddResponseCompression()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TestAPI.xml"));
                });

            services.AddHostedService<MigrationHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseTnfAspNetCore();

            app.UseTnfMetrics();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud API v1");
            });

            app.UseResponseCompression();

            app.UseTnfHealthChecks();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
