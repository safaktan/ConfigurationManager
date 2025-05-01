namespace ConfigurationReader.Interfaces
{
    public interface IConfigurationReader
    {
        T GetValue<T> (string key);
    }
}