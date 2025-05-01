using System.ComponentModel.DataAnnotations;

namespace ConfigurationUI.ViewModels
{
    public class ConfigurationViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Value { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string ApplicationName { get; set; } = string.Empty;
    }
}