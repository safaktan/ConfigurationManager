using Common.Enums;

namespace Common.Models
{
    public class ConfigurationParameter
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
        public ConfigurationType Type { get; set; }
        public bool IsActive { get; set; }
        public string ApplicationName { get; set; } = null!;
    }
}