// See https://aka.ms/new-console-template for more information
using ConfigurationReaderLibrary;
using MongoDB.Bson.Serialization.Serializers;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Configuration Reader...");
        try
        {
            // Example connection string for PostgreSQL
            string connectionString = "Host=localhost;Port=5432;Database=ConfigurationDb;Username=admin;Password=admin123";
            string applicationName = "SERVICE-B";
            int refreshIntervalInMs = 5000;

            Console.WriteLine("Enter the application name ");
            applicationName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(applicationName))
            {
                Console.WriteLine("Application name cannot be empty. Exiting...");
                return;
            }
            Console.WriteLine("Enter the refresh interval in milliseconds (default is 5000ms): ");
            string refreshIntervalInput = Console.ReadLine();
            if (int.TryParse(refreshIntervalInput, out int refreshInterval))
            {
                refreshIntervalInMs = refreshInterval;
            }
            else
            {
                Console.WriteLine("Invalid input. Using default refresh interval of 5000ms.");
            }


            string name = "";
            string type = "";
            var configReader = new ConfigurationReader(applicationName, connectionString, refreshIntervalInMs);
            while (true)
            {
                Console.WriteLine("Enter the application name (or 'exit' to quit): ");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name) || name.ToLower() == "exit")
                    break;

                Console.WriteLine("Enter the type of parameter name (or 'exit' to quit): ");
                type = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(type) || type.ToLower() == "exit")
                    break;

                var typeName = GetTypeFromString(type);
                if(typeName == typeof(int))
                {
                      var someConfigValue = configReader.GetValue<int>(name);
                      Console.WriteLine($"Configuration Value: {someConfigValue}");
                }
                else if(typeName == typeof(double))
                {
                      var someConfigValue = configReader.GetValue<double>(name);
                      Console.WriteLine($"Configuration Value: {someConfigValue}");
                }
                else if(typeName == typeof(bool))
                {
                      var someConfigValue = configReader.GetValue<bool>(name);
                      Console.WriteLine($"Configuration Value: {someConfigValue}");
                }
                else if(typeName == typeof(string))
                {
                      var someConfigValue = configReader.GetValue<string>(name);
                      Console.WriteLine($"Configuration Value: {someConfigValue}");
                }
                else
                {
                    Console.WriteLine($"Unsupported type: {type}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving configuration: {ex.Message}");
        }
    }

    public static Type? GetTypeFromString(string typeName)
    {
        return typeName.ToLower() switch
        {
            "int" => typeof(int),
            "double" => typeof(double),
            "bool" => typeof(bool),
            "string" => typeof(string),
            _ => null
        };
    }
}