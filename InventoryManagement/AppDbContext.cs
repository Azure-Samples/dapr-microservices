using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace InventoryManagement
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.Product> Products { get; set; }

        public DbSet<Models.InventoryRequest> Requests { get; set; }

        public DbSet<Models.InventoryRequestLine> OrderLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=inventory.db");
        }
    }
}