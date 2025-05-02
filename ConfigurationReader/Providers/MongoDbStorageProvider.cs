using ConfigurationReaderLibrary.Interfaces;
using ConfigurationReaderLibrary.Models;
using MongoDB.Driver;

namespace ConfigurationReaderLibrary.Providers
{
    public class MongoDbStorageProvider : IStorageProvider
    {
        private readonly IMongoDatabase _database;

        public MongoDbStorageProvider(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("ConfigurationDB");
        }

        public Task<List<ConfigurationParameter>> GetConfigurationsAsync(string applicationName)
        {
            var collection = _database.GetCollection<ConfigurationParameter>("Configurations");
            return collection.Find(c => c.ApplicationName == applicationName && c.IsActive).ToListAsync();
        }
    }
}