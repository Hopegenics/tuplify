using HG.Tuplify.CognitoTrigger.Persistence.Config;
using HG.Tuplify.CognitoTrigger.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace HG.Tuplify.CognitoTrigger.Persistence
{
    public class TuplifyDbContext : DbContext
    {
        public DbSet<CustomerInfo> CustomerInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = TuplifyConfiguration.GetConnectionString();

            optionsBuilder.UseMySQL(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
