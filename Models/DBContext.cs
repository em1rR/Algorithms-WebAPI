using AlgorithmsWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlgorithmsWebAPI.Models
{
    public class DBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DBContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("InvoiceDB"));
        }

        public DbSet<Company> companies { get; set; } = null!;
        public DbSet<Invoice> invoices { get; set; } = null!;
    }
}
