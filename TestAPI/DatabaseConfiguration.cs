using Microsoft.Extensions.Configuration;
using System;

namespace TestAPI
{
    public class DatabaseConfiguration
    {
        public DatabaseConfiguration(IConfiguration configuration)
        {
            DefaultSchema = configuration["DefaultSchema"];
            ConnectionStringName = configuration["DefaultConnectionString"];

            if (DatabaseType.SqlServer.ToString().Equals(ConnectionStringName, StringComparison.CurrentCultureIgnoreCase))
            {
                DatabaseType = DatabaseType.SqlServer;
            }
            else
                throw new NotSupportedException($"Invalid ConnectionString name '{ConnectionStringName}'.");

            ConnectionString = configuration[$"ConnectionStrings:{ConnectionStringName}"];
        }

        public string DefaultSchema { get; }
        public string ConnectionStringName { get; }
        public string ConnectionString { get; }
        public DatabaseType DatabaseType { get; }
    }

    public enum DatabaseType
    {
        SqlServer
    }
}
