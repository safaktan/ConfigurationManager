namespace ConfigurationReaderLibrary.Interfaces
{
    public interface IConfigurationReader
    {
        T GetValue<T> (string key);
    }
}