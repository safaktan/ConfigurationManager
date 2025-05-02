// See https://aka.ms/new-console-template for more information
using ConfigurationReaderLibrary;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting Configuration Reader...");
        try
        {
            // Example connection string for PostgreSQL
            string connectionString = "Host=localhost;Port=5432;Database=ConfigurationDb;Username=admin;Password=admin123";
            string applicationName = "SERVICE-A";
            int refreshIntervalInMs = 5000;
            var configReader = new ConfigurationReader(applicationName, connectionString, refreshIntervalInMs);
            while (true)
            {
                Console.WriteLine("Enter the application name (or 'exit' to quit): ");
                applicationName = Console.ReadLine();
                if (applicationName.ToLower() == "exit")
                    break;
                
                var someConfigValue = configReader.GetValue<double>(applicationName);
                Console.WriteLine($"Configuration Value: {someConfigValue}");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving configuration: {ex.Message}");
        }
    }
}