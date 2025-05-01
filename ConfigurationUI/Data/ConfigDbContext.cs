using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationUI.Data
{
    public class ConfigDbContext : DbContext
    {
        public ConfigDbContext(DbContextOptions<ConfigDbContext> options) : base(options)
        {
        }

        public DbSet<ConfigurationParameter> ConfigurationParameters { get; set; } = null!;
    }
}