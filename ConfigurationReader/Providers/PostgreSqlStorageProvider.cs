using ConfigurationReaderLibrary.Interfaces;
using ConfigurationReaderLibrary.Models;
using Dapper;
using Npgsql;

namespace ConfigurationReaderLibrary.Providers
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
            var connection = new NpgsqlConnection(_connectionString);
            try
            {
                
                connection.Open();
                var query = "SELECT * FROM \"ConfigurationParameters\" WHERE \"ApplicationName\" = @ApplicationName AND \"IsActive\" = true";
                return Task.FromResult(connection.Query<ConfigurationParameter>(query, new { ApplicationName = applicationName }).ToList());
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error retrieving configurations from PostgreSQL: {ex.Message}");
                return default;
            }
            finally
            {
                connection?.Close(); 
            }
        }
    }
}