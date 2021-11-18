using HG.Tuplify.CognitoTrigger.Service.Config;
using HG.Tuplify.CognitoTrigger.Service.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace HG.Tuplify.CognitoTrigger.Service.Persistence
{
    internal class TuplifyDbContext : DbContext
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
