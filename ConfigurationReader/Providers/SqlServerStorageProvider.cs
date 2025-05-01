using ConfigurationReader.Interfaces;
using ConfigurationReader.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ConfigurationReader.Providers
{
    public class SqlServerStorageProvider : IStorageProvider
    {
        private readonly string _connectionString;

        public SqlServerStorageProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<ConfigurationParameter> GetConfigurations(string applicationName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM ConfigurationParameters WHERE ApplicationName = @ApplicationName AND IsActive = 1";
                return connection.Query<ConfigurationParameter>(query, new { ApplicationName = applicationName });
            }
        }
    }

}