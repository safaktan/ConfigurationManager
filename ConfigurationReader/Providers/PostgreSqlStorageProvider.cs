using ConfigurationReader.Interfaces;
using ConfigurationReader.Models;
using Dapper;
using Npgsql;

namespace ConfigurationReader.Providers
{
    public class PostgreSqlStorageProvider : IStorageProvider
    {
        private readonly string _connectionString;

        public PostgreSqlStorageProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<List<ConfigurationParameter>> GetConfigurationsAsync(string applicationName)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM ConfigurationParameters WHERE ApplicationName = @ApplicationName AND IsActive = 1";
                return Task.FromResult(connection.Query<ConfigurationParameter>(query, new { ApplicationName = applicationName }).ToList());
            }
        }
    }
}