using ConfigurationReaderLibrary.Interfaces;

namespace ConfigurationReaderLibrary.Providers
{
    public static class StorageProviderFactory
    {
        public static IStorageProvider CreateProvider(string connectionString)
        {
            if(connectionString.Contains("Host") && connectionString.Contains("Port"))
            {
                return new PostgreSqlStorageProvider(connectionString);
            }
            else if (connectionString.StartsWith("mongodb://"))
            {
                return new MongoDbStorageProvider(connectionString);
            }
            else if(connectionString.Contains(":") && !connectionString.Contains("Database"))
            {
                return new RedisStorageProvider(connectionString);
            }
            else if (connectionString.Contains("Server") && connectionString.Contains("Database"))
            {
                return new SqlServerStorageProvider(connectionString);
            }
            else
            {
                throw new ArgumentException("Invalid Provider connection string format.");
            }
        }
    }
}